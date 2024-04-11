// MessageController.cs
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MessageController : MonoBehaviour
{
    public StoryController storyController;
    public MessageService messageService;

    public static event Action<Message> MessageReceived;
    public static event Action ChatIsOver;

    private void OnEnable()
    {
        MessageView.CreateMessage += CreateUserMessage;
    }

    private void OnDisable()
    {
        MessageView.CreateMessage -= CreateUserMessage;
    }

    private void CreateUserMessage(string content)
    {
        string storyId = storyController.activeStory._id;
        string jsonPayload = messageService.ConstructContentPayload(content);
        StartCoroutine(messageService.CreateUserMessageRequest(storyId, jsonPayload, OnUserMessageReceived, OnErrorReceived));
    }

    private void CreateChatbotMessage(string storyId)
    {
        StartCoroutine(messageService.CreateChatbotMessageRequest(storyId, OnChatbotMessageReceived, OnErrorReceived));
    }

    // Adjusted to emit event after processing the message
    public void OnUserMessageReceived(Message message)
    {
        storyController.activeStory.messages.Add(message._id);
        Debug.Log("User message processed, emitting event...");
        MessageReceived?.Invoke(message);
        CreateChatbotMessage(storyController.activeStory._id);
    }

    // Adjusted to emit event after processing the message and chat over status
    public void OnChatbotMessageReceived(Message message, bool chatOver)
    {
        storyController.activeStory.messages.Add(message._id);
        Debug.Log("Chatbot message processed, emitting event...");
        MessageReceived?.Invoke(message); 
        if (chatOver)
        {
            Debug.Log("Chat over, emitting event...");
            ChatIsOver?.Invoke();
        }
        else
        {
            Debug.Log("Chat is not over.");
        }
    }


    public void OnErrorReceived(string error)
    {
        Debug.LogError($"Error: {error}");
    }
}
