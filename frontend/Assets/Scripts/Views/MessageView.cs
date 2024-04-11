using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageView : MonoBehaviour
{
    public GameObject messagesContent;
    public ScrollRect scrollView;
    public TMP_InputField messageInputField;
    public Button sendButton;
    public GameObject messagePrefab;

    // Events
    public static event System.Action<string> CreateMessage;
    
    void OnEnable()
    {
        MessageController.MessageReceived += AddMessageToView;
    }

    void OnDisable()
    {
        MessageController.MessageReceived -= AddMessageToView;
    }

    void Start()
    {
        sendButton.onClick.AddListener(HandleSendButtonClick);
    }

    public void AddMessageToView(Message message)
    {
        string role = message.role.ToLower() == "user" ? "You" : "AI";

        GameObject newMessage = Instantiate(messagePrefab, messagesContent.transform);
        TextMeshProUGUI roleText = newMessage.transform.Find("Role").GetComponent<TextMeshProUGUI>();
        roleText.text = role;

        TextMeshProUGUI contentText = newMessage.transform.Find("Content").GetComponent<TextMeshProUGUI>();
        contentText.text = message.content;

        ScrollToBottom();
    }

    void HandleSendButtonClick()
    {
        string messageText = messageInputField.text;
        CreateMessage?.Invoke(messageText); 
        messageInputField.text = ""; 
    }

    void ScrollToBottom()
    {
        StartCoroutine(ScrollToBottomCoroutine());
    }

    IEnumerator ScrollToBottomCoroutine()
    {
        yield return new WaitForEndOfFrame();
        scrollView.verticalNormalizedPosition = 0f; 
    }
}
