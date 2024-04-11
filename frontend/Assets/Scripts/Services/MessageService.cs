using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MessageService : MonoBehaviour
{
    private readonly string baseUrl = "https://story-cocreation-8a5410ee7c19.herokuapp.com";
    public IEnumerator CreateUserMessageRequest(string storyId, string jsonPayload, Action<Message> onUserMessageReceived, Action<string> onFailure)
    {
        string url = $"{baseUrl}/story/{storyId}/user-message";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(body);
        request.SetRequestHeader("Content-Type", "application/json");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Message message = ProcessUserMessageResponse(request.downloadHandler.text);
            if (message != null) onUserMessageReceived?.Invoke(message);
        }
        else
        {
            onFailure?.Invoke(request.error);
        }
    }

    public IEnumerator CreateChatbotMessageRequest(string storyId, Action<Message, bool> onChatbotMessageReceived, Action<string> onFailure)
    {
        string url = $"{baseUrl}/story/{storyId}/chatbot-message";
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            ChatbotMessageResponse response = ProcessChatbotMessageResponse(request.downloadHandler.text);
            if (response != null && response.message != null) onChatbotMessageReceived?.Invoke(response.message, response.chatOver);
        }
        else
        {
            onFailure?.Invoke(request.error);
        }
    }

    public string ConstructContentPayload(string content)
    {
        ContentPayload payload = new ContentPayload { content = content };
        return JsonUtility.ToJson(payload);
    }

    private Message ProcessUserMessageResponse(string responseText)
    {
        try
        {
            Debug.Log(responseText);
            UserMessageResponse userMessageResponse = JsonUtility.FromJson<UserMessageResponse>(responseText);
            return userMessageResponse.message;
        }
        catch (Exception e)
        {
            Debug.LogError("Error processing message response: " + e.Message);
            return null;
        }
    }

    private ChatbotMessageResponse ProcessChatbotMessageResponse(string responseText)
    {
        try
        {
            Debug.Log(responseText);
            ChatbotMessageResponse chatbotMessageResponse = JsonUtility.FromJson<ChatbotMessageResponse>(responseText);
            return chatbotMessageResponse;
        }
        catch (Exception e)
        {
            Debug.LogError("Error processing chatbot message response: " + e.Message);
            return null;
        }
    }

    [System.Serializable]
    public class ContentPayload
    {
        public string content;
    }
    [System.Serializable]
    public class UserMessageResponse
    {
        public Message message;
    }

    [System.Serializable]
    public class ChatbotMessageResponse
    {
        public Message message;
        public bool chatOver;

    }
}
