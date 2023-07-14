using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class PopupController : MonoBehaviourPunCallbacks
{
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
