using UnityEngine;
using System.Collections;

public class BossSummon : MonoBehaviour {

    public GameObject summonGround;
    bool dead;

	void Start () {
        dead = false;
        GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>().ChangeAudioTo(1);
    }
	
	void Update () {
        if ((GetComponent<EnemyCommon>().enemyStatement == 1)&&(!dead))
        {
            dead = true;
            Vector3 summonposition = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
            Instantiate(summonGround, summonposition, transform.rotation);
            GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>().ChangeAudioTo(2);
        }
    }
}
