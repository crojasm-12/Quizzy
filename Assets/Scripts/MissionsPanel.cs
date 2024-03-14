using TMPro;
using UnityEngine;

public class MissionsPanel : MonoBehaviour
{
    private MainScene mainScene;
    private TopicList topicList; // List of topics in the mission panel

    public TextMeshProUGUI MissionTopic;

    public GameObject Panel;

    private TOPICS currentTopic;

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
        currentTopic = topic;
        switch(topic)
        {
            case TOPICS.MATH:
                MissionTopic.text = "Mathematics";
                break;
            case TOPICS.LANGUAGE_ARTS:
                MissionTopic.text = "Language Arts";
                break;
            case TOPICS.SCIENCE:
                MissionTopic.text = "Science";
                break;
            case TOPICS.SOCIAL_STUDIES:
                MissionTopic.text = "Social Studies";
                break;
        }
    }

    void Awake() // It's better to use Awake to ensure that references are set before any Start methods call them
    {
        mainScene = FindFirstObjectByType<MainScene>();
        topicList = FindFirstObjectByType<TopicList>();
        if (mainScene == null || topicList == null)
        {
            Debug.LogError("MainFrame or TopiList instances not found in MissionsPanel!");
        }
    }


    public void OnBack()
    {
        HidePanel();
        mainScene.ShowPanel();
    }
}
