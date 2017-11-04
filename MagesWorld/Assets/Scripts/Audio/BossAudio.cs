using UnityEngine;
using System.Collections;

public class BossAudio : MonoBehaviour {

    void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
