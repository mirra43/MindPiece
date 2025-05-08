using UnityEngine;
using Oculus.Interaction;
using System.Collections;

public class Piece : MonoBehaviour
{
    public GameManager manager;
    public Vector2 initialPosition;
    public int pieceID = 0;
    public int pieceCurrentPlaceID = 0;
    public bool pieceIsInCorrectPlace = false;
    private Grabbable grabbable;
    private bool isSnaped = false;
    private float intialPositionY = 0.45f;

    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
    }

    public void SetInitialPosition(Vector2 position)
    {
        initialPosition = position;
        transform.localPosition = new Vector3(initialPosition.x, intialPositionY, initialPosition.y);
    }

    public void ChangeBoolSnap(bool state)
    {
        isSnaped = state;
    }

    public void OnGrab()
    {
        Debug.Log("Piece Picked Up");
        //Vector3 liftAmount = new Vector3(0, 0.8f, 0);
        //transform.localPosition += liftAmount;
    }

    public void OnReleased()
    {
        var snapInteractable = GetComponent<SnapInteractable>();
        if (isSnaped)
        {
            if(pieceCurrentPlaceID == pieceID)
            {
                pieceIsInCorrectPlace = true;
                manager.CheckPiecesPositions();
            }
            else
            {
                pieceIsInCorrectPlace = false;
            }
            return;
        }
        pieceIsInCorrectPlace = false;
        Debug.Log("Piece Released");
        StartCoroutine(MoveBackToInitial());
    }

    private IEnumerator MoveBackToInitial()
    {
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = new Vector3(initialPosition.x, intialPositionY, initialPosition.y);

        Quaternion startRot = transform.localRotation;
        Quaternion endRot = Quaternion.Euler(-90f, 0f, 0f); // Target rotation

        float time = 0f;
        float duration = 0.2f;

        while (time < duration)
        {
            float t = time / duration;
            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            transform.localRotation = Quaternion.Slerp(startRot, endRot, t);

            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = endPos;
        transform.localRotation = endRot;

        pieceCurrentPlaceID = 0;
        pieceIsInCorrectPlace = false;
    }

}
