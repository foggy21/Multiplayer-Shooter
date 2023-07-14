using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviourPun
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject coin;
    [SerializeField] private JoystickController joystickController;
    [SerializeField] private ShootController shootController;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private TextMeshProUGUI moneyCount;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject winPopUp;
    [SerializeField] private TextMeshProUGUI winner;
    [SerializeField] private TextMeshProUGUI moneyCollected;
    [SerializeField] private float startCoinTimeInstantiate;
    private float currentCoinTimeInstantiate;
    private PlayerPresenter playerPresenter;
    private PlayerModel playerModel;
    private bool gameStart = false;
    private int destroyedPlayers = 0;


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
        playerView.HealthBar = healthBar;
        playerView.Enable();
        playerView.OnCoinGet += SetMoneyCount;
        playerView.OnPlayerDestroy += IncrementDestroyedPlayers;
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

        if (gameStart && PhotonNetwork.PlayerList.Length - destroyedPlayers == 1)
        {
            Debug.Log("Game End");
            gameStart = false;
            winPopUp.SetActive(true);
            winner.text = $"Winner: {PhotonNetwork.PlayerList[0].NickName}";
            moneyCollected.text = moneyCount.text;
        }
    }

    private void IncrementDestroyedPlayers()
    {
        destroyedPlayers++;
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
            gameStart = true;
        }
        return prefabObject;
    }
}
