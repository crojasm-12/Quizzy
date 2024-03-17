using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class AnswerChoice : MonoBehaviour, IPointerClickHandler
{
    private string _choice;
    public UnityEvent<string, GameObject> onClick;

    public void InitializeItem(string displayText)
    {
        _choice = displayText;
        GetComponentInChildren<TextMeshProUGUI>().text = displayText;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke(_choice, this.gameObject);
    }
}
