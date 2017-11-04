using UnityEngine;
using System.Collections;


public class KeyboardInput : MonoBehaviour
{
    public float spellingTime_Wave, spellingTime_Tornado, spellingTime_Bolt;
    public GameObject player;
    float lockingTime; // to lock all the operation.
    public bool locked;
    bool needCast;
    
    float callingLockTime;
    
    // Use this for initialization
    void Start()
    {
        
        lockingTime = 0;
        locked = false;
        callingLockTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<UIController>().InterfaceProcess();
        }
        if (lockingTime > 0)
        {
            lockingTime -= Time.deltaTime;
            locked = true;
        }
        else
        {
            if (locked)
            {
                locked = false;
                if (needCast)
                {
                    GetComponent<Spell>().Cast();
                    needCast = false;
                }
                
                player.GetComponent<PlayerMovement>().EndCastSpell();
            }
            lockingTime = 0;
            if (callingLockTime <= 0)
            {
                if (Input.GetKeyDown("q"))
                {
                    if (GetComponent<CharacterState>().consumeFireElement(10))
                        GetComponent<CharacterState>().In_ElementSlots(1);
                }
                if (Input.GetKeyDown("w"))
                {
                    if (GetComponent<CharacterState>().consumeLightningElement(10))
                        GetComponent<CharacterState>().In_ElementSlots(2);
                }
                if (Input.GetKeyDown("e"))
                {
                    if (GetComponent<CharacterState>().consumeIceElement(10))
                        GetComponent<CharacterState>().In_ElementSlots(3);
                }
                if (Input.GetKeyDown("r"))
                {
                    if (GetComponent<CharacterState>().consumeAirElement(10))
                        GetComponent<CharacterState>().In_ElementSlots(4);
                }
            }
            else
            {
                callingLockTime -= Time.deltaTime;
            }  
            if (Input.GetKeyDown("a"))
            {
                player.GetComponent<PlayerMovement>().CastSpell();
                GetComponent<CharacterState>().callMagicShape(1);
            }

            if (Input.GetKeyDown("s"))
            {
                player.GetComponent<PlayerMovement>().CastSpell();
                GetComponent<CharacterState>().callMagicShape(2);
            }

            if (Input.GetKeyDown("d"))
            {
                player.GetComponent<PlayerMovement>().CastSpell();
                GetComponent<CharacterState>().callMagicShape(3);
            }

            
        }
        
    }

    void FixedUpdate()
    {
        if (lockingTime <= 0)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                player.GetComponent<PlayerMovement>().MoveTo(GetComponent<MouseMove>().mousepoint);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                switch (GetComponent<Spell>().MergeElements() % 10)
                {
                    case 0:
                        GetComponent<CharacterState>().ClearElementSlots();
                        GetComponent<CharacterState>().callMagicShape(0);
                        player.GetComponent<PlayerMovement>().EndCastSpell();
                        break;
                    case 1:
                        SetLockingTime(spellingTime_Wave,true);
                        player.GetComponent<PlayerMovement>().SetCast(1);
                        break;
                    case 2:
                        SetLockingTime(spellingTime_Tornado,true);
                        player.GetComponent<PlayerMovement>().SetCast(2);
                        break;
                    case 3:
                        SetLockingTime(spellingTime_Bolt,true);
                        player.GetComponent<PlayerMovement>().SetCast(3);
                        break;
                }
            }
        } 
    }

    public void SetLockingTime (float time, bool cast)//set a period of time in which player cannot do any thing  
    {
        
        needCast = cast;
        lockingTime = time;
    }

    public void SetCallingLock(float time) //set a period of time in which player cannot call elements 
    {
        callingLockTime = time;

    }
}