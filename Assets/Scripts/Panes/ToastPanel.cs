using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class ToastPanel : MonoBehaviour
{
    public GameObject Panel;
    public TextMeshProUGUI TextMessage;
    public float delay = 5f;

    public void ShowPanel()
    {
        Panel.SetActive(true);
    }

    public void HidePanel()
    {
        Panel.SetActive(false);
    }

    public void ShowError(string message)
    {
        if (TextMessage != null)
        {
            TextMessage.text = message;
            ShowPanel();
            StartCoroutine(HideAfterDelay(delay));
        }
    }

    public void ShowMessage(string message)
    {
        if (TextMessage != null)
        {
            TextMessage.text = message;
            ShowPanel();
            StartCoroutine(HideAfterDelay(delay));
        }
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HidePanel();
    }
}
