using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

public class HttpService : MonoBehaviour
{
    private string baseUrl;

    public HttpService()
    {
        this.baseUrl = "https://api.tomorrow-notebook.com";
    }

    public IEnumerator Get(string endpoint, Dictionary<string, string> headers, Action<string> sucessCallback, Action<string> errorCallback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl + endpoint))
        {
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                if (errorCallback != null)
                {
                    errorCallback(request.error);
                }
                Debug.LogError(request.error);
            }
            else
            {
                sucessCallback(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator Post(string endpoint, Dictionary<string, string> headers, string jsonPayload, Action<string> sucessCallback, Action<string> errorCallback = null)
    {
        UnityWebRequest request = new UnityWebRequest(baseUrl + endpoint, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonPayload);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Set additional headers
        if (headers != null)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                request.SetRequestHeader(header.Key, header.Value);
            }
        }
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            errorCallback?.Invoke(request.error);
        }
        else
        {
            sucessCallback?.Invoke(request.downloadHandler.text);
        }
        request.Dispose();
    }

    public IEnumerator Put(string endpoint, Dictionary<string, string> headers, string jsonPayload, Action<string> sucessCallback, Action<string> errorCallback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Put(baseUrl + endpoint, jsonPayload))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                if (errorCallback != null)
                {
                    errorCallback(request.error);
                }
                Debug.LogError(request.error);
            }
            else
            {
                sucessCallback(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator Delete(string endpoint, Dictionary<string, string> headers, Action<string> sucessCallback, Action<string> errorCallback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Delete(baseUrl + endpoint))
        {
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                if (errorCallback != null)
                {
                    errorCallback(request.error);
                }
                Debug.LogError(request.error);
            }
            else
            {
                sucessCallback(request.downloadHandler.text);
            }
        }
    }
}
