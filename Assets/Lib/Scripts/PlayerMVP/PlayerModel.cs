public class PlayerModel
{
    private float speedMovement = 5;
    private float speedRotation = 10;
    private int coinsCount = 0;
    private float maxHealth = 100;
    private float currentHealth;
    public float SpeedRotation { get => speedRotation; }
    public float SpeedMovement { get => speedMovement; }
    public int CouinsCount { get => coinsCount; set { coinsCount = value; } }
    public float MaxHealth { get => maxHealth; }
    public float CurrentHealth { get => currentHealth; set { currentHealth = value; } }

}
