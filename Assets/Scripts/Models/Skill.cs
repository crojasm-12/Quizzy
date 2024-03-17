using System;
using System.Collections.Generic;


[System.Serializable]
public class Skills
{
    public string _id;
    public int subjectId;
    public int gradeId;
    public List<Topic> topics;
    public string description;
    public string gradeName;
    public string subjectName;
    public bool normalized;
}

[System.Serializable]
public class Topic
{
    public string id;
    public string name;
    public List<Subtopic> subtopics;
}

[System.Serializable]
public class Subtopic
{
    public string id;
    public string name;
    public List<Skill> skills;
}

[System.Serializable]
public class Skill
{
    public string id;
    public string name;
}

