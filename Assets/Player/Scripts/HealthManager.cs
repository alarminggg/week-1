using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    float hitPoints = 100f;

    void Hit(float rawDamage)
    {
        hitPoints -= rawDamage;
        Debug.Log("Outch: " + hitPoints.ToString());

        if(hitPoints <= 0)
        {
            //todo: game over
            Debug.Log("Game Over - You Died");
        }
    }
}
