using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public Material originalMaterial;
    public Color hoverColor = Color.yellow;
    private Material instanceMaterial;
    private Color originalColor;

    private void Start()
    {
        if (originalMaterial != null)
        {
            // Create a new material instance for this object
            instanceMaterial = new Material(originalMaterial);
            // Apply the instantiated material to the object's renderer
            Renderer renderer = GetComponent<Renderer>();
            Material[] materials = renderer.materials;

            // Find the original material in the materials array and replace it with the instance
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].name.Contains(originalMaterial.name))
                {
                    materials[i] = instanceMaterial;
                    break;
                }
            }

            renderer.materials = materials;
            originalColor = instanceMaterial.color; // Use instanceMaterial.GetColor("_Color") if needed
        }
    }

    private void OnMouseEnter()
    {
        if (instanceMaterial != null)
        {
            instanceMaterial.color = hoverColor; // Use instanceMaterial.SetColor("_Color", hoverColor) if needed
        }
    }

    private void OnMouseExit()
    {
        if (instanceMaterial != null)
        {
            instanceMaterial.color = originalColor; // Use instanceMaterial.SetColor("_Color", originalColor) if needed
        }
    }
}
