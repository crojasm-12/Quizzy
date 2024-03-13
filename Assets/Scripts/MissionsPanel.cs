using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsPanel : MonoBehaviour
{
    private MainScene mainScene;

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
        mainScene = FindFirstObjectByType<MainScene>();
        if (mainScene == null)
        {
            Debug.LogError("MainFrame instances not found in MissionsPanel!");
        }
    }


    public void OnBack()
    {
        HidePanel();
        mainScene.ShowPanel();
    }
}
