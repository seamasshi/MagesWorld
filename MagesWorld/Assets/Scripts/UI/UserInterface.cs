using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour
{
    //Camera mainCamera;
    public Texture2D health_red;
    public Texture2D health_black;
    public Texture2D health_frame;
    public Texture2D villager;
    public Texture2D[] Slots = new Texture2D[5];  // 0 - empty 1- fire 2-lightning 3-ice 4 air;


    public float healthBarOffset;
    [Tooltip("position of UI healthbar -rate to screen resolution")]
    public Rect HealthBarRect;
    Rect HealthRect;
        

    [Tooltip("position of UI Villagers -rate to screen resolution")]
    public Rect villagerStartRect;
    Rect villagerCurrentRect; // an assistant parameter for UI villagers
    int villagerNum;

    float healthPoint;
    float healthPointMax;

    int[] elementSlots = new int[4];
    [Tooltip("position of UI Element Slots -rate to screen resolution")]
    public Rect[] slotRect = new Rect[4];

    // Use this for initialization
    void Awake()
    {
       // mainCamera = Camera.main;
        healthPoint = GetComponent<CharacterState>().healthPoint;
        healthPointMax = GetComponent<CharacterState>().healthPointMax;
        villagerNum = GetComponent<CharacterState>().GetVillagerNum();

        HealthBarRect.x *= Screen.width;
        HealthBarRect.y *= Screen.height;
        HealthBarRect.width *= Screen.width;
        HealthBarRect.height *= Screen.height;

        villagerStartRect.x *= Screen.width;
        villagerStartRect.y *= Screen.height;
        villagerStartRect.width *= Screen.width;
        villagerStartRect.height *= Screen.height;

        
        HealthRect = HealthBarRect;

        elementSlots = GetComponent<CharacterState>().Out_ElementSlots();

        for (int i = 0; i < 4; i++)
        {
            slotRect[i].x *= Screen.width;
            slotRect[i].y *= Screen.height;
            slotRect[i].width *= Screen.width;
            slotRect[i].height *= Screen.height;
        }
        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
    }

    // Update is called once per frame
    void Update()
    {

        healthPoint = GetComponent<CharacterState>().healthPoint;
        healthPointMax = GetComponent<CharacterState>().healthPointMax;
        villagerNum = GetComponent<CharacterState>().GetVillagerNum();
        HealthRect.xMax = HealthBarRect.xMin + healthBarOffset * Screen.width + (HealthBarRect.xMax - HealthBarRect.xMin - 2*healthBarOffset * Screen.width) * healthPoint / healthPointMax;
        elementSlots = GetComponent<CharacterState>().Out_ElementSlots();
    }

    void OnGUI()
    {
        GUI.DrawTextureWithTexCoords(HealthBarRect, health_black, new Rect(0, 0, 1, 1));
        GUI.DrawTextureWithTexCoords(HealthRect, health_red, new Rect(0, 0, (HealthRect.xMax - HealthRect.xMin) / (HealthBarRect.xMax-HealthBarRect.xMin), 1));
        GUI.DrawTextureWithTexCoords(HealthBarRect, health_frame, new Rect(0, 0, 1, 1));
        for (int i = 0; i < villagerNum; i++)
        {
            villagerCurrentRect = villagerStartRect;
            villagerCurrentRect.x += Screen.width * 0.020f * i;
            GUI.DrawTextureWithTexCoords(villagerCurrentRect, villager, new Rect(0, 0, 1, 1));
        }
        for (int i = 0; i < 4; i++)
        {
            GUI.DrawTextureWithTexCoords(slotRect[i], Slots[elementSlots[i]], new Rect(0, 0, 1, 1));
        }
            //GUI.DrawTextureWithTexCoords(villagerStartRect, villager, new Rect(0, 0, 1, 1));

    }
}