using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameLogic gameLogic;
    public GameObject spawner;
    public BoxCollider spawnerCollider;
    public string tagNormal;
    public string tagBroken;

    public GameObject itemNormal;
    public GameObject itemBroken;

    private List<GameObject> objectsInside = new List<GameObject>();
    private bool isSpawning;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInside.Contains(other.gameObject))
        {
            objectsInside.Add(other.gameObject);
            CheckIfSpawns();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInside.Contains(other.gameObject))
        {
            objectsInside.Remove(other.gameObject);
            CheckIfSpawns();
        }
    }

    void CheckIfSpawns()
    {
        if (isSpawning) return;
        if (!gameLogic.isPlayingBool()) return;

        // If there are no objects inside, spawn an item
        if (objectsInside.Count == 0)
        {
            StartCoroutine(SpawnItemTimer());
            return;
        }

        foreach (GameObject obj in objectsInside)
        {
            if (obj != null)
            {
                // Use CompareTag with string parameters
                if (obj.CompareTag(tagNormal) || obj.CompareTag(tagBroken)) return;
            }
        }

        StartCoroutine(SpawnItemTimer());
    }

    IEnumerator SpawnItemTimer()
    {
        isSpawning = true;
        yield return new WaitForSeconds(2);
        SpawnItem();
    }

    public void SpawnItem()
    {
        float randomValue = Random.value;

        // If random value is less than 1/3, spawn the broken item; otherwise, spawn the normal item
        GameObject itemToSpawn = (randomValue < 1f / 3f) ? itemBroken : itemNormal;

        Instantiate(itemToSpawn, spawner.transform.position, spawner.transform.rotation);
        isSpawning = false;
    }
}
