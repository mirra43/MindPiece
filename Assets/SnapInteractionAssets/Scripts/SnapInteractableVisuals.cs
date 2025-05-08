using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SnapInteractableVisuals : MonoBehaviour
{
    [SerializeField] private SnapInteractable snapInteractable;
    [SerializeField] private Material hoverMaterial;

    private GameObject currentInteractorGameObject;
    private SnapInteractor currentInteractor;

    private void OnEnable()
    {
        snapInteractable.WhenInteractorAdded.Action += WhenInteractorAdded_Action;
        snapInteractable.WhenSelectingInteractorViewAdded += SnapInteractable_WhenSelectingInteractorViewAdded;
        snapInteractable.WhenInteractorViewRemoved += SnapInteractable_WhenInteractorViewRemoved;
        snapInteractable.WhenInteractorViewAdded += SnapInteractable_WhenInteractorViewAdded;
    }

    private void WhenInteractorAdded_Action(SnapInteractor obj)
    {
        if (currentInteractor == null)
            currentInteractor = obj;
        else if (currentInteractor != obj)
        {
            currentInteractor = obj;
            var tempGP = currentInteractorGameObject;
            Destroy(tempGP);
            currentInteractorGameObject = null;
        }
        else
            return;

        SetupGhostModel(obj);
    }

    private void SnapInteractable_WhenSelectingInteractorViewAdded(IInteractorView obj)
    {
        currentInteractorGameObject?.SetActive(false);
    }

    private void SnapInteractable_WhenInteractorViewAdded(IInteractorView obj)
    {
        currentInteractorGameObject?.SetActive(true);
    }

    private void SnapInteractable_WhenInteractorViewRemoved(IInteractorView obj)
    {
        currentInteractorGameObject.SetActive(false);
    }

    private void SetupGhostModel(SnapInteractor interactor)
    {
        // Create ghost object
        currentInteractorGameObject = new GameObject(interactor.transform.parent?.name + "_Ghost");

        // Set position, rotation and scale matching the interactor's parent (world aligned)
        currentInteractorGameObject.transform.position = interactor.transform.parent.position;
        currentInteractorGameObject.transform.rotation = interactor.transform.parent.rotation;
        currentInteractorGameObject.transform.localScale = interactor.transform.parent.lossyScale;

        // Set it as a child of this script's object (optional, depends if you want ghost to follow)
        currentInteractorGameObject.transform.SetParent(transform, true); // true = worldPositionStays

        // Copy mesh from parent
        var parentMesh = interactor.transform.parent.GetComponent<MeshFilter>();
        if (parentMesh != null)
        {
            currentInteractorGameObject.AddComponent<MeshFilter>().mesh = parentMesh.sharedMesh;
            currentInteractorGameObject.AddComponent<MeshRenderer>().material = hoverMaterial;
        }

        // Copy all child meshes (keep world transforms)
        var childMeshes = interactor.transform.parent.GetComponentsInChildren<MeshFilter>();
        foreach (var item in childMeshes)
        {
            if (item == parentMesh) continue; // Skip if already copied

            var newChild = new GameObject(item.name + "_Ghost");
            newChild.transform.position = item.transform.position;
            newChild.transform.rotation = item.transform.rotation;
            newChild.transform.localScale = item.transform.lossyScale;
            newChild.transform.SetParent(currentInteractorGameObject.transform, true);

            newChild.AddComponent<MeshFilter>().mesh = item.sharedMesh;
            newChild.AddComponent<MeshRenderer>().material = hoverMaterial;
        }
    }

}