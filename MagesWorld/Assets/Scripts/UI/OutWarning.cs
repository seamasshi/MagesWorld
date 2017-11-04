using UnityEngine;
using System.Collections;

public class OutWarning : MonoBehaviour {
    public Texture2D textureWarning;
    public Rect rectWarning;
    bool warned;
    void Start()
    {
        warned = false;
        rectWarning.x *= Screen.width;
        rectWarning.y *= Screen.height;
        rectWarning.width *= Screen.width;
        rectWarning.height *= Screen.height;
    }
    // Use this for initialization
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            warned = true;
        }

    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            warned = false;
        }
    }

    void OnGUI()
    {
        if (warned)
        {
            GUI.DrawTextureWithTexCoords(rectWarning, textureWarning, new Rect(0, 0, 1, 1));            
        }

    }
}
