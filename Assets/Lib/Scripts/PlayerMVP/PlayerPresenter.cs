using UnityEngine;

public class PlayerPresenter 
{
    private PlayerView playerView;
    private PlayerModel playerModel;

    public PlayerPresenter(PlayerView playerView, PlayerModel playerModel)
    {
        this.playerView = playerView;
        this.playerModel = playerModel;
        this.playerModel.CurrentHealth = this.playerModel.MaxHealth;
        this.playerView.OnCoinGet += SetNewCount;
        this.playerView.OnBulletCollide += SetNewHealth;
    }

    public void Move(Vector2 direction)
    {
        float zAxis = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        playerView.transform.Translate(playerModel.SpeedMovement * direction * Time.deltaTime, Space.World);
        playerView.transform.eulerAngles = new Vector3(0, 0, zAxis);
        
    }
    public void SetNewCount()
    {
        playerModel.CouinsCount++;
        Debug.Log(playerModel.CouinsCount);
    }

    public void SetNewHealth(float damage)
    {
        playerModel.CurrentHealth -= damage;
        if (playerModel.CurrentHealth <= 0)
        {
            playerView.Disable();
        }
        playerView.DisplayNewHealthBar(playerModel.CurrentHealth, playerModel.MaxHealth);
    }
}
