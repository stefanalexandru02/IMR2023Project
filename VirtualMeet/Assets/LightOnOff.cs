using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    private Light _light;
    
    private void Start()
    {
        this._light = this.GetComponent<Light>();
    }
    
    void FixedUpdate()
    {
        Vector3 difference = new Vector3(
             this.transform.position.x - Camera.main.transform.position.x,
             this.transform.position.y - Camera.main.transform.position.y,
             this.transform.position.z - Camera.main.transform.position.z);
        
        double distance = Math.Sqrt(
             Math.Pow(difference.x, 2f) +
             Math.Pow(difference.z, 2f)
        );

        if (distance < 5)
        {
            _light.enabled = true;
        }
        else
        {
            _light.enabled = false;
        }
    }
}
