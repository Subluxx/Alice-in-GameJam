using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomEnlarge : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    BigRoom bigRoom;
    private void Awake(){
        bigRoom = GetComponent<BigRoom>();
    }
    public void OnPointerClick(PointerEventData eventData){
        bigRoom.enlarge();
        //Debug.Log("FRENCH TOAST");
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