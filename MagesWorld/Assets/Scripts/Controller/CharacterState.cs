using UnityEngine;
using System.Collections;

public class CharacterState : MonoBehaviour {
    public float healthPoint;
    public float healthPointMax;
    public float healthRecover;
    bool dead;
    float respawnInvincible; // invincible Time remaining  , be set after respawn
    float deadTickTime;
    public float deadTime;

    public int villagerNumMax;
    int villagerNum;

    //float energy;
    //public float energyMax;

    int elementFire;            //element #1
    int elementLightning;       //element #2
    int elementIce;             //element #3
    int elementAir;             //element #4

    public int elementFireMax;
    public int elementLightningMax;
    public int elementIceMax;
    public int elementAirMax;

    [Tooltip("Elements automatically recover once per seconds, this denotes how many points will you get when recover")]
    public int elementAutoRecoverRate;
    float time;                         // record the time of this frame, used to identify if it is time to recover 

    int elementCountInSlots;            //to count how many elements was called
    int[] elementSlots = new int[4];    // the slots showing elements called
    int magicShape;                     //1-wave 2-tornado 3-bolt 

    

    // Objects for controlling the globes to show elements remain
    public GameObject globeFire;
    public GameObject globeLightning;
    public GameObject globeIce;
    public GameObject globeAir;
    Renderer rendFire;
    Renderer rendLightning;
    Renderer rendIce;
    Renderer rendAir;

    // Use this for initialization
    void Awake () {

        //initialize all the variables and objects
        healthPoint = healthPointMax;
        villagerNum = villagerNumMax;
        //energy = 0;
        elementFire = elementFireMax;
        elementLightning = elementLightningMax;
        elementIce = elementIceMax;
        elementAir = elementAirMax;

        time = 0;
        elementCountInSlots = 0;
        ClearElementSlots();
        magicShape = 0;

        rendFire = globeFire.GetComponent<Renderer>();
        rendLightning = globeLightning.GetComponent<Renderer>();
        rendIce = globeIce.GetComponent<Renderer>();
        rendAir = globeAir.GetComponent<Renderer>();
        dead = false;

        deadTickTime = 0;

        respawnInvincible = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        UpdateHealth();
        elementCountInSlots = CountElementInSlots();
        if (dead)
        {
            deadTickTime -= Time.deltaTime;
            if (deadTickTime <= 0)
            {
                dead = false;
                deadTickTime = 0;
                respawnInvincible = 3;
            }
        }
        else
        {
            ElementAutoRecover();
        }
        if (respawnInvincible > 0)
        {
            respawnInvincible -= Time.deltaTime;
        }
        

        rendFire.material.SetFloat("_Progress", (float)elementFire / elementFireMax);
        rendLightning.material.SetFloat("_Progress", (float)elementLightning / elementLightningMax);
        rendIce.material.SetFloat("_Progress", (float)elementIce / elementIceMax);
        rendAir.material.SetFloat("_Progress", (float)elementAir / elementAirMax);

    }

    // functions to change variables
    public int Out_MagicShape()
    {
        return magicShape;
    }
    public void callMagicShape(int shapeNumber)
    {
        magicShape = shapeNumber;
    }


    public bool consumeFireElement(int consume)
    {
        if (elementFire < consume)
        {
            return false;
        }
        else
        {
            if (elementCountInSlots < elementSlots.Length)
            {
                elementFire -= consume;
                return true;
            }
            else
            {
                return false;
            }
                
        }            
    }

    public bool consumeLightningElement(int consume)
    {
        if (elementLightning < consume)
        {
            return false;
        }
        else
        {
            if (elementCountInSlots < elementSlots.Length)
            {
                elementLightning -= consume;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool consumeIceElement(int consume)
    {
        if (elementIce < consume)
        {
            return false;
        }
        else
        {
            if (elementCountInSlots < elementSlots.Length)
            {
                elementIce -= consume;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool consumeAirElement(int consume)
    {
        if (elementAir < consume)
        {
            return false;
        }
        else
        {
            if (elementCountInSlots < elementSlots.Length)
            {
                elementAir -= consume;
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    public int[] Out_ElementSlots()
    {
        return   elementSlots;
    }

    public bool In_ElementSlots(int elementIndex)
    {
        for (int i = 0; i < 4; i++)
        {
            if (elementSlots[i] == 0)
            {
                elementSlots[i] = elementIndex;
                return true;
            }
        }
        return false;
    }

    public int Out_ElementCountInSlots()
    {
        return elementCountInSlots;
    }


    int CountElementInSlots()
    {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            if (elementSlots[i] != 0)
            {
                count++;
            }
        }
        return count;
    }


    public void ClearElementSlots()
    {
        for (int i = 0; i < 4; i++)
        {
            elementSlots[i] = 0;
        }
    }

    public void RecoverElement(int type, int value)
    {
        switch (type)
        {
            case 1:
                elementFire += value;
                elementFire = (elementFire > elementFireMax ? elementFireMax : elementFire);
                break;
            case 2:
                elementLightning += value;
                elementLightning = (elementLightning > elementLightningMax ? elementLightningMax : elementLightning);
                break;
            case 3:
                elementIce += value;
                elementIce = (elementIce > elementIceMax ? elementIceMax : elementIce);
                break;
            case 4:
                elementAir += value;
                elementAir = (elementAir > elementAirMax ? elementAirMax : elementAir);
                break;
        }
    }

    void ElementAutoRecover()
    {
        if ((Time.time - time) > 1)
        {
            for (int i = 1; i <= 4; i++)
            {
                RecoverElement(i, elementAutoRecoverRate);
                time = Time.time;
            }
        }        
        
    }

    void UpdateHealth ()
    {
        if (healthPoint < 0)
        {
            PlayerDie();
            healthPoint = 0;
        }
        healthPoint += healthRecover * Time.deltaTime;
        if (healthPoint > healthPointMax)
        {
            healthPoint = healthPointMax;
        }      
    }

    public void GetDamage(float value)
    {
        if((!dead)&&(respawnInvincible <= 0))
        healthPoint -= value;
    }

    public void GetHeal(float value)
    {
        if (!dead)
        healthPoint += value;
        if (healthPoint > healthPointMax)
        {
            healthPoint = healthPointMax;
        }
    }

    void PlayerDie()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().PlayerDie();
        dead = true;
        deadTickTime = deadTime;
        elementFire = 0;
        elementLightning = 0;
        elementIce = 0;
        elementAir = 0;
    }

    public bool GetDeadState()
    {
        return dead;
    }

    public int GetVillagerNum()
    {
        return villagerNum;

    }

    public void DecreaseVillagerNum(int num)
    {
        villagerNum -= num;
        if (villagerNum <= 0)
        {
            villagerNum = 0;
            Application.LoadLevel(0);
        }
    }

    public void ResetVillagerNum()
    {
        villagerNum = villagerNumMax;
    }
}
