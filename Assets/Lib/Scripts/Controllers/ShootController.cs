using System;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Action OnShoot;
    
    public void Shoot()
    {
        if (OnShoot != null)
        {
            OnShoot.Invoke();
        }
    }
}
