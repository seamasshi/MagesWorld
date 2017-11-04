using UnityEngine;
using System.Collections;

public class DelayedDestroy : MonoBehaviour {
    public float delayTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        delayTime -= Time.deltaTime;
        if (delayTime <= 0)
        {
            Destroy(gameObject);
        }
	
	}
}
