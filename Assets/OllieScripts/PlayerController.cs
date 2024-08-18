using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class PlayerController : MonoBehaviour
{
    public Camera cam;

    public LayerMask movementMask;

    public Interactable focus;

    [SerializeField] ParticleSystem clickEffect;

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("interact the " + hit.collider.name + ' ' + hit.point);

                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }

            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;
        }

        newFocus.OnFocused(transform);

    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }

        focus = null;
    }
}
