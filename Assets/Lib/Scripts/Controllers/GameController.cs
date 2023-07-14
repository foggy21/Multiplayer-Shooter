using Photon.Pun;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviourPun
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject coin;
    [SerializeField] private JoystickController joystickController;
    [SerializeField] private ShootController shootController;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private TextMeshProUGUI moneyCount;
    [SerializeField] private float startCoinTimeInstantiate;
    private float currentCoinTimeInstantiate;
    private PlayerPresenter playerPresenter;
    private PlayerModel playerModel;
    private bool gameStart = false;
    private bool gameEnd = false;

    private void Awake()
    {
        playerModel = new PlayerModel();
        cameraController.Enable();
        GameObject playerObject = InstantiatePrefab(player.name);
        PlayerView playerView = playerObject.GetComponent<PlayerView>();
        playerPresenter = new PlayerPresenter(playerView, playerModel);
        cameraController.Player = playerObject;
        playerView.PlayerPresenter = playerPresenter;
        playerView.JoystickController = joystickController;
        playerView.ShootController = shootController;
        playerView.Enable();
        playerView.OnCoinGet += SetMoneyCount;
    }

    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length > 1)
        {
            if (currentCoinTimeInstantiate <= 0)
            {
                InstantiatePrefab(coin.name);
                currentCoinTimeInstantiate = startCoinTimeInstantiate;
            }
            else
            {
                currentCoinTimeInstantiate -= Time.deltaTime;
            }
        }

        if (gameStart && PhotonNetwork.PlayerList.Length < 2)
        {
            Debug.Log("Game End");
            gameEnd = true;
        }
    }

    private void SetMoneyCount()
    {
        moneyCount.text = $"Money: {playerModel.CouinsCount}";
    }

    private GameObject InstantiatePrefab(string prefabName)
    {
        Vector2 randomPosition = new Vector2(Random.Range(cameraController.MinX, cameraController.MaxX), Random.Range(cameraController.MinY, cameraController.MaxY));
        GameObject prefabObject = PhotonNetwork.Instantiate(prefabName, randomPosition, Quaternion.identity);
        if (PhotonNetwork.PlayerList.Length > 1)
        {
            Debug.Log("Game Start");
            gameStart = true;
        }
        return prefabObject;
    }
}
