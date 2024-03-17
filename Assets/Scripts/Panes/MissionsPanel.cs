using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SkillQuery
{
    public int gradeId;
    public int subjectId;
}

[System.Serializable]
public class SkillResponse
{
    public Boolean success;
    public String message;
    public Skills data;
}

[System.Serializable]
public class FullSkill
{
    public int gradeId;
    public int subjectId;
    public string topic;
    public string topicId;
    public string subtopic;
    public string subtopicId;
    public string skill;
    public string skillId;
}

public class MissionsPanel : MonoBehaviour
{
    private MainScene mainScene;
    private TopicList topicList; // List of topics in the mission panel
    private QuestionPanel questionPanel;

    public TextMeshProUGUI MissionTopic;
    public TextMeshProUGUI TopicTitle;
    public GameObject ButtonBack;

    public GameObject Panel;

    private TOPICS currentTopic;

    private Skills _skills = null;

    public void ShowPanel()
    {
        Panel.SetActive(true);
    }

    public void HidePanel()
    {
        Panel.SetActive(false);
    }

    public void SetTopic(TOPICS topic)
    {
        int subjectId = 1;
        currentTopic = topic;
        switch (topic)
        {
            case TOPICS.MATH:
                MissionTopic.text = "Mathematics";
                subjectId = 1;
                break;
            case TOPICS.LANGUAGE_ARTS:
                MissionTopic.text = "Language Arts";
                subjectId = 13;
                break;
            case TOPICS.SCIENCE:
                MissionTopic.text = "Science";
                subjectId = 2;
                break;
            case TOPICS.SOCIAL_STUDIES:
                MissionTopic.text = "Social Studies";
                subjectId = 5;
                break;
        }
        ReadSkills(subjectId, 3);
    }

    private void ShowButtonBack()
    {
        ButtonBack.SetActive(true);
    }
    private void HideButtonBack()
    {
        ButtonBack.SetActive(false);
    }

    private void ShowTopics()
    {
        TopicTitle.text = "Topics";
        topicList.ShowTopics(null, _skills.topics);
        HideButtonBack();
    }

    public void OnButtonBack()
    {
        if (topicList.ParentId == null)
        {
            ShowTopics();
            return;
        }
        var (childObject, parentObject) = FindById(_skills, topicList.ParentId);
        switch (childObject)
        {
            case Topic topic:
                ShowTopics();
                break;
            case Subtopic subtopic:
                Topic topicParent = parentObject as Topic;

                TopicTitle.text = topicParent.name;
                topicList.ShowSubtopics(topicParent.id, topicParent.subtopics);
                ShowButtonBack();
                break;
        }

    }

    void Awake()
    {
        mainScene = FindFirstObjectByType<MainScene>();
        topicList = FindFirstObjectByType<TopicList>();
        questionPanel = FindFirstObjectByType<QuestionPanel>();
        if (mainScene == null || topicList == null)
        {
            Debug.LogError("MainFrame or TopiList instances not found in MissionsPanel!");
            return;
        }
        topicList.onItemClicked.AddListener(OnItemClicked);
    }

    private void OnItemClicked(string guid)
    {
        var (childObject, parentObject) = FindById(_skills, guid);
        switch (childObject)
        {
            case Topic topic:
                TopicTitle.text = topic.name;
                topicList.ShowSubtopics(topic.id, topic.subtopics);
                ShowButtonBack();
                break;
            case Subtopic subtopic:
                Topic topicParent = parentObject as Topic;
                TopicTitle.text = topicParent.name + " > " + subtopic.name;
                topicList.ShowSkills(subtopic.id, subtopic.skills);
                ShowButtonBack();
                break;
            case Skill skill:
                questionPanel.ShowPanel(FullSkillById(_skills, skill.id));
                break;
        }
    }

    public void OnBack()
    {
        HidePanel();
        mainScene.ShowPanel();
    }

    private void ReadSkills(int subjectId, int gradeId)
    {
        SkillQuery skillQuery = new SkillQuery();
        skillQuery.subjectId = subjectId;
        skillQuery.gradeId = gradeId;

        HttpService httpService;

        httpService = gameObject.AddComponent<HttpService>();

        string jsonPayload = JsonUtility.ToJson(skillQuery);

        Dictionary<string, string> headers = new Dictionary<string, string>();
        UserData userData = DataStorage.Instance.GetData<UserData>("UserData");

        headers.Add("Authorization", $"Bearer {userData.authToken}");

        StartCoroutine(httpService.Post("/api/skill/v1/skills", headers, jsonPayload,
            (response) =>
            {
                try
                {
                    SkillResponse skillResponse = JsonUtility.FromJson<SkillResponse>(response);
                    _skills = skillResponse.data;
                    if (_skills != null)
                    {
                        ShowTopics();
                    }
                }
                catch (Exception e)
                {
                    _skills = null;
                    Debug.Log(e.ToString());
                }
            },
            (error) =>
            {
                _skills = null;
                Debug.LogError($"Error reading: ${error}");
            }
        ));
    }

    public (object child, object parent) FindById(Skills skills, string id)
    {
        foreach (var topic in skills.topics)
        {
            if (topic.id == id)
            {
                return (topic, skills);
            }
            foreach (var subtopic in topic.subtopics)
            {
                if (subtopic.id == id)
                {
                    return (subtopic, topic);
                }
                foreach (var skill in subtopic.skills)
                {
                    if (skill.id == id)
                    {
                        return (skill, subtopic);
                    }
                }
            }
        }
        return (null, null);
    }


    public FullSkill FullSkillById(Skills skills, string id)
    {
        foreach (var topic in skills.topics)
        {
            foreach (var subtopic in topic.subtopics)
            {
                foreach (var skill in subtopic.skills)
                {
                    if (skill.id == id)
                    {

                        return new FullSkill
                        {
                            gradeId = skills.gradeId,
                            subjectId = skills.subjectId,
                            topic = topic.name,
                            topicId = topic.id,
                            subtopic = subtopic.name,
                            subtopicId = subtopic.id,
                            skill = skill.name,
                            skillId = skill.id
                        };
                    }
                }
            }
        }
        return null;
    }


}
