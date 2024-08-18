using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReSizeRoom : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    BigRoom bigRoom;
    private void Awake(){
        bigRoom = GetComponent<BigRoom>();
    }
    public void OnPointerClick(PointerEventData eventData){
        bigRoom.enlarge();
    }
    public void OnPointerDown(PointerEventData eventData){
    }
    public void OnPointerUp(PointerEventData eventData){
    }
    public void OnPointerEnter(PointerEventData eventData){

    }
    public void OnPointerExit(PointerEventData eventData){

    }

}