using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedidoCreator : MonoBehaviour
{
    public GameLogic gameLogic;
    public List<Material> materials;
    public List<GameObject> objetos;
    public TextMesh texto;

    public Material currentMaterial;
    public GameObject currentObjeto;
    public string displayObject;

    public bool test = false;

    private void Update()
    {
        if (test) 
        {
            ItemGenerator();
            DisplayObject();
            test = false;
        }
    }

    public void ItemGenerator()
    {
        currentMaterial = materials[Random.Range(0,materials.Count)];
        currentObjeto = objetos[Random.Range(0, objetos.Count)];
        DisplayObject();
    }

    public void DisplayObject()
    {
        NameGiver();
        texto.text = displayObject;
        texto.color = currentMaterial.color;
    }

    public void NameGiver() 
    {
        switch (currentObjeto.name)
        {
            case "BookNormal":
                displayObject = "Book";
                
                break;

            case "PhoneNormal":
                displayObject = "Phone";
                
                break;

            case "DuckNormal":
                displayObject = "Duck";
                
                break;

            default:
                break;
        }
    }

    public bool CheckItemDelivered(GameObject objectDelivered)
    {
        // Check if the parent has the correct tag
        if (!objectDelivered.CompareTag(currentObjeto.tag))
        {
            return false; // Parent tag mismatch
        }

        // Find the child with the "ChangeMaterial" tag
        Transform childWithTag = null;
        foreach (Transform child in objectDelivered.transform)
        {
            if (child.CompareTag("ChangeMaterial"))
            {
                childWithTag = child;
                break;
            }
        }

        // If a child with the correct tag was found, compare its material
        if (childWithTag != null)
        {
            Renderer childRenderer = childWithTag.GetComponent<Renderer>();

            if (childRenderer != null)
            {
                Debug.Log(childRenderer.sharedMaterial == currentMaterial);
                return childRenderer.sharedMaterial == currentMaterial;
            }
        }

        return false; // No matching child or material
    }


}
