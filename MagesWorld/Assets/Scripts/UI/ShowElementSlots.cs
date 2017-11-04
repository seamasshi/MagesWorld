using UnityEngine;
using System.Collections;

public class ShowElementSlots : MonoBehaviour {
    public GUIText elements;
    int[] elementSlots = new int[4]; 
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        elementSlots = GetComponent<CharacterState>().Out_ElementSlots();
        elements.text = "ELEMENT SLOTS:";
        for (int i = 0; i < 4; i++)
        {
            switch (elementSlots[i])
            {
                case 1:
                    elements.text += " *Fire* ";
                    break;
                case 2:
                    elements.text += " *Lightning* ";
                    break;
                case 3:
                    elements.text += " *Ice* ";
                    break;
                case 4:
                    elements.text += " *Air* ";
                    break;
                default:
                    elements.text += " *NULL* ";
                    break;


            }
                
           
        }
	}
}
