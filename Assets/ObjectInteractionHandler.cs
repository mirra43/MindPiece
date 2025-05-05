using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectInteractionHandler : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        // Get the XR Grab Interactable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // Subscribe to the select entered event
            grabInteractable.selectEntered.AddListener(OnGrabbed);

            // Optionally, subscribe to the select exited event
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            // Unsubscribe from the events
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    // Called when the object is grabbed
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} was grabbed!");
        // Call your custom method here
        CustomOnGrabMethod();
    }

    // Called when the object is released
    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log($"{gameObject.name} was released!");
        // Call your custom method here
        CustomOnReleaseMethod();
    }

    private void CustomOnGrabMethod()
    {
        // Add your custom logic for when the object is grabbed
        Debug.Log("Custom logic for grabbing the object.");
    }

    private void CustomOnReleaseMethod()
    {
        // Add your custom logic for when the object is released
        Debug.Log("Custom logic for releasing the object.");
    }
}
