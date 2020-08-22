using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BallController : MonoBehaviourPun, IPunOwnershipCallbacks
{
    private PhotonView PV;
    [HideInInspector]
    public bool BallIsAttached = false;
    [HideInInspector]
    public Player Requesting_Player;
    [HideInInspector]
    public int Photon_View_Id;

    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        Photon_View_Id = PV.ViewID;
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != base.photonView)
            return;

        Requesting_Player = requestingPlayer;
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        if (targetView != base.photonView)
            return;
    }
}
