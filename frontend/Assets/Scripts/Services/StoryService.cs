using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Networking;

public class StoryService : MonoBehaviour
{
    private readonly string baseUrl = "https://story-cocreation-8a5410ee7c19.herokuapp.com"; 

    public IEnumerator CreateStoryRequest(string endpoint, Action<Story> onStoryReceived, Action<string> onErrorReceived)
    {

        string url = $"{baseUrl}/{endpoint}";
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;
            Story story = ProcessStoryResponse(responseJson);
            onStoryReceived?.Invoke(story);
        }
        else
        {
            onErrorReceived?.Invoke(request.error);
        }
    }

     public IEnumerator UpdateStoryRequest(string endpoint, string storyId, string jsonPayload, Action<Story> onStoryProcessed, Action<string> onErrorReceived)
    {
        string url = $"{baseUrl}/story/{storyId}/{endpoint}";
        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        request.downloadHandler = new DownloadHandlerBuffer();

        // Add the JSON payload to the request
        byte[] body = string.IsNullOrEmpty(jsonPayload) ? null : System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        if (body != null)
        {
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(body);
            request.SetRequestHeader("Content-Type", "application/json");
        }
        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;
            Story story = ProcessStoryResponse(responseJson); // Assume this method exists and parses the JSON to a Story object
            onStoryProcessed?.Invoke(story);
        }
        else
        {
            onErrorReceived?.Invoke(request.error);
        }
    }


    
    private Story ProcessStoryResponse(string responseJson)
    {
        try
        {
            Debug.Log($"Story Response: {responseJson}");
            StoryWrapper storyWrapper = JsonUtility.FromJson<StoryWrapper>(responseJson);
            Debug.Log($"Story received: {storyWrapper.story._id}");
            return storyWrapper.story;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error processing story response: {ex.Message}");
            return null;
        }
    }

    public string ConstructReadingLevelPayload(string readingLevel)
    {
        ReadingLevelPayload payload = new ReadingLevelPayload { readingLevel = readingLevel };
        return JsonUtility.ToJson(payload);
    }

    [System.Serializable]
    public class ReadingLevelPayload
    {
        public string readingLevel;
    }
}
