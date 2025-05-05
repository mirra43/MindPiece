using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRChangeMaterialOnPickup : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material pickedUpMaterial;

    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (defaultMaterial != null)
            objectRenderer.material = defaultMaterial;
    }

    public void OnSelectEntered()
    {
        if (pickedUpMaterial != null)
            objectRenderer.material = pickedUpMaterial;
    }

    public void OnSelectExited()
    {
        if (defaultMaterial != null)
            objectRenderer.material = defaultMaterial;
    }
}
