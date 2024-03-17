using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class AnswerChoices : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform contentPanel;

    public UnityEvent<string> onChoiceClicked;

    public Sprite SelectedSprite;
    public  Sprite UnselectedSprite;

    private void ClearContentPanel()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddChoice(string choice)
    {
        GameObject newChoice = Instantiate(itemPrefab, contentPanel);
        newChoice.GetComponentInChildren<TextMeshProUGUI>().text = choice;
        AnswerChoice itemChoice = newChoice.GetComponent<AnswerChoice>();
        Image itemImage = newChoice.GetComponent<Image>();
        itemImage.sprite = UnselectedSprite;
        itemChoice.InitializeItem(choice);
        itemChoice.onClick.AddListener(OnChoiceClick);
    }

    public void ShowChoices(List<string> choices)
    {
        ClearContentPanel();
        foreach (string choice in choices)
        {
            AddChoice(choice);
        }
    }

    private void UnselectAll()
    {
        foreach (Transform child in contentPanel)
        {
            Image itemImage = child.gameObject.GetComponent<Image>();
            itemImage.sprite = UnselectedSprite;
        }
    }
    void OnChoiceClick(string name, GameObject gameObject)
    {
        UnselectAll();
        Image itemImage = gameObject.GetComponent<Image>();
        itemImage.sprite = SelectedSprite;
        onChoiceClicked.Invoke(name);
    }
}

