using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopicList : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform contentPanel;

    private void ClearContentPanel()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }


    public void Initialize(TOPICS topic, int grade)
    {
        ClearContentPanel();

        GameObject newItem = Instantiate(itemPrefab, contentPanel);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = "hello one";
    }

}

