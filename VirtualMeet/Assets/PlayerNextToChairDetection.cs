using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNextToChairDetection : MonoBehaviour
{
    [SerializeField] private Text NextToChairPopupText;

    private bool EntryOnSitState { get; set; }
    
    void LateUpdate()
    {
        Vector3 difference = new Vector3(
            this.transform.position.x - Camera.main.transform.position.x,
            this.transform.position.y - Camera.main.transform.position.y,
            this.transform.position.z - Camera.main.transform.position.z);
        
        double distance = Math.Sqrt(
            Math.Pow(difference.x, 2f) +
            // Math.Pow(difference.y, 2f)
            Math.Pow(difference.z, 2f)
        );

        if (distance < 1)
        {
            EntryOnSitState = true;
            NextToChairPopupText.text = "Press J to sit on the chair";
        }
        else
        {
            if (EntryOnSitState)
            {
                NextToChairPopupText.text = "";
            }
            EntryOnSitState = false;
        }
    }
}
