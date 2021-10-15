using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : Fighter   
{

   
   
    
    Vector2 cntrl;
    // Update is called once per frame
    void Update()
    {
        cntrl = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        if(cntrl.x !=0)
        {
            sr.flipX = cntrl.x < 0;
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {

            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine(Punch());
            }


            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", cntrl.magnitude != 0);
                rb.velocity = new Vector2(cntrl.x * horizontalSpeed *2, cntrl.y * verticalSpeed*2);
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, LimitsY.y, LimitsY.x), transform.position.z);
            }
            else
            {
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsWalking", cntrl.magnitude != 0);
                rb.velocity = new Vector2(cntrl.x * horizontalSpeed, cntrl.y * verticalSpeed);
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, LimitsY.y, LimitsY.x), transform.position.z);
            }
                                   
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
                        
    }

    
}
