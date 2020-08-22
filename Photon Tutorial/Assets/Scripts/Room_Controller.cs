using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Room_Controller : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int waitingRoomSceneIndex;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this); // register to Photon Callback functions
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this); // unregister to Photon Callback functions
    }

    public override void OnJoinedRoom() // Callback function when we successfully create or join a room
    {
        SceneManager.LoadScene(waitingRoomSceneIndex); // called when our player joins the room. This will load into waiting room scene
    }
}
