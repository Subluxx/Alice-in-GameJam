using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    Transform player;
    public Transform interactionTransform;
    public float radius = 5f;

    bool isFocus = false;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        //method meant to be overiden
        Debug.Log("Interacting with" + transform.name);
    }



    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;

        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;

        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }




    void Start()
    {

    }

    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            Debug.Log("interact! ;3");
            Interact();
            hasInteracted = true;
        }
    }
}
