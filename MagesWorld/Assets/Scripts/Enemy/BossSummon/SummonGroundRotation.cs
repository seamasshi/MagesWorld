using UnityEngine;
using System.Collections;

public class SummonGroundRotation : MonoBehaviour {
    public GameObject BOSS;
    Renderer rend;
    float softFactor;
    bool summoning;
    public float SummoningTime;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        softFactor = 0f;
        summoning = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (summoning)
        {
            if (softFactor < 2)
            {
                softFactor += Time.deltaTime * 0.25f;
            }
            SummoningTime -= Time.deltaTime;
            if (SummoningTime < 0)
            {
                Instantiate(BOSS, transform.position, transform.rotation);
                summoning = false;
            }
        }
        else
        {
            if (softFactor > 0)
            {
                softFactor -= Time.deltaTime * 0.25f;
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
        }
        
        rend.material.SetFloat("_InvFade", softFactor); //set soft particles factor
        transform.Rotate(Vector3.up, Time.deltaTime * 10, Space.World);      
      

    }
}
