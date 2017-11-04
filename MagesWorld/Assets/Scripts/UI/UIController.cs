using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
    
    public GameObject Tutorial;
    public float timeBeforeTutorials;
    bool tutorialTriggered; // determine if turorials have been triggered

	
	void Start () {

        tutorialTriggered = false;
        //Tutorial.GetComponent<Tutorial>().enabled = false;
	}
	
	
	void Update () {
        if (!tutorialTriggered)
        {
            if (Time.time > timeBeforeTutorials)
            {
                StartTutorial();
            }
        }
           
       
        
	
	}

    public void InterfaceProcess()
    {
        if(Tutorial != null)
        Tutorial.GetComponent<Tutorial>().Progress();


    }

    public void StartTutorial()
    {
        tutorialTriggered = true;
        //Tutorial.GetComponent<Tutorial>().enabled = true;
        Tutorial.GetComponent<Tutorial>().StartTutorial();
    }
}
