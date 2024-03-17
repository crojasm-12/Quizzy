using TMPro;
using UnityEngine;


public class ShopPanel : MonoBehaviour
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
            Debug.LogError("MainFrame instance not found in ShopPanel!");
        }
    }
    public void OnBack()
    {
        HidePanel();
        mainScene.ShowPanel();
    }
}

