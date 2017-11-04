using UnityEngine;
using System.Collections;

public class MuscleEarthquake : MonoBehaviour {
    [Tooltip("damage per second")]
    public float damage;
    public float radius;
    public float slowRate;
    public float duration;
    float tickTime;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            Destroy(gameObject);
        }
        if ((Time.time - tickTime) > 0.333)
        {
            tickTime = Time.time;
            Collider[] Colliders = Physics.OverlapSphere(transform.position, radius);
            for (int i = 0; i < Colliders.Length; i++)
            {
                if (Colliders[i].tag == "Player")
                {
                    Colliders[i].GetComponent<PlayerMovement>().SetSpeedModi(1f-slowRate);
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<CharacterState>().GetDamage(damage/3.0f);
                    break;
                }
            }

        }

    }
}
