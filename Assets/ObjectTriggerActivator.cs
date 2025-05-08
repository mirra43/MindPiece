using UnityEngine;

public class ObjectTriggerActivator : MonoBehaviour
{
    [Header("Settings")]
    public int snapId;
    public LayerMask targetLayer;            // Which layer(s) to detect
    public GameObject objectToActivate;      // GameObject to activate/deactivate

    private void OnTriggerEnter(Collider other)
    {
        if (IsInLayerMask(other.gameObject, targetLayer))
        {
            if (objectToActivate != null)
                objectToActivate.SetActive(true);
            other.gameObject.GetComponent<Piece>().ChangeBoolSnap(true);
            other.gameObject.GetComponent<Piece>().pieceCurrentPlaceID = snapId;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsInLayerMask(other.gameObject, targetLayer))
        {
            if (objectToActivate != null)
                objectToActivate.SetActive(false);
            other.gameObject.GetComponent<Piece>().ChangeBoolSnap(false);
            other.gameObject.GetComponent<Piece>().pieceCurrentPlaceID = 0;
        }
    }

    // Helper function to check if the object's layer is within the LayerMask
    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }
}
