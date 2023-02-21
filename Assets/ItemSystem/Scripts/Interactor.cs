using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    Transform actorCamera;
    LayerMask layerMask;

    [SerializeField]
    private float maxDistanceFromCamera = 10f;

    [SerializeField]
    private float maxInteractableDistance = 3f;
    private float distanceFromActor;



    // Start is called before the first frame update
    void Start()
    {
        layerMask = ~LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    void Interact()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            actorCamera = Camera.main.transform;
            Debug.Log("Live Camera: " + actorCamera.name);

            Ray ray = new Ray(actorCamera.position, actorCamera.forward);
            Debug.DrawRay(actorCamera.position, actorCamera.forward * 10, Color.magenta, 0.5f);

            if(Physics.Raycast(ray, out RaycastHit raycastHit, maxDistanceFromCamera, layerMask))
            {
                Debug.Log("RAY HIT");

                if(raycastHit.transform != null)
                {
                    distanceFromActor = Vector3.Distance(transform.position, raycastHit.transform.position);
                    if(distanceFromActor <= maxInteractableDistance)
                    {
                        Debug.Log("In range: " + raycastHit.transform.name + " (" + distanceFromActor.ToString("0.00") + ")");
                        Item item = raycastHit.transform.GetComponent<Item>();
                        if(item != null)
                        {
                            item.Interact();
                        }
                    }
                }
            }
            else
            {
                Debug.Log("RAY MISS");
            }
        }
   
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxInteractableDistance);
    }

}

