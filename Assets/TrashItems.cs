using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashItems : MonoBehaviour
{
    public GameLogic gameLogic;
    public string brokenBookTag;
    public string brokenDuckTag;
    public string brokenPhoneTag;
    public Light worldLight;

    private HashSet<GameObject> processedObjects = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        // Check if the object was already processed
        if (processedObjects.Contains(obj)) return;

        // Mark the object as processed
        processedObjects.Add(obj);

        // Check tags and update score
        if (obj.CompareTag(brokenBookTag) || obj.CompareTag(brokenDuckTag) || obj.CompareTag(brokenPhoneTag))
        {
            Debug.Log("Object processed: " + obj);
            gameLogic.UpdateScore(1);
            worldLight.color = Color.green;
            StartCoroutine(ResetColorLight());
        }
        else
        {
            gameLogic.UpdateScore(-1);
            worldLight.color = Color.red;
            StartCoroutine(ResetColorLight());
        }

        Destroy(obj);
    }

    private IEnumerator ResetColorLight()
    {
        yield return new WaitForSeconds(3f); // Waits for 3 seconds
        worldLight.color = Color.white;
    }
}
