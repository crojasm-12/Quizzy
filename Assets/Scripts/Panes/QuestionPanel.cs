using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionPanel : MonoBehaviour
{
    private SkillQuestion _skillQuestion;
    private FullSkill _skill;

    private ToastPanel toastPanel;
    public GameObject Panel;

    public TextMeshProUGUI Question;

    public FullSkill Skill
    {
        get { return _skill; }
    }

    public void ShowPanel(FullSkill skill)
    {
        _skill = skill;
        Panel.SetActive(true);
        ShowQuestion();
    }

    public void HidePanel()
    {
        Panel.SetActive(false);
    }

    public void OnClaim()
    {
        HidePanel();
    }

    public void OnSubmit()
    {


        ShowQuestion();
    }

    private void ShowQuestion()
    {
        if (GetNextQuestion())
        {
            Question.text = _skillQuestion.question.content[0];
        }
    }

    private bool GetNextQuestion()
    {
        if (_skill != null)
        {

            _skillQuestion = null;
            int retries = 4;
            while (retries > 0)
            {
                GetSkills();
                if (_skillQuestion != null)
                    break;
                retries--;
            }
            if (_skillQuestion == null)
            {
                toastPanel.ShowError("System Error. Go Back and Start Again");
                return false;
            }
            return true;
        }
        else
        {
            toastPanel.ShowError("Skill Not Defined. Go Back and Start Again");
            return false;
        }
    }

    private void GetSkills()
    {
        HttpService httpService;

        httpService = gameObject.AddComponent<HttpService>();
        string jsonPayload = JsonUtility.ToJson(_skill);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        UserData userData = DataStorage.Instance.GetData<UserData>("UserData");

        headers.Add("Authorization", $"Bearer {userData.authToken}");
        StartCoroutine(httpService.Post("/api/skill/v1/question", headers, jsonPayload,
            (response) =>
            {
                try
                {
                    SkillQuestionRespose skillQuestionResponse = JsonUtility.FromJson<SkillQuestionRespose>(response);
                    _skillQuestion = skillQuestionResponse.data;
                }
                catch (Exception e)
                {
                    _skillQuestion = null;
                    toastPanel.ShowError(e.ToString());
                    Debug.Log(e.ToString());
                }
            },
            (error) =>
            {
                _skillQuestion = null;
                Debug.LogError($"Error reading skillQuestion: ${error}");
            }
        ));
    }


    void Awake()
    {
        toastPanel = FindFirstObjectByType<ToastPanel>();
    }

}
