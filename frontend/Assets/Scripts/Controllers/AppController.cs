using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AppController : MonoBehaviour
{
    public enum AppState
    {
        ReadingLevelSelection,
        Chat,
        Loading,
        StoryDisplay
    }

    // Reference to controllers
    public StoryController storyController;
    public MessageController messageController;


    // UI Views
    public GameObject readingLevelView;
    public GameObject messageView;
    public GameObject loadingView;
    public GameObject storyView;

    private void OnEnable()
    {
        // Subscribe to events
        StoryController.StoryReadingLevelUpdated += OnStoryReadingLevelUpdated;
        MessageController.ChatIsOver += OnChatIsOver;
        StoryController.StoryContentUpdated += OnStoryContentUpdated;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        StoryController.StoryReadingLevelUpdated -= OnStoryReadingLevelUpdated;
        MessageController.ChatIsOver -= OnChatIsOver;
        StoryController.StoryContentUpdated -= OnStoryContentUpdated;
    }

    void Start()
    {
        SetAppState(AppState.ReadingLevelSelection);
    }

    public void SetAppState(AppState newState)
    {
        Debug.Log($"Transitioning to state: {newState}");
        HideAllViews();
        switch (newState)
        {
            case AppState.ReadingLevelSelection:
                readingLevelView.SetActive(true);
                break;
            case AppState.Chat:
                messageView.SetActive(true);
                break;
            case AppState.Loading:
                loadingView.SetActive(true);
                break;
            case AppState.StoryDisplay:
                storyView.SetActive(true);
                break;
        }
    }

    private void HideAllViews()
    {
        readingLevelView.SetActive(false);
        messageView.SetActive(false);
        loadingView.SetActive(false);
        storyView.SetActive(false);
    }

    // Event handlers
    private void OnStoryReadingLevelUpdated(Story story)
    {
        Debug.Log($"Story reading level updated: {story.readingLevel}");
        SetAppState(AppState.Chat);
    }

    private void OnChatIsOver()
    {
        SetAppState(AppState.Loading);
    }

    private void OnStoryContentUpdated(Story story)
    {
        Debug.Log($"Story content updated: {story.content.title} - {story.content.text}");
        SetAppState(AppState.StoryDisplay);

        // Display the story in the StoryView
        StoryView storyViewComponent = storyView.GetComponent<StoryView>();
        storyViewComponent.DisplayStory(story);
    }
}
