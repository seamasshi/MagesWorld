using UnityEngine;
using System.Collections;

public class Vacuum : MonoBehaviour {
    public float radius;
    public float force;
    public float duration;
    float tickTime;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            Destroy(gameObject);
        }
        if ((Time.time - tickTime) > 0.3)
        {
            tickTime = Time.time;
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.AddForce((transform.position-collider.transform.position)*force);
                }

            }
        }
    }
}
