using UnityEngine;
using System.Collections;

public class BackToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            Application.LoadLevel(0);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Application.LoadLevel(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(0);
        }
    }
}
