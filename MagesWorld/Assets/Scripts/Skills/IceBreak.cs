using UnityEngine;
using System.Collections;

public class IceBreak : MonoBehaviour {

    public float damage;
    public float radius;
    [Tooltip("time between spawn and deal damage")]
    public float delayTime;
    bool breaked;
    [Tooltip("time between dealing damage and destroy")]
    public float destroyTime;

    
    public bool slow;    
    public float slowedTime;    
    public float slowedRate;
    
    void Awake () {
        slow = false;
        slowedRate = 0;
        slowedTime = 0;
        breaked = false;

    }
	
	// Update is called once per frame
	void Update () {
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
                        if (slow)
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

    public void SetSlow(float time, float rate)
    {
        slow = true;
        slowedRate = rate;
        slowedTime = time;

    }
}
