using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentPowerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;
    void OnTriggerEnter2D(Collider2D collision)
    {
        powerupEffect.Apply(collision.gameObject);
        collision.gameObject.GetComponent<ShipBehavior>().boolShipUpgraded = true;
    }
}
