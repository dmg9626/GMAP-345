using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterListener : MonoBehaviour
{
    public PlayerShooter shooter;

    public void FireReady()
    {
        shooter.FireBullet();
    }
}
