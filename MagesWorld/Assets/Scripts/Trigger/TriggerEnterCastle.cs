using UnityEngine;
using System.Collections;

public class TriggerEnterCastle : MonoBehaviour {

    GameObject gameController;
    // Use this for initialization
    void Start () {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0];
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            gameController.GetComponent<CharacterState>().DecreaseVillagerNum(collider.GetComponent<EnemyCommon>().villagerKillingAbility);
            Destroy(collider.gameObject);
        }

    }
}
