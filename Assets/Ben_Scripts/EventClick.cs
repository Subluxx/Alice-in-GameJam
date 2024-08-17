using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    ReSizeRoom reSizeRoom;
    PopUp popUp;
    Outline outline;
    bool touching = false;
    //ReSizeRoom reSizeRoom;
    private Vector2 scale;
    private void Awake(){
        reSizeRoom = GetComponent<ReSizeRoom>();
        popUp = GetComponent<PopUp>();
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineHidden;
        outline.OutlineColor = Color.magenta;
        outline.OutlineWidth = 5f;
        //materialApplier = GetComponent<MaterailApplier>;
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
                reSizeRoom.ReSizeObject();
            }
        }
    }
}
