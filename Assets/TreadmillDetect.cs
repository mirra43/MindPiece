using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreadmillDetect : MonoBehaviour
{
    public PedidoCreator pedidoCreator;
    public GameLogic gameLogic;
    public LayerMask itemsLayer;
    public LayerMask boxLayer;
    public Light worldLight;
    public GameObject boxObject;

    private List<GameObject> objectsDelivered = new List<GameObject>();
    bool isCollecting = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!objectsDelivered.Contains(other.gameObject) &&
            (((itemsLayer.value & (1 << other.gameObject.layer)) != 0) ||
            ((boxLayer.value & (1 << other.gameObject.layer)) != 0)))
        {
            objectsDelivered.Add(other.gameObject);

            // Start the collection timer if not already running
            if (!isCollecting)
            {
                StartCoroutine(CollectObjectsForDuration(0.5f));
            }
        }
    }

    private IEnumerator CollectObjectsForDuration(float duration)
    {
        isCollecting = true;
        yield return new WaitForSeconds(duration);
        CheckItemsDelivered();
        isCollecting = false;

        // Collection is done, you can now process the collected objects
        Debug.Log("Collected objects: " + objectsDelivered.Count);
    }

    private void CheckItemsDelivered()
    {
        bool correctItemDelivered = false;
        bool boxDelivered = false;

        foreach (GameObject obj in objectsDelivered)
        {
            if (pedidoCreator.CheckItemDelivered(obj))
            {
                correctItemDelivered = true;
            }

            if (obj.CompareTag(boxObject.tag))
            {
                boxDelivered = true;
            }

            Destroy(obj);
        }

        if (correctItemDelivered && boxDelivered)
        {
            gameLogic.UpdateScore(2);
            worldLight.color = Color.green;
            StartCoroutine(ResetColorLight());
        }
        else
        {
            gameLogic.UpdateScore(-2);
            worldLight.color = Color.red;
            StartCoroutine(ResetColorLight());
        }

        objectsDelivered.Clear();
        pedidoCreator.ItemGenerator();
    }

    private IEnumerator ResetColorLight()
    {
        yield return new WaitForSeconds(3f); // Waits for 5 seconds
        worldLight.color = Color.white;
    }
}