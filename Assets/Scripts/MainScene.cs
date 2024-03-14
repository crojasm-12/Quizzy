using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    private MissionsPanel missionsPanel;
    private ShopPanel shopPanel;

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
        shopPanel = FindFirstObjectByType<ShopPanel>();
        if (missionsPanel == null || shopPanel == null)
        {
            Debug.LogError("MissionsPanel or ShopPanel instances not found in MainScene!");
        }
    }

    public void OnShopPanel()
    {
        HidePanel();
        shopPanel.ShowPanel();
    }

    public void OnMathematics()
    {
        HidePanel();
        missionsPanel.SetTopic(TOPICS.MATH);
        missionsPanel.ShowPanel();
    }

    public void OnLanguageArts()
    {
        HidePanel();
        missionsPanel.SetTopic(TOPICS.LANGUAGE_ARTS);
        missionsPanel.ShowPanel();
    }

    public void OnSocialStudies()
    {
        HidePanel();
        missionsPanel.SetTopic(TOPICS.SOCIAL_STUDIES);
        missionsPanel.ShowPanel();
    }

    public void OnScience()
    {
        HidePanel();
        missionsPanel.SetTopic(TOPICS.SCIENCE);
        missionsPanel.ShowPanel();
    }
}
