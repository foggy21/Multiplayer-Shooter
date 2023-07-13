using UnityEngine;

public class PlayerPresenter 
{
    private PlayerView playerView;
    private PlayerModel playerModel;
    private Canvas canvas;

    public PlayerPresenter(PlayerView playerView, PlayerModel playerModel)
    {
        this.playerView = playerView;
        this.playerModel = playerModel;
        canvas = playerView.GetComponentInChildren<Canvas>();
    }

    public void Move(Vector2 direction)
    {
        float zAxis = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        playerView.transform.Translate(playerModel.SpeedMovement * direction * Time.deltaTime, Space.World);
        playerView.transform.eulerAngles = new Vector3(0, 0, zAxis);
        canvas.transform.rotation = Quaternion.identity;
        
    }
}
