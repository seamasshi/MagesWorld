using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
    public Texture2D[] texture_turorials = new Texture2D[6];
    Rect rect_tutorials;
    Rect spaceRect;
    public Texture2D dialog_space_1;
    public Texture2D dialog_space_2;
    bool dialog_space;
    float time;
    int tutorialProgress;
    bool tutorialStarted;
    GameObject gameController;
    // Use this for initialization
    void Start () {
        tutorialProgress = 0;
        tutorialStarted = false;
        rect_tutorials = new Rect(0,0,Screen.width,Screen.height);
        spaceRect = new Rect(Screen.width * 0.7f, Screen.height * 0.55f, Screen.width * 0.12f, Screen.height * 0.12f);
        gameController = GameObject.FindGameObjectWithTag("GameController");
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
        if (tutorialStarted)
        {
            gameController.GetComponent<KeyboardInput>().SetLockingTime(0.3f,false);
            GUI.DrawTextureWithTexCoords(rect_tutorials, texture_turorials[tutorialProgress], new Rect(0, 0, 1, 1));
            GUI.DrawTextureWithTexCoords(spaceRect, (dialog_space ? dialog_space_2 : dialog_space_1), new Rect(0, 0, 1f, 1f));
        }
        
    }

    public void Progress()
    {
        tutorialProgress++;
        if (tutorialProgress >= texture_turorials.Length)
        {
            tutorialProgress = 0;
            EndTutorial();
        }
    }

    public void StartTutorial()
    {
        tutorialStarted = true;
        tutorialProgress = 0;
        Time.timeScale = 0;
    }

    public void EndTutorial()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
