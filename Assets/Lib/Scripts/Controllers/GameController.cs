using Photon.Pun;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private JoystickController joystickController;
    private PlayerPresenter playerPresenter;
    private PlayerModel playerModel;

    private void Start()
    {
        playerModel = new PlayerModel();
        GameObject playerObject = PhotonNetwork.Instantiate(player.name, Vector2.zero, Quaternion.identity);
        PlayerView playerView = playerObject.GetComponent<PlayerView>();
        playerPresenter = new PlayerPresenter(playerView, playerModel);
        playerView.PlayerPresenter = playerPresenter;
        playerView.JoystickController = joystickController;
        playerView.Enable();
    }
}
