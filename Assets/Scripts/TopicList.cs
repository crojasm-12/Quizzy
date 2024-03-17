
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class TopicList : MonoBehaviour
{
    private string _parentId = null;

    public GameObject itemPrefab;
    public Transform contentPanel;

    public UnityEvent<string> onItemClicked;

    private SKILL_VIEW_LEVEL skillViewLevel = SKILL_VIEW_LEVEL.NONE;

    public SKILL_VIEW_LEVEL ViewLevel
    {
        get { return skillViewLevel; }
    }

    public string ParentId
    {
        get {  return _parentId; }
    }

    private void ClearContentPanel()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddItem(string name, string id)
    {
        GameObject newItem = Instantiate(itemPrefab, contentPanel);
        newItem.GetComponentInChildren<TextMeshProUGUI>().text = name;
        ItemTopic itemTopic = newItem.GetComponent<ItemTopic>();
        itemTopic.InitializeItem(id, name);
        itemTopic.onClick.AddListener(OnItemClick);
    }
    public void ShowTopics(string parentId, List<Topic> topics)
    {
        ClearContentPanel();
        foreach (Topic topic in topics)
        {
            AddItem(topic.name, topic.id);
        }
        skillViewLevel = SKILL_VIEW_LEVEL.TOPIC;
        _parentId = parentId;
    }

    public void ShowSubtopics(string parentId, List<Subtopic> subtopics)
    {
        ClearContentPanel();
        foreach (Subtopic subtopic in subtopics)
        {
            AddItem(subtopic.name, subtopic.id);
        }
        skillViewLevel = SKILL_VIEW_LEVEL.SUBTOPIC;
        _parentId = parentId;
    }

    public void ShowSkills(string parentId, List<Skill> skills)
    {
        ClearContentPanel();
        foreach (Skill skill in skills)
        {
            AddItem(skill.name, skill.id);
        }
        skillViewLevel = SKILL_VIEW_LEVEL.SKILL;
        _parentId = parentId;
    }

    void OnItemClick(string id)
    {
        onItemClicked.Invoke(id);
    }
}

