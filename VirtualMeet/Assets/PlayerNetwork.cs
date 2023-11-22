using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    void Update()
    {
        if(!IsOwner) return;
        
        Vector3 moveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;

        if (moveDir == Vector3.zero)
        {
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
            
            float moveSpeed = 3f;
            // when moving, you need to add animation
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }
}
