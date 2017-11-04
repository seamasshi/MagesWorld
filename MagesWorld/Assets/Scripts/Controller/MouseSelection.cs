using UnityEngine;
using System.Collections;

public class MouseSelection : MonoBehaviour {
    public Vector3 mousePoint;
    public GameObject hidePosition;
    public GameObject areaSelection;
    
    public bool isSpellingArea;
	// Use this for initialization
	void Awake () {
        isSpellingArea = false;        
    }
	
	// Update is called once per frame
	void Update () {

        mousePoint = GetComponent<MouseMove>().mousepoint;
        if (GetComponent<CharacterState>().Out_MagicShape() == 2)
        {
            isSpellingArea = true;
        }
        else
        {
            isSpellingArea = false;
        }
        if (isSpellingArea)
        {
            //areaSelection.transform.position = mousePoint;
            areaSelection.transform.position = new Vector3(mousePoint.x,0.5f,mousePoint.z);
        }
        else
        {
            areaSelection.transform.position = hidePosition.transform.position;
        }
        if (Input.GetKeyDown("z"))
        {
            areaSelection.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
        if (Input.GetKeyDown("x"))
        {
            areaSelection.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }

    }
}
