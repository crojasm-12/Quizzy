using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Animations.Rigging;
using static System.Net.Mime.MediaTypeNames;


public class LoginPopupManager : MonoBehaviour
{
    public GameObject Popup_Login;

    public GameObject Text_UI;
    public GameObject Text_Password;


    private string _userId;
    private string _password;

    void Start()
    {
        if (Popup_Login != null)
        {
            Popup_Login.SetActive(true); // Make the login popup visible
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    IEnumerator PostRequest(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            // Here you can handle the received token as needed
        }
    }

    public void ReadUserID(string userId)
    {

        GameObject ui = Popup_Login.GetComponent<GameObject>();
        Debug.Log($"UserId: [{userId}]");
    }

    public void ReadPassword(string password)
    {
        Debug.Log($"Password: [{password}]");
    }

    public void OnLogin()
    {
        //_userId = Text_UI.text;
        //_password = Text_Password.text;
        TMP_InputField[] texts = Popup_Login.GetComponentsInChildren<TMP_InputField>(true);
        foreach (var text in texts)
        {
            if (text.gameObject.name == "InputField_ID")
            {
                _userId = text.text;
            }
            else if (text.gameObject.name == "InputField_Password")
            {
                _password = text.text;
            }
        }

        HttpService httpService;

        httpService = gameObject.AddComponent<HttpService>();
        httpService.Initialize("https://api.tomorrow-notebook.com");

        string jsonPayload = $"{{ \"email\": \"{_userId}\", \"password\": \"{_password}\" }}";
        StartCoroutine(httpService.Post("/api/idm/v1/logInLocal", null, jsonPayload, (response) =>
            {
                Popup_Login.SetActive(false);
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                Debug.Log("Response: " +  response);
            }
        ));
    }

}
