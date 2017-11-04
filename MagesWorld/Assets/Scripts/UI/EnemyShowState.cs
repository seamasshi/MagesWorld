using UnityEngine;
using System.Collections;

public class EnemyShowState : MonoBehaviour
{
    Camera mainCamera;
    public float height;
    public Texture2D blood_red;
    public Texture2D blood_black;
    public Texture2D texture_attraction;
    Vector3 pos;
    public Vector2 screenPos;
    float lifeMax;
    float life;
    float attraction;
    public float hpBarWidth;
    public float hpBarHeight;
    // Use this for initialization
    void Awake()
    {
        mainCamera = Camera.main;
        hpBarHeight *= Screen.height / 1080f;
        hpBarWidth *= Screen.width / 1920f;

    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position + height * Vector3.up;

        screenPos = mainCamera.WorldToScreenPoint(pos);
        screenPos = new Vector2(screenPos.x,Screen.height-screenPos.y);

        life = GetComponent<EnemyCommon>().life;
        lifeMax = GetComponent<EnemyCommon>().lifeMax;
        attraction = GetComponent<EnemyCommon>().getAttraction();
    }
    void OnGUI()
    {
        if ((GetComponent<EnemyCommon>().enemyStatement != 4 )&& (GetComponent<EnemyCommon>().enemyStatement != 1)&&((attraction>=50)||(life <lifeMax)))
        {
            GUI.DrawTexture(new Rect(screenPos.x - hpBarWidth / 2, screenPos.y, hpBarWidth, hpBarHeight), blood_black);
            GUI.DrawTexture(new Rect(screenPos.x - hpBarWidth / 2, screenPos.y, hpBarWidth * (life / lifeMax), hpBarHeight), blood_red);
            GUI.DrawTexture(new Rect(screenPos.x - hpBarWidth / 2, screenPos.y + hpBarHeight, hpBarWidth * (attraction / 100), hpBarHeight-1), texture_attraction);
        }
        
    }
}