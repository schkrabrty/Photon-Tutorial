using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameSetupManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer(); // Create a networked player object for each player that loads into the multiplayer game
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }
}
