using Photon.Pun;
using UnityEngine;

public class Coin : MonoBehaviourPun
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerView>() != null)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
