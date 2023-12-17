using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine.UI;

public class Chat : MonoBehaviour , IChatClientListener
{
    public string userName;
    public ChatClient ChatClient;

    public InputField InputField;
    public Text ChatContent;

    #region Chat-Methods
    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("Chat - " + level + " - " +  message);    
        //throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("Chat - OnStateChange" +  state);
       // throw new System.NotImplementedException();
    }

    public void OnConnected()
    {
        Debug.Log("Chat - user: " + userName + " has connected ");
        ChatClient.Subscribe(PhotonNetwork.CurrentRoom.Name, creationOptions: new ChannelCreationOptions() { PublishSubscribers = true });
        //throw new System.NotImplementedException();
    }

    public void OnDisconnected()
    {
        Debug.Log("Chat - user:" + userName + "has disconnected");
        //throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        ChatChannel currentChat;
        if (ChatClient.TryGetChannel(PhotonNetwork.CurrentRoom.Name,out currentChat))
        {
            ChatContent.text = currentChat.ToStringMessages();
        }
       // throw new System.NotImplementedException();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i=0; i< channels.Length; i++)
        {
            if (results[i])
            {
                Debug.Log("Chat - Subscribed to " + channels[i] + "channel");
                ChatClient.PublishMessage(PhotonNetwork.CurrentRoom.Name, "has joined the chat");
            }
        }
        //throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        //throw new System.NotImplementedException();
    }
    #endregion
    void Start()
    {
        ChatClient= new ChatClient(this);
    }
    void Update()
    {
        ChatClient.Service();
    }
    public void SetMessage()
    {
        if (InputField.text == "")
            return;

        ChatClient.PublishMessage(PhotonNetwork.CurrentRoom.Name, InputField.text);
        InputField.text = "";
    }
}
