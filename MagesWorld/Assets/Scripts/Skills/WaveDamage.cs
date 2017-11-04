using UnityEngine;
using System.Collections;

public class WaveDamage : MonoBehaviour {


    public float damage;
    [Tooltip("The percentage damage fade to")]
    float damagePercentage;
    [Tooltip("the fade rate of damage")]
    public float damageFadeRate;
    [Tooltip("the minimum fade rate of damage")]
    public float damageFadeMin;
    [Tooltip("Will this spell spawn Hit effects when hit enemies, keep it empty if no effects")]
    public GameObject hitEffect;

    public bool ignite;
    public bool slow;
    public float ignitedTime;
    public float slowedTime;
    public float ignitedDamage;
    public float slowedRate;

    void Awake()
    {
        damagePercentage = 1;


        ignite = false;
        slow = false;
        ignitedDamage = 0;
        ignitedTime = 0;
        slowedRate = 0;
        slowedTime = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyCommon>().Hit(damage*damagePercentage);
            damagePercentage -= damageFadeRate;
            damagePercentage = ((damagePercentage>damageFadeMin)?damagePercentage:damageFadeMin);
            if (hitEffect != null)//spawn hit effects
            {
                Instantiate(hitEffect, other.transform.position + Vector3.up * other.transform.localScale.y, other.transform.rotation);
            }
            if (ignite)
            {
                other.gameObject.GetComponent<EnemyCommon>().DebuffIgnite(ignitedTime, ignitedDamage);
            }
            if (slow)
            {
                other.gameObject.GetComponent<EnemyCommon>().DebuffSlow(slowedTime, slowedRate);
            }
        }
    }

    //public functions for set Debuffs.
    public void SetIgnite(float time, float damage)
    {
        ignite = true;
        ignitedDamage = damage;
        ignitedTime = time;
    }

    public void SetSlow(float time, float rate)
    {
        slow = true;
        slowedRate = rate;
        slowedTime = time;

    }
}
