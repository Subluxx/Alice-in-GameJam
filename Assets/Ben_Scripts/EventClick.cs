using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    ReSizeObject reSizeObject;
    PopUp popUp;
    Outline outline;
    bool touching = false;
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
    }
    public void OnDrag(PointerEventData eventData){
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);  


    }
    public void OnBeginDrag(PointerEventData eventData){
        Cursor.visible = false;
    }
    public void OnEndDrag(PointerEventData eventData){
        Cursor.visible = true;
    }
    public void OnPointerClick(PointerEventData eventData){
        //Debug.Log("FRENCH TOAST");
    }
    public void OnPointerDown(PointerEventData eventData){
    }
    public void OnPointerUp(PointerEventData eventData){
    }
    public void OnPointerEnter(PointerEventData eventData){
        if(Cursor.visible == true){
            outline.OutlineMode = Outline.Mode.OutlineAll;
            popUp.popUpVisable();
            touching = true;
        }

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
