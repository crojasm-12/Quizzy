using System;
using System.Collections.Generic;


[System.Serializable]
public class SkillQuestionRespose
{
    public Boolean success;
    public String message;
    public SkillQuestion data;
}


[System.Serializable]
public class SkillQuestion
{
    public string id;
    public int gradeId;
    public int subjectId;
    public string topicId;
    public string subtopicId;
    public string skillId;
    public string type;
    public Question question;
    public Choice choice;
    public Answer answer;
    public string solution;
}

[System.Serializable]
public class Question
{
    public List<string> content;
}

[System.Serializable]
public class Choice
{
    public List<string> content;
}

[System.Serializable]
public class Answer
{
    public List<string> content;
}
