using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    [SerializeField]
    private short Port = 4444;
    [SerializeField]
    private string Address = "127.0.0.1";

    private NetworkClient myClient;

    private void Start()
    {
        SetupServer();
        SetupClient();
    }

    private void SetupServer()
    {
        NetworkServer.Listen(Port);
    }

    private void SetupClient()
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect(Address, Port);
    }

    private void SetupLocalClient()
    {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
    }

    private void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("OnConnected " + NetworkServer.active);
        SceneManager.LoadScene("MainMenu");
    }
}