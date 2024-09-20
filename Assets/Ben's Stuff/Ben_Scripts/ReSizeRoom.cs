using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReSizeRoom : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    BigRoom bigRoom;
    private void Awake(){
        bigRoom = GetComponent<BigRoom>();
    }
    public void OnPointerClick(PointerEventData eventData){
    }
    public void OnPointerDown(PointerEventData eventData){
        if(eventData.pointerId == -1){
            bigRoom.enlarge();
        }else if(eventData.pointerId == -2){
            bigRoom.returnSize();
        }
    }
    public void OnPointerUp(PointerEventData eventData){
    }
    public void OnPointerEnter(PointerEventData eventData){

    }
    public void OnPointerExit(PointerEventData eventData){
        //bigRoom.returnSize();
    }

}