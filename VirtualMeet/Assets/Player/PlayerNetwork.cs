using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{
    private const float moveSpeed = 3f;
    public float sensitivityX;
    public float sensitivityY;

    private float xRotation;
    private float yRotation;
    
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraPos;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void LateUpdate()
    {
        bool shouldMoveMouse = Cursor.lockState == CursorLockMode.Locked;
        if(!IsOwner) return;
        
        // Mouse movement
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;
        yRotation += mouseX;
        // yRotation = Mathf.Clamp(yRotation, -45f, 45f);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -20f, 20f);
        
        // here are for both axis, but in the PlayerNetwork you should have only Y i think. No UP/DOWN rotation. That is on camera only
        // transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // this is for camera -> don't propagate
        // orientation.rotation = Quaternion.Euler(0, yRotation, 0); // this is for player rotation -> it needs to be propagated

        if (shouldMoveMouse)
        {
            cameraPos.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        
        // Keyboard movement
        Vector3 moveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;

        if (Input.GetKey(KeyCode.Q))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        // if(moveDir.z > 0)
        //     moveDir.x += 0.02f * yRotation;
        // else if(moveDir.z < 0)
        //     moveDir.x -= 0.02f * yRotation;

        moveDir = Quaternion.AngleAxis(yRotation, Vector3.up) * moveDir;

        // Debug.Log($"yRotation: {yRotation}");

        if (moveDir == Vector3.zero)
            animator.SetBool("IsWalking", false);
        else
            animator.SetBool("IsWalking", true);
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        Camera.main.transform.position = cameraPos.position;
        Camera.main.transform.rotation = cameraPos.rotation;
    }
}
