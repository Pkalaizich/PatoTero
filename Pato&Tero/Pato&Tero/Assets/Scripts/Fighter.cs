using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Fighter : MonoBehaviour
{
    protected static Vector2 LimitsY = new Vector2(1f, -1.65f);


    [SerializeField]
    protected float verticalSpeed;
    [SerializeField]
    protected float horizontalSpeed;
    [SerializeField]
    protected Transform leftpunch;
    [SerializeField]
    protected Transform rightpunch;
    [SerializeField]
    protected float punchradius = 0.1f;
    public float punchDamage=5;
    public float Life=20;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator anim;

    protected virtual void OnDrawGizmosSelected()
    {

        if (leftpunch == null || rightpunch == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftpunch.position, punchradius);
        Gizmos.DrawWireSphere(rightpunch.position, punchradius);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void GetPunched(float damage)
    {
        Debug.Log("AIA2");
        anim.SetTrigger("IsDamaged");
        Life -= damage;
        if (Life<=0)
        {
            this.gameObject.SetActive(false);
            if (this.gameObject.tag=="Player")
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    protected IEnumerator Punch()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            anim.SetTrigger("SendPunch");
            yield return new WaitForSeconds(0.05f);
            Vector2 punchPosition = sr.flipX ? leftpunch.position : rightpunch.position;

            var castcircle = Physics2D.CircleCast(punchPosition, punchradius, Vector2.up);
            if (castcircle.collider != null)
            {
                Debug.Log("AIA");
                if (castcircle.collider.gameObject != gameObject)
                {
                    castcircle.collider.gameObject.SendMessage("GetPunched", punchDamage);
                }
            }
        }
    }

}
