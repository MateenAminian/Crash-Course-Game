using Steamworks;
using UnityEngine;
using Mirror;
using TMPro;

public class Notification : NetworkMessage
{
    public string content;
}

public class MessagesTest : MonoBehaviour
{
    /*[SerializeField] private TMP_Text notificationsText = null;

    private void Start()
    {
        if (!NetworkClient.active) { return; }

        NetworkClient.RegisterHandler<Notification>(OnNotification);
    }

    private void OnNotification(NetworkConnection conn, Notification msg)
    {
        notificationsText.text += $"\n{msg.content}";
    }*/
}
