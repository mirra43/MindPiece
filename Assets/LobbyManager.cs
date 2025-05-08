using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public TMP_InputField imageNameInput;
    public TMP_InputField descriptionInput;
    public Material targetMaterial;
    public Image targetUIImage;
    private string baseUrl = "https://i.imgur.com/";

    public void LoadImageFromInput()
    {
        string imageName = imageNameInput.text.Trim();

        if (!string.IsNullOrEmpty(imageName))
        {
            string fullUrl = baseUrl + imageName;
            StartCoroutine(DownloadImage(fullUrl));
        }
    }

    private IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error downloading image: " + request.error);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);

            if (targetMaterial != null)
            {
                targetMaterial.SetTexture("_MainTex", texture);
            }

            if (targetUIImage != null)
            {
                Rect rect = new Rect(0, 0, texture.width, texture.height);
                Vector2 pivot = new Vector2(0.5f, 0.5f);
                Sprite newSprite = Sprite.Create(texture, rect, pivot);
                targetUIImage.sprite = newSprite;
            }
        }
    }

    public void ChangeScene()
    {
        SaveInputFieldToPrefs();
        SceneManager.LoadScene("mainScene");
    }

    public void SaveInputFieldToPrefs()
    {
        string textToSave = descriptionInput.text;
        PlayerPrefs.SetString("Description", textToSave);
        PlayerPrefs.Save();
    }
}
