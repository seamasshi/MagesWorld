using UnityEngine;
using System.Collections;

public class MuscleEarthquakeSummon : MonoBehaviour {
    public float summonTime;
    public GameObject Summon;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        summonTime -= Time.deltaTime;
        if (summonTime <= 0)
        {
            Instantiate(Summon, transform.position, transform.rotation);
            Destroy(gameObject);

        }
	
	}
}
