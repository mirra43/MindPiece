using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    private List<GameObject> objectsInside = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInside.Contains(other.gameObject))
        {
            objectsInside.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInside.Contains(other.gameObject))
        {
            objectsInside.Remove(other.gameObject);
        }
    }

    public void ChangeObjectsMaterial(Material material)
    {
        foreach (GameObject obj in objectsInside)
        {
            // Iterate through all children of obj
            foreach (Transform child in obj.transform)
            {
                if (child.CompareTag("ChangeMaterial"))
                {
                    Renderer childRenderer = child.GetComponent<Renderer>();
                    if (childRenderer != null)
                    {
                        childRenderer.material = material;
                    }
                }
            }
        }
    }


}
