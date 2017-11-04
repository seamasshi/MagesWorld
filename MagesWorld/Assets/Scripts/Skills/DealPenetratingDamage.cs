using UnityEngine;
using System.Collections;

public class DealPenetratingDamage : MonoBehaviour {
    public float damage;

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

    void OnTriggerEnter(Collider collider)
    {
        
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<EnemyCommon>().Hit(damage);
                if (ignite)
                {
                    collider.GetComponent<EnemyCommon>().DebuffIgnite(ignitedTime, ignitedDamage);
                }
                if (slow)
                {
                    collider.GetComponent<EnemyCommon>().DebuffSlow(slowedTime, slowedRate);
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
