using UnityEngine;
using Photon.Pun;

public class Network_Controller : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are connected to the " + PhotonNetwork.CloudRegion + " server!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
