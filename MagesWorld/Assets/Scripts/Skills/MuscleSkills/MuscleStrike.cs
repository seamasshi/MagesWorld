using UnityEngine;
using System.Collections;

public class MuscleStrike : MonoBehaviour {

    public float damage_inner;
    public float damage_outer;
    public float radius_inner;
    public float radius_outer;
    [Tooltip("time between spawn and deal damage")]
    public float delayTime;
    bool breaked;
    [Tooltip("time between dealing damage and destroy")]
    public float destroyTime;
    Collider[] Colliders;
    // Use this for initialization
    void Start () {
        Colliders = Physics.OverlapSphere(transform.position, radius_inner);
        for (int i = 0; i < Colliders.Length; i++)
        {
            if (Colliders[i].tag == "Player")
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<CharacterState>().GetDamage(damage_inner);
                break;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (!breaked)
        {
            delayTime -= Time.deltaTime;
            if (delayTime < 0)
            {
                breaked = true;
                Colliders = Physics.OverlapSphere(transform.position, radius_outer);
                for (int i = 0; i < Colliders.Length; i++)
                {
                    if (Colliders[i].tag == "Player")
                    {
                        GameObject.FindGameObjectWithTag("GameController").GetComponent<CharacterState>().GetDamage(damage_outer);
                        break;
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
}
