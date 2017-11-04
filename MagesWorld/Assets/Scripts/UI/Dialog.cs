using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {

    public Texture2D dialog_framework;
    public Texture2D dialog_space_1;
    public Texture2D dialog_space_2;
    Rect dialogRect,spaceRect;
    bool dialog_space;
    public float time;
    // Use this for initialization
    void Start () {
        dialogRect = new Rect(Screen.width * 0.15f, Screen.height * 0.05f, Screen.width * 0.7f, Screen.height * 0.3f);
        spaceRect = new Rect(Screen.width * 0.75f, Screen.height * 0.22f, Screen.width * 0.08f, Screen.height * 0.08f);

    }
	
	// Update is called once per frame
	void Update () {
        
        time += Time.unscaledDeltaTime;
        if (time >= 0.5)
        {
            dialog_space = !dialog_space;
            time = 0;
        }
        
    }

    void OnGUI()
    {
        GUI.DrawTextureWithTexCoords(dialogRect, dialog_framework,new Rect(0,0,1f,1f));       
        GUI.DrawTextureWithTexCoords(spaceRect, (dialog_space?dialog_space_2:dialog_space_1), new Rect(0, 0, 1f, 1f));  


    }
}
