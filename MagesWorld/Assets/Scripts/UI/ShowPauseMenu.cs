using UnityEngine;
using System.Collections;

public class ShowPauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject btnPause;
    GameObject gameController;
    bool paused;
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        paused = false;
    }

    public void LoadPauseMenu()
    {
        paused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        btnPause.SetActive(false);

    }
    
    public void DisablePauseMenu()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        btnPause.SetActive(true);

    }

	void Update() {
        if (paused)
        {
            gameController.GetComponent<KeyboardInput>().SetLockingTime(0.3f, false);
        }
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if (Time.timeScale == 0f) {
				pauseMenu.SetActive (false);
				Time.timeScale = 1f;
				btnPause.SetActive (true);
			} else {
				Time.timeScale = 0f;
				pauseMenu.SetActive (true);
				btnPause.SetActive (false);
			}
		}
	}

}
