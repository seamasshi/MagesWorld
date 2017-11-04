using UnityEngine;
using System.Collections;

public class EnemyMuscleSkills : MonoBehaviour {
    public GameObject EarthquakeSummon;
    [Tooltip("Time between two waves of earthquake")]
    public float castingSpace;
    bool isCasting;
    float tickTime;
    [Tooltip("Time the skill lasts")]
    public float castingTime;
    float castingTickTime;
    [Tooltip("The time interval between two Skills")]
    public float spacingTime;
    float spacingTickTime; 
    // Use this for initialization
    void Start () {
        spacingTickTime = spacingTime;
        castingTickTime = castingTime;
        isCasting = false;
        tickTime = Time.time;

    }
	
	// Update is called once per frame
	void Update () {
        if (!isCasting)
        {
            spacingTickTime -= Time.deltaTime;
            if (spacingTickTime <= 0)
            {
                isCasting = true;
                spacingTickTime = spacingTime;

                Instantiate(EarthquakeSummon, transform.position + transform.forward * 6f,transform.rotation);
                Instantiate(EarthquakeSummon, transform.position - transform.forward * 6f, transform.rotation);
                Instantiate(EarthquakeSummon, transform.position + transform.right * 6f, transform.rotation);
                Instantiate(EarthquakeSummon, transform.position - transform.right * 6f, transform.rotation);

            }
        }
        else
        {
            castingTickTime -= Time.deltaTime;
            if ((Time.time - tickTime) > castingSpace)
            {
                Instantiate(EarthquakeSummon, GameObject.FindGameObjectWithTag("Player").transform.position, transform.rotation);
                tickTime = Time.time;
            }
            if (castingTickTime <= 0)
            {
                isCasting = false;
                castingTickTime = castingTime;
            }
        } 
	}
}
