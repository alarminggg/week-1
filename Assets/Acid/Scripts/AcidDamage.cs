using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AcidDamage : MonoBehaviour
{
    [SerializeField]
    float rawDamage = 1000f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerObject")
        {
            Debug.Log("Player touched acid!");
            HealthManager playerHealth = other.gameObject.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                playerHealth.Hit(rawDamage);
            }
        }
    }
}
