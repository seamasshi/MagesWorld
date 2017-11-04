using UnityEngine;
using System.Collections;

public class BoltMovement : MonoBehaviour {

    public float movespeed;
    Vector3 velocity;
    Rigidbody waveRigidbody;
    
    void Start()
    {
        waveRigidbody = GetComponent<Rigidbody>();
        velocity = transform.rotation * Vector3.forward * movespeed;
        waveRigidbody.velocity = velocity;
    }
   
    void Update () {
	
	}
}
