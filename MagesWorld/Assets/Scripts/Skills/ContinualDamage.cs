using UnityEngine;
using System.Collections;

public class ContinualDamage : MonoBehaviour {
    [Tooltip("damage per second")]
    public float damage;
    public float radius;
    public float duration;
    float tickTime;

    public bool ignite;
    public bool slow;
    public float ignitedTime;
    public float slowedTime;
    public float ignitedDamage;
    public float slowedRate;

    // Use this for initialization
    void Awake()
    {
        tickTime = 0;
        ignite = false;
        slow = false;
        ignitedDamage = 0;
        ignitedTime = 0;
        slowedRate = 0;
        slowedTime = 0;
    }
   
	
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            Destroy(gameObject);
        }
        if ((Time.time - tickTime) > 0.5)
        {
            tickTime = Time.time;
            Collider[] Colliders = Physics.OverlapSphere(transform.position, radius);
            for (int i = 0; i < Colliders.Length; i++)
            {
                if (Colliders[i].tag == "Enemy")
                {
                    Colliders[i].GetComponent<EnemyCommon>().Hit(damage / 2.0f);
                    if (ignite)
                    {
                        Colliders[i].GetComponent<EnemyCommon>().DebuffIgnite(ignitedTime, ignitedDamage);
                    }
                    if (slow)
                    {
                        Colliders[i].GetComponent<EnemyCommon>().DebuffSlow(slowedTime, slowedRate);
                    }
                }
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
