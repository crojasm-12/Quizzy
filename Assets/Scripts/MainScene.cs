using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    private MissionsPanel missionsPanel;

    public GameObject Panel;

    public void ShowPanel()
    {
        Panel.SetActive(true);
    }

    public void HidePanel()
    {
        Panel.SetActive(false);
    }

    void Awake() // It's better to use Awake to ensure that references are set before any Start methods call them
    {
        missionsPanel = FindFirstObjectByType<MissionsPanel>();
        if (missionsPanel == null)
        {
            Debug.LogError("MissionsPanel instances not found in MainScene!");
        }
    }


    public void OnMathematics()
    {
        HidePanel();
        missionsPanel.ShowPanel();
        Debug.Log("Clicked on Mathemathics");
    }

    public void OnLanguageArts()
    {
        Debug.Log("Clicked on Language Arts");
    }

    public void OnSocialStudies()
    {
        Debug.Log("Clicked on Social Studies");
    }

    public void OnScience()
    {
        Debug.Log("Clicked on Science");
    }

}
