using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{
    public GameObject PlayerSphere, BallPrefab, SpawnedObject;
    [HideInInspector]
    public GameObject Ball;
    public GameObject Camera;
    [HideInInspector]
    public bool Ball_is_attached = false;
    private PhotonView PV;
    private BallController ball;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
            Camera.SetActive(true);
        else
            Camera.SetActive(false);

        if (Ball == null)
            Ball = GameObject.Find("Ball");

        ball = Ball.GetComponent<BallController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetKey(KeyCode.D))
                this.transform.Translate(Vector3.right * Time.deltaTime * 3f);
            else if (Input.GetKey(KeyCode.W))
                this.transform.Translate(Vector3.forward * Time.deltaTime * 3f);
            else if (Input.GetKey(KeyCode.A))
                this.transform.Translate(Vector3.left * Time.deltaTime * 3f);
            else if (Input.GetKey(KeyCode.S))
                this.transform.Translate(Vector3.back * Time.deltaTime * 3f);
            else if (Input.GetKeyDown(KeyCode.I))
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player Sphere"), this.gameObject.transform.position + PlayerSphere.transform.position, Quaternion.identity);
            else if (Input.GetKeyDown(KeyCode.P))
            {
                if (Ball_is_attached == false)
                    ball.photonView.RequestOwnership();

                PV.RPC("GrabTheBall", RpcTarget.AllBuffered);
            }
        }

        if (Ball_is_attached == true)
            Ball.transform.position = this.gameObject.transform.localPosition + new Vector3(0, 0.5f, 0) + Vector3.forward;
    }

    [PunRPC]
    public void GrabTheBall()
    {
        if (Ball_is_attached == false && ball.BallIsAttached == false)
        {
            ball.photonView.TransferOwnership(ball.Requesting_Player);
            ball.transform.position = this.gameObject.transform.position + new Vector3(0, 0.5f, 0) + Vector3.forward;
            Ball_is_attached = true;
            ball.BallIsAttached = true;
            PV.RPC("BallIsAttached", RpcTarget.AllBuffered, Ball_is_attached, ball.BallIsAttached);
        }
        else if (Ball_is_attached == true && ball.BallIsAttached == true)
        {
            ball.photonView.TransferOwnership(ball.Photon_View_Id);
            Ball_is_attached = false;
            ball.BallIsAttached = false;
            PV.RPC("BallIsAttached", RpcTarget.AllBuffered, Ball_is_attached, ball.BallIsAttached);
        }
    }

    [PunRPC]
    public void BallIsAttached(bool value1, bool value2)
    {
        Ball_is_attached = value1;
        ball.BallIsAttached = value2;
    }
}
