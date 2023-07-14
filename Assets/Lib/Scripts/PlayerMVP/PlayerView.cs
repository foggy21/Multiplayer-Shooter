using UnityEngine;
using Photon.Pun;
using TMPro;
using System;
using UnityEngine.UI;

public class PlayerView : MonoBehaviourPun
{
    
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireStartDistance;
    private PhotonView view;
    private TextMeshProUGUI nickName;
    private Canvas canvas;
    private Image healthBar;
    private JoystickController joystickController;
    private ShootController shootController;
    private PlayerPresenter playerPresenter;

    public Action OnPlayerDestroy;
    public Action OnCoinGet;
    public Action<float> OnBulletCollide;
    public Image HealthBar { get => healthBar; set { if (healthBar == null) healthBar = value; } }
    public PlayerPresenter PlayerPresenter { get => playerPresenter; set { if (playerPresenter == null) playerPresenter = value; } }
    public JoystickController JoystickController { get => joystickController; set { if (joystickController == null) joystickController = value; } }
    public ShootController ShootController { get => shootController; set { if (shootController == null) shootController = value; } }

    public void Move(Vector2 direction)
    {
        if (view.IsMine)
        {
            playerPresenter.Move(direction);
            canvas.transform.rotation = Quaternion.identity;
        }
    }

    public void DisplayNewHealthBar(float currentHealth, float maxHealth)
    {
        if (view.IsMine)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    public void InstantiateBullet()
    {
        GameObject bulletObject = PhotonNetwork.Instantiate(bullet.name, transform.position, transform.rotation);
        Physics2D.IgnoreCollision(bulletObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void Enable()
    {
        joystickController.OnPlayerMove += Move;
        shootController.OnShoot += InstantiateBullet;
        view = GetComponent<PhotonView>();
        nickName = GetComponentInChildren<TextMeshProUGUI>();
        canvas = GetComponentInChildren<Canvas>();
        nickName.text = photonView.Owner.NickName;
    }

    public void Disable()
    {
        joystickController.OnPlayerMove -= Move;
        shootController.OnShoot -= InstantiateBullet;
        OnPlayerDestroy.Invoke();
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coin>() != null)
        {
            OnCoinGet.Invoke();
        }
    }
}
