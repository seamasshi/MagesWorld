using UnityEngine;
using System.Collections;

public class BallTrack : MonoBehaviour {
    GameObject gameController;
    // Use this for initialization
    void Start () {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0];
    }
	
	// Update is called once per frame
	void Update () {
        if (gameController.GetComponent<CharacterState>().Out_MagicShape() == 3)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
	}
}
