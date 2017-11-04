using UnityEngine;
using System.Collections;

public class AirWave : MonoBehaviour {
    public float force;
    // Use this for initialization
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            rb.AddForce((collider.transform.position - transform.position + transform.forward *2.0f)*force);


        }
    }
}
