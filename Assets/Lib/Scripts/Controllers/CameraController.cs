using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private Camera mainCamera;
    private float minX, minY, maxX, maxY;
    public float MinX { get => minX; }
    public float MaxX { get => maxX; }
    public float MinY { get => minY; }
    public float MaxY { get => maxY; }
    public GameObject Player { get => player; set { if (player == null) player = value; } } 
    public void Enable()
    {
        mainCamera = Camera.main;

        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        minX = mainCamera.transform.position.x - cameraWidth / 2f;
        maxX = mainCamera.transform.position.x + cameraWidth / 2f;
        minY = mainCamera.transform.position.y - cameraHeight / 2f;
        maxY = mainCamera.transform.position.y + cameraHeight / 2f;
    }


    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 clampedPosition = player.transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
            player.transform.position = clampedPosition;
        }
    }
}
