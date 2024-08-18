using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject popUp;
    public void popUpVisable(){
        popUp.SetActive(true);
    }
    public void popUpInvisable(){
        popUp.SetActive(false);
    }
}
