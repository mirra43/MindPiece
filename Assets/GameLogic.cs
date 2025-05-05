using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SocialPlatforms.Impl;

public class GameLogic : MonoBehaviour
{
    public PedidoCreator pedidoCreator;
    public Collider deleteItemsGroundCollider;
    public LayerMask itemsLayer;
    public TextMesh taskText;
    public TextMesh timerText;
    public TextMesh scoreText;
    public TextMesh highscoreText;
    public Light worldLight;

    public float timeBegin = 120f;
    float timeRemaining;
    public int score;

    public SpawnItems spawnBook;
    public SpawnItems spawnDuck;
    public SpawnItems spawnPhone;

    public GameObject boxSpawner;
    public GameObject boxPrefab;

    bool isPlaying = false;

    private void Start()
    {
        UpdateHighScore();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            SpawnBox();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((itemsLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(other.gameObject, 1f);
        }
    }

    public void StartDay()
    {
        if (isPlaying) return;
        isPlaying = true;
        worldLight.color = Color.black;
        timeRemaining = timeBegin;
        score = 0;
        scoreText.text = "Score: " + score.ToString("D2");
        SpawnItems();
        StartCoroutine(StartDayWithDelay());
        
    }

    private IEnumerator StartDayWithDelay()
    {
        yield return new WaitForSeconds(5f); // Waits for 5 seconds
        worldLight.color = Color.white;
        StartCoroutine(StartTimer()); // Calls StartTimer after delay
        pedidoCreator.ItemGenerator();
    }

    IEnumerator StartTimer()
    {
        while (timeRemaining > 0)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
        }

        timerText.text = "00:00"; // Ensure timer stops at 00:00
        TurnOffDay();
    }

    private void UpdateHighScore()
    {
        highscoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
        if (score < PlayerPrefs.GetInt("HighScore", 0)) return;
        PlayerPrefs.SetInt("HighScore", score);
        PlayerPrefs.Save();
        highscoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public void UpdateScore(int pointsToAdd)
    {
        score += pointsToAdd;
        if(score<0) score = 0;
        scoreText.text = "Score: " + score.ToString("D2");
    }

    public bool isPlayingBool()
    {
        return isPlaying;
    }

    private void TurnOffDay()
    {
        isPlaying = false;
        UpdateHighScore();
        taskText.text = "";
        DestroyAllItems();
    }

    private void DestroyAllItems()
    {
        // Find all game objects in the scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Check if the object's layer matches "item" or "box"
            if (obj.layer == LayerMask.NameToLayer("item") || obj.layer == LayerMask.NameToLayer("box"))
            {
                Destroy(obj);
            }
        }
    }

    public void SpawnBox()
    {
        if (!isPlayingBool()) return;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer("box"))
            {
                Destroy(obj);
            }
        }

        Instantiate(boxPrefab, boxSpawner.transform.position, boxSpawner.transform.rotation);
    }

    private void SpawnItems()
    {
        spawnBook.SpawnItem();
        spawnDuck.SpawnItem();
        spawnPhone.SpawnItem();
    }
}
