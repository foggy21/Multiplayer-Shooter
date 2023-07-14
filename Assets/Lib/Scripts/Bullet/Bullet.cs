using Photon.Pun;
using System;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    private CameraController cameraController;
    public float Damage { get => damage; }
    private void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        
        if (cameraController.MaxX < transform.position.x || cameraController.MinX > transform.position.x ||
            cameraController.MaxY < transform.position.y || cameraController.MinY > transform.position.y)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        transform.Translate(speed * Vector2.right * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerView>() != null)
        {
            collision.GetComponent<PlayerView>().OnBulletCollide.Invoke(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerView>() != null)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
