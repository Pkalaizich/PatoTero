using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Barril : MonoBehaviour
{

    SpriteRenderer sr;
    Animator anim;
    float life = 20f;

    private void Awake()
    {        
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    

    IEnumerator GetPunched(float damage)
    {
        anim.SetTrigger("getpunch");
        life -= damage;
        if (life<=0)
        {
            anim.SetTrigger("Destroyed");
            yield return new WaitForSeconds(0.1f);
            this.gameObject.SetActive(false);
        }
    }
}
