using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Outline outline;
    private void Awake(){
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineHidden;
        outline.OutlineColor = Color.magenta;
        outline.OutlineWidth = 5f;
        //materialApplier = GetComponent<MaterailApplier>;
    }
    public void OnPointerDown(PointerEventData eventData){
        //materialApplier.ApplyOther();
    }
    public void OnPointerUp(PointerEventData eventData){
        //materialApplier.ApplyOriginal();
    }
    public void OnPointerClick(PointerEventData eventData){
        Debug.Log("FRENCH TOAST");
    }
    public void OnPointerEnter(PointerEventData eventData){
        outline.OutlineMode = Outline.Mode.OutlineAll;
    }
    public void OnPointerExit(PointerEventData eventData){
        outline.OutlineMode = Outline.Mode.OutlineHidden;
    }
}
