using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Image cooldownBar;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private Renderer matRenderer;
    [SerializeField] private Transform sphere;
    [SerializeField] private GameObject rocket;
    [SerializeField] private Transform rocketLaunchPosition;

    public readonly PlanetEvent OnPlanetDestroyed = new PlanetEvent();

    private float radius;
    private float currentAngle;
    private float rotationSpeed;
    private float angleSpeed;
    private int materialNumber;

    private PlanetType planetType;
    private WaitForSeconds waitCooldown;

    private bool isCooldown;
    private bool isPlayer;
    private float currentHealth;

    public void InitializePlanet(float radius, float size, float startAngle, float rotationSpeed, float angleSpeed, Material rendererMaterial, int materialNumber)
    {
        this.radius = radius;
        currentAngle = startAngle;
        this.rotationSpeed = rotationSpeed;
        this.angleSpeed = angleSpeed;
        this.materialNumber = materialNumber;

        transform.localScale = new Vector3(size, size, size);

        matRenderer.material = rendererMaterial;

        MovePlanet();
    }

    public PlanetData GetPlanetData()
    {
        return new PlanetData
        {
            radius = this.radius,
            currentAngle = this.currentAngle,
            rotationSpeed = this.rotationSpeed,
            angleSpeed = this.angleSpeed,
            isPlayer = this.isPlayer,
            size = this.transform.localScale.x,
            currentHealth = CurrentHealth,
            planetType = this.planetType,
            materialNumber = this.materialNumber
        };
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            hpBar.fillAmount = currentHealth;
            if (isPlayer)
            {
                GameObject playerHP = GameObject.Find("PlayerFillHPBar");
                playerHP.GetComponent<Image>().fillAmount = currentHealth;
            }
            if(currentHealth <= 0)
            {
                DestroyPlanet();
            }
        }
    }

    public void CreatePlanet(PlanetType planetType)
    {
        this.planetType = planetType;
        hpBar.fillAmount = this.planetType.health;
        CurrentHealth = this.planetType.health; 

        waitCooldown = new WaitForSeconds(planetType.cooldown);
        if (isPlayer)
        {
            hudPanel.SetActive(false);
        }
    }

    public bool IsPlayer
    {
        get { return isPlayer; }
        set { isPlayer = value; }
    }

    public void Shoot()
    {
        if (!isCooldown)
        {
            GameObject rocketGameObject = Instantiate(
                rocket,
                rocketLaunchPosition.position,
                rocketLaunchPosition.rotation);
            rocketGameObject.GetComponent<Rigidbody>().AddForce(rocketLaunchPosition.up * this.planetType.rocketForce);
            Rocket rocketObject = rocketGameObject.GetComponent<Rocket>();
            if (rocketObject)
            {
                rocketObject.InitializeRocket(planetType.rocketDamage);
            }

            StartCoroutine(Cooldown());
            StartCoroutine(CooldownBar());
        }
    }


    private void DestroyPlanet()
    {
        OnPlanetDestroyed.Invoke(this);
        Destroy(gameObject);
    }

    private void Update()
    {
        RotatePlanet();
        MovePlanet();

        if (isPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void MovePlanet()
    {
        currentAngle += angleSpeed * Time.deltaTime;

        float x = Mathf.Cos(currentAngle) * radius;
        float z = Mathf.Sin(currentAngle) * radius;

        transform.localPosition = new Vector3(x, 0, z);
    }
    
    private void RotatePlanet()
    {
        sphere.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return waitCooldown;
        isCooldown = false;
    }

    private IEnumerator CooldownBar()
    {

        GameObject playerCooldownBar = GameObject.Find("PlayerFillCooldownBar");
        playerCooldownBar.GetComponent<Image>().fillAmount = currentHealth;

        float elapsedTime = 0;

        while(elapsedTime < planetType.cooldown)
        {
            cooldownBar.fillAmount = Mathf.Lerp(1, 0, elapsedTime / planetType.cooldown);
            if (isPlayer)
            {
                playerCooldownBar.GetComponent<Image>().fillAmount = cooldownBar.fillAmount;
            }            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cooldownBar.fillAmount = 1;
        if (isPlayer)
        {
            playerCooldownBar.GetComponent<Image>().fillAmount = 1;
        }
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rocket rocket = collision.gameObject.GetComponent<Rocket>();
        CurrentHealth -= rocket.damage;
    }
}
