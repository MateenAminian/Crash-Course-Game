using UnityEngine;
using Mirror;
using Steamworks;

public class MyNetworkManager : NetworkManager
{
    [SerializeField] private string notificationMessage = string.Empty;

    public override void OnStartServer()
    {
        ServerChangeScene("Henry Level Design");
    }

    [ContextMenu("Send Notification")]
    private void SendNotification()
    {
        //NetworkServer.SendToAll(new Notification { content = notificationMessage });
    }
}
