using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCloseScript : MonoBehaviour
{
    private GameObject popup;
    public Vector3 popupHome; //Where the popup should go when it isn't being displayed
    
    // Start is called before the first frame update
    void Start()
    {
        //Get the popup
        popup = GameObject.Find("Popup");
        popupHome = new Vector3(0, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Triggered when the popup is clicked and should close
    void OnMouseUp()
    {
        popup.GetComponent<Transform>().position = popupHome;
    }
}
