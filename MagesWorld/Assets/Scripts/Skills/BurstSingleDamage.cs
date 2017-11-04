using UnityEngine;
using System.Collections;

public class BurstSingleDamage : MonoBehaviour {
    [Tooltip("Damage of this spell")]
    public float damage;
    [Tooltip("Burst radius")]
    public float radius;
    [Tooltip("time between spawn and deal damage")]
    public float delayTime;
    bool breaked;// record the state of this spell
    [Tooltip("time between dealing damage and destroy")]
    public float destroyTime;
    [Tooltip("Will this spell spawn Hit effects when hit enemies, keep it empty if no effects")]
    public GameObject hitEffect;

    //variables for debuffs
    bool ignite;
    bool slow;
    float ignitedTime;
    float slowedTime;
    float ignitedDamage;
    float slowedRate;

    void Awake()// initiate all paramenters immediately, before the Spell script adjust them
    {
        ignite = false;
        slow = false;
        ignitedDamage = 0;
        ignitedTime = 0;
        slowedRate = 0;
        slowedTime = 0;
        breaked = false;
    }

    void Update()
    {
        if (!breaked)
        {
            delayTime -= Time.deltaTime;
            if (delayTime < 0)
            {
                breaked = true;
                Collider[] Colliders = Physics.OverlapSphere(transform.position, radius);
                for (int i = 0; i < Colliders.Length; i++)
                {
                    if (Colliders[i].tag == "Enemy")
                    {
                        Colliders[i].GetComponent<EnemyCommon>().Hit(damage);
                        if (hitEffect != null)//spawn hit effects
                        {
                            Instantiate(hitEffect, Colliders[i].transform.position+Vector3.up* Colliders[i].transform.localScale.y, Colliders[i].transform.rotation);
                        }
                        if (ignite)//deal with debuff
                        {
                            Colliders[i].GetComponent<EnemyCommon>().DebuffIgnite(ignitedTime, ignitedDamage);
                        }
                        if (slow)//deal with debuff
                        {
                            Colliders[i].GetComponent<EnemyCommon>().DebuffSlow(slowedTime, slowedRate);
                        }
                    }
                }
            }

        }
        else
        {
            destroyTime -= Time.deltaTime;
            if (destroyTime < 0)
            {
                Destroy(gameObject);
            }
        }
    }

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
