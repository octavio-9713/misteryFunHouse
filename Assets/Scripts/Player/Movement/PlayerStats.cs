using System;

[Serializable]
public class PlayerStats
{
    public int maxHp = 5;
    public int currentHp;

    public float playerSpeed = 10000;

    public float dashSpeed;
    public float initialDashForce = 8000;

}
