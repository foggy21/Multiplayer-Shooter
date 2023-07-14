using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerView : MonoBehaviourPun
{
    private PhotonView view;
    private TextMeshProUGUI nickName;
    private Canvas canvas;
    private JoystickController joystickController;
    private PlayerPresenter playerPresenter;
    public PlayerPresenter PlayerPresenter { get => playerPresenter; set { if (playerPresenter == null) playerPresenter = value; } }
    public JoystickController JoystickController { get => joystickController; set { if (joystickController == null) joystickController = value; } }

    public void Move(Vector2 direction)
    {
        if (view.IsMine)
        {
            playerPresenter.Move(direction);
            canvas.transform.rotation = Quaternion.identity;
        }
    }

    public void Enable()
    {
        joystickController.OnPlayerMove += Move;
        view = GetComponent<PhotonView>();
        nickName = GetComponentInChildren<TextMeshProUGUI>();
        canvas = GetComponentInChildren<Canvas>();
        nickName.text = photonView.Owner.NickName;
    }
}
