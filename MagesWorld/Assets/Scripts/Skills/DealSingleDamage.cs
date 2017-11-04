using UnityEngine;
using System.Collections;

public class DealSingleDamage : MonoBehaviour
{
    public float damage;
    public bool needdestroy;

    public bool ignite;
    public bool slow;
    public float ignitedTime;
    public float slowedTime; 
    public float ignitedDamage;
    public float slowedRate;

    void Awake()
    {
        ignite = false;
        slow = false;
        ignitedDamage = 0;
        ignitedTime = 0;
        slowedRate = 0;
        slowedTime = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyCommon>().Hit(damage);
            //deal with debuff
            if (ignite)
            {
                collision.gameObject.GetComponent<EnemyCommon>().DebuffIgnite(ignitedTime,ignitedDamage);
            }
            if (slow)
            {
                collision.gameObject.GetComponent<EnemyCommon>().DebuffSlow(slowedTime,slowedRate);
            }
            if (needdestroy)
            Destroy(gameObject);
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

