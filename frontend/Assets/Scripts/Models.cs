
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    public string _id;
    public string role;
    public string content;
}
// MessageList
[System.Serializable]
public class MessageList
{
    public List<Message> messages;
    // initialize the list
    public MessageList()
    {
        messages = new List<Message>();
    }
}

[System.Serializable]
public class Story
{
    public string _id;
    public string readingLevel = null;
    public List<string> messages = null;
    public Summary summary = null;
    public Content content = null;
}

[System.Serializable]
public class Summary
{
    public string theme = null;
    public string character = null;
    public string setting = null;
}

[System.Serializable]
public class Content
{
    public string title = null;
    public string text = null;
}


[System.Serializable]
public class StoryWrapper
{
    public Story story;
}

[System.Serializable]
public class StoryList
{
    public List<Story> stories;
    // initialize the list
    public StoryList()
    {
        stories = new List<Story>();
    }
}
