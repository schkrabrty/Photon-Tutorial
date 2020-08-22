using UnityEngine;
using Photon.Pun;

public class PlayerSphereController : MonoBehaviourPun
{
    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV != base.photonView)
            return;

        if (Input.GetMouseButtonDown(0) && PV.IsMine)
            this.transform.Translate(Vector3.forward * Time.deltaTime * 20f);
    }
}
