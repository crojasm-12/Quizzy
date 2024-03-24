using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class QuestionPanel : MonoBehaviour
{
    private SkillQuestion _skillQuestion;
    private FullSkill _skill;

    private AnswerChoices answerChoices; // List of topics in the mission panel

    private ToastPanel toastPanel;
    public GameObject Panel;
    public GameObject particlesEffect;

    public TextMeshProUGUI Question;
    public TextMeshProUGUI AnswerFeedback;

    // Reward Panel
    public TextMeshProUGUI TotalScore;
    public TextMeshProUGUI TotalReward;
    public TextMeshProUGUI ElapsedTime;

    // Sounds
    public TadaaAudio tadaaAudio;
    public FailAudio failAudio;


    private float _delay = 3f;
    private float _elapsedTime = 0f;
    private float _totalReward = 0f;
    private int _totalQuestions = 0;
    private int _totalCorrect = 0;
    private bool _activeQuestion = false;

    private float _questionValue = 0.15f;

    private int _answerTries;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        int hours = (int)(_elapsedTime / 3600);
        int minutes = (int)((_elapsedTime % 3600) / 60);
        int seconds = (int)(_elapsedTime % 60);
        ElapsedTime.text = string.Format("{0:D2}h {1:D2}m {2:D2}s", hours, minutes, seconds);
        TotalScore.text = string.Format("{0:D3} / {1:D3}", _totalCorrect, _totalQuestions);
        TotalReward.text = _totalReward.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
    }

    public FullSkill Skill
    {
        get { return _skill; }
    }

    public void ShowPanel(FullSkill skill)
    {
        _skill = skill;

        _elapsedTime = 0f;
        _totalReward = 0f;
        _totalQuestions = 0;
        _totalCorrect = 0;
        _activeQuestion = false;

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

    private void OnSelectedChoice(string answer)
    {
        if (_skillQuestion != null &&
            _skillQuestion.answer != null &&
            _skillQuestion.answer.content.Count > 0)
        {
            if (_skillQuestion.answer.content[0] == answer)
            {
                if (_answerTries > 0 && _activeQuestion)
                {
                    // Only add points if the question is active.
                    // Question is active if has not been answered.
                    _totalCorrect++;
                    _totalReward += (_questionValue / _answerTries);
                }
                AnswerFeedback.text = "Correct!!!";
                AnswerFeedback.color = Color.green;
                particlesEffect.SetActive(true);
                tadaaAudio.PlayAudio();
                _activeQuestion = false; // Question has been ansered, disable question.
            }
            else
            {
                _answerTries++;
                failAudio.PlayAudio();
                AnswerFeedback.text = "Wrong!!!";
                AnswerFeedback.color = Color.red;
            }
            AnswerFeedback.gameObject.SetActive(true);
            StartCoroutine(HideAnswerFeedback(_delay));
        }
    }

    private IEnumerator HideAnswerFeedback(float delay)
    {
        yield return new WaitForSeconds(delay);
        AnswerFeedback.gameObject.SetActive(false);
        particlesEffect.SetActive(false);
        failAudio.StopAudio();
        tadaaAudio.StopAudio();
    }
    private void ShowQuestion()
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
                    Question.text = _skillQuestion?.question?.content[0];
                    if (_skillQuestion.type == "MultipleChoice")
                    {
                        answerChoices.ShowChoices(_skillQuestion.choice.content);
                    }
                    _answerTries = 1;
                    _activeQuestion = true;
                    _totalQuestions++;
                }
                catch (Exception e)
                {
                    _skillQuestion = null;
                    _answerTries = 0;
                    _activeQuestion = false;
                    toastPanel.ShowError(e.ToString());
                    Debug.Log(e.ToString());
                }
            },
            (error) =>
            {
                _skillQuestion = null;
                _answerTries = 0;
                _activeQuestion = false;
                Debug.LogError($"Error reading skillQuestion: ${error}");
            }
        ));
    }


    void Awake()
    {
        toastPanel = FindFirstObjectByType<ToastPanel>();
        answerChoices = FindFirstObjectByType<AnswerChoices>();

        tadaaAudio = FindFirstObjectByType<TadaaAudio>();
        failAudio = FindFirstObjectByType<FailAudio>();

        answerChoices.onChoiceClicked.AddListener(OnSelectedChoice);
    }

}
