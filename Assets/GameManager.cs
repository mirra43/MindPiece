using UnityEngine;
using System.Collections.Generic;
using Meta.WitAi;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject UIPuzzleCompleted;
    public TMP_Text descriptionText;
    private List<Piece> pieces = new List<Piece>();

    private List<Vector2> availablePositions = new List<Vector2>
    {
        new Vector2(-0.6f, 1.2f),
        new Vector2(0f, 1.2f),
        new Vector2(0.6f, 1.2f),
        new Vector2(1.2f, 0.7f),
        new Vector2(1.2f, 0f),
        new Vector2(1.2f, -0.7f),
        new Vector2(0.6f, -1.2f),
        new Vector2(0f, -1.2f),
        new Vector2(-0.6f, -1.2f)
    };

    private void Start()
    {
        FindAllPieces();
        AssignRandomPositions();
        CreateDescription();
    }

    private void FindAllPieces()
    {
        // Find all objects in the scene that have the Piece script
        pieces = new List<Piece>(FindObjectsOfType<Piece>());
    }

    private void AssignRandomPositions()
    {
        if (pieces.Count > availablePositions.Count)
        {
            Debug.LogWarning("More pieces than available positions! Some pieces will not be positioned correctly.");
            return;
        }

        // Create a copy of the available positions and shuffle it
        List<Vector2> shuffledPositions = new List<Vector2>(availablePositions);
        Shuffle(shuffledPositions);

        // Assign positions
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].SetInitialPosition(shuffledPositions[i]);
        }
    }

    // Fisher-Yates Shuffle
    private void Shuffle(List<Vector2> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector2 temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void CheckPiecesPositions()
    {
        bool allPiecesCorrect = true;

        foreach (Piece piece in pieces)
        {
            if (piece != null)
            {
                Debug.Log(piece.name + " is in correct place: " + piece.pieceIsInCorrectPlace);

                if (!piece.pieceIsInCorrectPlace)
                {
                    allPiecesCorrect = false;
                }
            }
        }

        if (allPiecesCorrect)
        {
            PuzzleCompleted();
        }
    }

    private void PuzzleCompleted()
    {
        UIPuzzleCompleted.SetActive(true);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void CreateDescription()
    {
        if (PlayerPrefs.HasKey("Description"))
        {
            string loadedText = PlayerPrefs.GetString("Description");
            descriptionText.text = loadedText;
        }
    }

}
