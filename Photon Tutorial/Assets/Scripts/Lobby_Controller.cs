using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Lobby_Controller : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private GameObject CancelButton;
    [SerializeField]
    private int roomSize;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        StartButton.SetActive(true);
    }

    public void DelayStart()
    {
        StartButton.SetActive(false);
        CancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Joined to a random room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Creating Room");
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom();
    }

    public void DelayCancel()
    {
        CancelButton.SetActive(false);
        StartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
