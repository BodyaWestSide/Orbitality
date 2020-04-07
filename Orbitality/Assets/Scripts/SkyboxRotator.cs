using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speedMultiplier);
    }
}
