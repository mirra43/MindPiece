using UnityEngine;
using Oculus.Interaction;

public class RayHoverColorChanger : MonoBehaviour
{
    [Header("Settings")]
    public Color hoverColor = Color.green;
    private Renderer rend;
    private Color originalColor;


    private void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    public void Hover()
    {
        if (rend != null)
        {
            rend.material.color = hoverColor;
        }
    }

    public void Unhover()
    {
        if (rend != null)
        {
            rend.material.color = originalColor;
        }
    }
}
