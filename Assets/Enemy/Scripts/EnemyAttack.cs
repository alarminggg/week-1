using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    Transform gunTransform;
    [SerializeField]
    float maxDistanceToTarget = 6f;

    float distanceToTarget;

    [SerializeField]
    float rawDamage = 10f;

    [SerializeField]
    float delayTimer = 2f;
    float tick;
    bool attackReady = true;


    private void Start()
    {
        tick = delayTimer;
        gunTransform = gameObject.transform.Find("Gun");
    }

    private void Update()
    {
        Attack();
    }

    void Attack()
    {
        distanceToTarget = Vector3.Distance(playerTransform.position, transform.position);
        attackReady = IsAttackReady();

        if(distanceToTarget <= maxDistanceToTarget)
        {
            LookAtTarget();
            if(attackReady)
            {
                tick = 0f;
                Ray ray = new Ray(gunTransform.transform.position, gunTransform.transform.forward);
                Debug.DrawRay(gunTransform.transform.position, gunTransform.transform.forward * 3, Color.magenta, 0.2f);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistanceToTarget))
                {
                    Debug.Log("Enemy Shoots");
                    if(raycastHit.transform != null)
                    {
                        raycastHit.collider.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver);
                    }
                    
                }
                else
                {
                    Debug.Log("Enemy: Failed to return value on raycast");
                }
            }
        }
    }
    
    void LookAtTarget()
    {
        //ransform.LookAt(playerTransform.position);
        Vector3 lookVector = playerTransform.position - transform.position;
        lookVector.y = transform.position.y;
        Quaternion rotation = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);
    }

    bool IsAttackReady()
    {
        if(tick < delayTimer)
        {
            tick += Time.deltaTime;
            return false;
        }

        return true;
    }
}
