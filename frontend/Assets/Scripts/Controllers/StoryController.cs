
using System;
using System.Collections;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    public StoryList storyList = new StoryList();
    public Story activeStory;
    public StoryService storyService;

    public static event Action<Story> StoryReadingLevelUpdated;
    public static event Action<Story> StoryContentUpdated;

    private void OnEnable()
    {
        ReadingLevelView.ReadingLevelSelected += HandleReadingLevelSelected;
        MessageController.ChatIsOver += UpdateStoryContentAndSummary;
    }

    private void OnDisable()
    {
        ReadingLevelView.ReadingLevelSelected -= HandleReadingLevelSelected;
        MessageController.ChatIsOver -= UpdateStoryContentAndSummary;
    }

    private void HandleReadingLevelSelected(string readingLevel)
    {
        StartCoroutine(CreateAndUpdateStoryWithReadingLevel(readingLevel));
    }

    private IEnumerator CreateAndUpdateStoryWithReadingLevel(string readingLevel)
    {
        yield return StartCoroutine(storyService.CreateStoryRequest("story", OnStoryReceived, OnErrorReceived));

        if (activeStory != null)
        {
            string jsonPayload = storyService.ConstructReadingLevelPayload(readingLevel);
            Debug.Log($"JSON Payload: {jsonPayload}");
            yield return StartCoroutine(storyService.UpdateStoryRequest("reading-level", activeStory._id, jsonPayload, OnStoryWithReadingLevelReceived, OnErrorReceived));
            StoryReadingLevelUpdated?.Invoke(activeStory);
        }
    }

    private void UpdateStoryContentAndSummary()
    {
        StartCoroutine(UpdateStoryContentAndSummaryCoroutine());
    }

    private IEnumerator UpdateStoryContentAndSummaryCoroutine()
    {
        string storyId = activeStory._id;
        yield return StartCoroutine(storyService.UpdateStoryRequest("summary", storyId, null, OnStoryWithSummaryReceived, OnErrorReceived));

        yield return StartCoroutine(storyService.UpdateStoryRequest("content", storyId, null, OnStoryWithContentReceived, OnErrorReceived));

        StoryContentUpdated?.Invoke(activeStory);
    }

    private void OnStoryReceived(Story story)
    {
        Debug.Log($"Story created with ID: {story._id}");
        storyList.stories.Add(story);
        activeStory = story;
    }

    private void OnStoryWithReadingLevelReceived(Story storyWithReadingLevel)
    {
        activeStory.readingLevel = storyWithReadingLevel.readingLevel;
    }

    private void OnStoryWithSummaryReceived(Story storyWithSummary)
    {
        activeStory.summary = storyWithSummary.summary;
    }

    private void OnStoryWithContentReceived(Story storyWithContent)
    {
        activeStory.content = storyWithContent.content;
    }

    public void OnErrorReceived(string error)
    {
        Debug.LogError($"An error occurred: {error}");
    }
}

