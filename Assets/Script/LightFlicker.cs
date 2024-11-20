using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class URPFlickerEffect : MonoBehaviour
{
    // Reference to the Renderer
    private Renderer objRenderer;

    // Emission property ID
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    // Reference to the Light component
    private Light flickerLight;

    // Flicker settings
    [Header("Flicker Settings")]
    [Range(1f, 50f)]
    [Tooltip("Controls the speed of the flickering effect.")]
    public float flickerSpeed = 10f;

    // Emission Intensity settings
    [Header("Emission Intensity Settings")]
    [Range(0f, 5f)]
    [Tooltip("Minimum emission intensity.")]
    public float minEmissionIntensity = 0.5f;

    [Range(0f, 5f)]
    [Tooltip("Maximum emission intensity.")]
    public float maxEmissionIntensity = 1.5f;

    // Light Intensity settings
    [Header("Light Intensity Settings")]
    [Range(0f, 10f)]
    [Tooltip("Minimum light intensity.")]
    public float minLightIntensity = 1f;

    [Range(0f, 35f)]
    [Tooltip("Maximum light intensity.")]
    public float maxLightIntensity = 35f;

    // Base Emission Color (Set this in the Inspector to match your material's emission color)
    [Header("Base Emission Color")]
    [ColorUsage(true, true)]
    public Color baseEmissionColor = Color.yellow;

    // Material Property Block
    private MaterialPropertyBlock propBlock;

    private void Start()
    {
        // Get the Renderer component
        objRenderer = GetComponent<Renderer>();
        if (objRenderer == null)
        {
            Debug.LogError("URPFlickerEffect: No Renderer found on the GameObject.");
            enabled = false;
            return;
        }

        // Get the Light component
        flickerLight = GetComponent<Light>();
        if (flickerLight == null)
        {
            Debug.LogError("URPFlickerEffect: No Light component found on the GameObject.");
            enabled = false;
            return;
        }

        // Initialize Material Property Block
        propBlock = new MaterialPropertyBlock();

        // Set the initial emission color
        propBlock.SetColor(EmissionColor, baseEmissionColor * minEmissionIntensity);
        objRenderer.SetPropertyBlock(propBlock);

        // Set initial Light intensity
        flickerLight.intensity = minLightIntensity;

        Debug.Log("URPFlickerEffect: Emission and Light initialized.");
    }

    private void Update()
    {
        // Calculate a smooth random intensity using Perlin noise
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f); // Value between 0 and 1

        // Map noise to emission intensity
        float emissionIntensity = Mathf.Lerp(minEmissionIntensity, maxEmissionIntensity, noise);
        Color finalEmissionColor = baseEmissionColor * emissionIntensity;

        // Map noise to Light intensity
        float lightIntensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, noise);
        flickerLight.intensity = lightIntensity;

        // Set the emission color in the property block
        propBlock.SetColor(EmissionColor, finalEmissionColor);
        objRenderer.SetPropertyBlock(propBlock);

        // Debugging: Log intensity values periodically
        if (Time.frameCount % 60 == 0) // Every ~1 second at 60fps
        {
            //Debug.Log($"URPFlickerEffect: Emission Intensity = {emissionIntensity}, Light Intensity = {lightIntensity}");
        }
    }
}