using Photon.Pun;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private JoystickController joystickController;
    [SerializeField] private CameraController cameraController;
    private PlayerPresenter playerPresenter;
    private PlayerModel playerModel;

    private void Awake()
    {
        playerModel = new PlayerModel();
        cameraController.Enable();
        Vector2 randomPosition = new Vector2(Random.Range(cameraController.MinX, cameraController.MaxX), Random.Range(cameraController.MinY, cameraController.MaxY));
        GameObject playerObject = PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
        cameraController.Player = playerObject;
        PlayerView playerView = playerObject.GetComponent<PlayerView>();
        playerPresenter = new PlayerPresenter(playerView, playerModel);
        playerView.PlayerPresenter = playerPresenter;
        playerView.JoystickController = joystickController;
        playerView.Enable();
    }
}
