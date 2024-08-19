using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
/*using UnityEditor.Experimental.GraphView;
*/
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    ReSizeObject reSizeObject;
    PopUp popUp;
    Outline outline;
    bool touching = false;
    //ReSizeRoom reSizeRoom;
    private Vector2 scale;
    Camera m_cam;
    private void Awake(){
        m_cam = Camera.main;
        reSizeObject = GetComponent<ReSizeObject>();
        popUp = GetComponent<PopUp>();
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineHidden;
        outline.OutlineColor = Color.magenta;
        outline.OutlineWidth = 5f;
        //materialApplier = GetComponent<MaterailApplier>;
    }
    public void OnDrag(PointerEventData eventData){
        Ray R = m_cam.ScreenPointToRay(Input.mousePosition); // Get the ray from mouse position
        Vector3 PO = transform.position; // Take current position of this draggable object as Plane's Origin
        Vector3 PN = -m_cam.transform.forward; // Take current negative camera's forward as Plane's Normal
        float t = Vector3.Dot(PO - R.origin, PN) / Vector3.Dot(R.direction, PN); // plane vs. line intersection in algebric form. It find t as distance from the camera of the new point in the ray's direction.
        Vector3 P = R.origin + R.direction * t; // Find the new point.
        transform.position = P;
    }
    public void OnBeginDrag(PointerEventData eventData){

    }
    public void OnEndDrag(PointerEventData eventData){

    }
    public void OnPointerClick(PointerEventData eventData){
        //Debug.Log("FRENCH TOAST");
    }
    public void OnPointerDown(PointerEventData eventData){
    }
    public void OnPointerUp(PointerEventData eventData){
    }
    public void OnPointerEnter(PointerEventData eventData){
        outline.OutlineMode = Outline.Mode.OutlineAll;
        popUp.popUpVisable();
        touching = true;

    }
    public void OnPointerExit(PointerEventData eventData){
        popUp.popUpInvisable();
        outline.OutlineMode = Outline.Mode.OutlineHidden;
        touching = false;
    }

    private void Update(){
        if(touching){
            if(Input.GetKey(KeyCode.F)){
                reSizeObject.reSizeObject();
            }else if(Input.GetKey(KeyCode.G)){
                reSizeObject.shrinkObject();
            }
        }
    }
}
