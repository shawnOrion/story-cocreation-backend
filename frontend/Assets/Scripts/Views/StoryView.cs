using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryView : MonoBehaviour
{
    public TextMeshProUGUI storyTitle;
    public TextMeshProUGUI storyText;

    public void DisplayStory(Story story)
    {
        Debug.Log($"Displaying story: {story.content.title}, {story.content.text}");
        storyTitle.text = story.content.title;
        storyText.text = story.content.text;
    }
}
