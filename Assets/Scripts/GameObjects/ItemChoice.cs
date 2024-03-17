using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class ItemChoice : MonoBehaviour, IPointerClickHandler
{
    public string guid;
    public UnityEvent<string> onClick;

    public void InitializeItem(string guidValue, string displayText)
    {
        guid = guidValue;
        GetComponentInChildren<TextMeshProUGUI>().text = displayText; 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke(guid);
    }
}
