using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    enum States { patrol,pursuit}

    [SerializeField]
    States state = States.patrol;
    [SerializeField]
    float searchRange = 1f;
    [SerializeField]
    float stoppingDistance = 0.3f;

    public float secondstoPunch=1.5f;

    Transform player;
    Vector3 target;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SetTarget", 0, 2.5f);
        InvokeRepeating("SendPunch", 0, secondstoPunch);
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, searchRange);
        Gizmos.DrawWireSphere(target, 0.2f);

        base.OnDrawGizmosSelected();
    }
    void SetTarget()
    {
        if (state!=States.patrol)
            return;
        target = new Vector2(transform.position.x + Random.Range(-searchRange,searchRange),Random.Range(LimitsY.y,LimitsY.x));
        
    }

    void SendPunch()
    {
        if (state != States.pursuit)
            return;
        if (vel.magnitude != 0)
            return;
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            StartCoroutine(Punch());
        }
    }

    Vector2 vel;
    void Update()
    {
        if(state==States.pursuit)
        {
            target = player.position;
            if(Vector3.Distance(target, transform.position)> searchRange *1.2f)
            {
                target = transform.position;
                state = States.patrol;
                return;
            }
        }

        else if(state == States.patrol)
        {
            var ob = Physics2D.CircleCast(transform.position, searchRange, Vector2.up);
            if(ob.collider!=null)
            {
                if (ob.collider.CompareTag("Player"))
                {
                    state = States.pursuit;
                    return;
                }
            }
        }

        vel = target - transform.position;
        sr.flipX = vel.x < 0;
        if (vel.magnitude < stoppingDistance)
            vel = Vector2.zero;
        vel.Normalize();
        

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            anim.SetBool("IsWalking", vel.magnitude != 0);
        }
        else
        {
            vel= Vector2.zero;
        }

        rb.velocity = new Vector2(vel.x * horizontalSpeed, vel.y * verticalSpeed);
    }
}
