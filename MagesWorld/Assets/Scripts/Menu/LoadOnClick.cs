using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingImage;

	public void LoadScene(int level) {
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        if (level == 1)
        {
            loadingImage.SetActive(true);
        }
        else
        {
            loadingImage.SetActive(false);
        }
		
		Application.LoadLevel(level);
	}

	public void ExitGmae() {
		Application.Quit();
	}
}
