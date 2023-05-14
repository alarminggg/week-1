using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Transform cameraTransform;
    float range = 100f;

    [SerializeField]
    float rawDamage = 10f;

    void Update()
    {
        if(!MenuController.IsGamePaused)
        {
            FireWeapon();
        }
        
    }

    void FireWeapon()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            cameraTransform = Camera.main.transform;
            Vector3 raycastOrigin = cameraTransform.position + cameraTransform.forward * 0.5f;
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            Debug.DrawRay(raycastOrigin, cameraTransform.forward * range, Color.red, 5f);

            int characterLayer = LayerMask.NameToLayer("Character");
            int playerLayer = LayerMask.NameToLayer("Player");
            int layerMask = ~(1 << playerLayer);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, range, layerMask))
            {
                if(raycastHit.transform != null)
                {
                    raycastHit.collider.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                Debug.Log("No RaycastHit object found from PlayerAttack");
            }
        }
    }
}
