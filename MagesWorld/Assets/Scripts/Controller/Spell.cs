using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {
    int[] elementSlots = new int[4];
    int magicShape;
    int[] elementnum = new int[5];  //denote how much each element in slots ,calculated in DetectConflict() 

    //=======================================
    //Spell prefebs

    //waves
    public GameObject fireWave;   
    public GameObject lightningWave;
    public GameObject iceWave;
    public GameObject airWave;
    //tornados
    public GameObject fireArea;
    public GameObject flameStrike;
    public GameObject lightningBurst;
    public GameObject iceBreak;
    public GameObject iceArea;
    public GameObject vacuum;
    //balls
    public GameObject fireBolt;
    public GameObject fireBall;
    public GameObject lightningBolt;
    public GameObject iceArrow;

    //Spell prefebs
    //=======================================


    //Positions for spell instantiate
    Vector3 mousePoint;
    public Transform startPoint;
    
    GameObject currentObject;  //handle of newly instantiated spell

    void Update () {
        //mousePoint = GetComponent<MouseMove>().mousepoint; // keep track of mouse	
	}

    public int MergeElements()//return the index of the coming spell
    {
        // get elements and shape form character
        elementSlots = GetComponent<CharacterState>().Out_ElementSlots();
        magicShape = GetComponent<CharacterState>().Out_MagicShape();

        if (DetectConflict())// if there is not element conflict
        {
            mousePoint = GetComponent<MouseMove>().mousepoint;
            switch (magicShape)
            {
                //no Shape
                case 0:
                    break;
                //Wave
                case 1:
                    // pure fire wave
                    if ((elementnum[1] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return 11;
                    }
                    // pure lightning wave
                    if ((elementnum[2] > 0) && (elementnum[1] == 0) && (elementnum[3] == 0))
                    {
                        return 21;
                    }
                    // pure ice wave
                    if ((elementnum[3] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return 31;
                    }
                    if ((elementnum[4] > 0) && (elementnum[3] == 0) && (elementnum[1] == 0))
                    {
                        return 41;
                    }
                    break;
                //Tornado
                case 2:
                    // pure fire tornado
                    if ((elementnum[1] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return 12;
                    }
                    // tornado, fire(main) combined with air(secondary)
                    if ((elementnum[1] > 0) && (elementnum[2] == 0) && (elementnum[4] > 0) && (elementnum[1] >= elementnum[4]))
                    {
                        return 412;
                    }
                    // pure lightning tornado
                    if ((elementnum[2] > 0) && (elementnum[1] == 0) && (elementnum[3] == 0))
                    {
                        return 22;
                    }
                    // pure ice tornado
                    if ((elementnum[3] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return 32;
                    }
                    if ((elementnum[3] > 0) && (elementnum[2] == 0) && (elementnum[4] > 0))
                    {
                        return 432;
                    }
                    if ((elementnum[4] > 0) && (elementnum[3] == 0) && (elementnum[1] == 0))
                    {
                        return 42;
                    }
                    break;
                //Bolt
                case 3:
                    // pure fire ball
                    if ((elementnum[1] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return 13;
                    }
                    //bolt, fire(main) combined with air(secondary)
                    if ((elementnum[1] > 0) && (elementnum[2] == 0) && (elementnum[4] > 0))
                    {
                        return 413;
                    }
                    //pure lightning ball
                    if ((elementnum[2] > 0) && (elementnum[1] == 0) && (elementnum[3] == 0))
                    {
                        return 23;
                    }
                    // pure ice ball
                    if ((elementnum[3] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return 33;
                    }

                    break;
            }
            return 0;
        }
        else
        {
            //some punishment for element conflict
            return 0;
        }

    }
    public int Cast() //determine the type of spells and their parameters.
    {        
            
            switch (magicShape)
            {
                //no Shape
                case 0:
                    break;
                //Wave
                case 1:
                    // pure fire wave
                    if ((elementnum[1] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return FireWave(0.6f + elementnum[1] * 0.4f, 3, 5 * (0.6f + elementnum[1] * 0.4f));
                    }
                    // pure lightning wave
                    if ((elementnum[2] > 0) && (elementnum[1] == 0) && (elementnum[3] == 0))
                    {
                        return LightningWave(0.6f + elementnum[3] * 0.4f);
                    }
                    // pure ice wave
                    if ((elementnum[3] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return IceWave(0.6f + elementnum[3] * 0.4f, 2 * (0.4f + elementnum[3] * 0.6f), 0.5f);
                    }
                    if ((elementnum[4] > 0) && (elementnum[3] == 0) && (elementnum[1] == 0))
                    {
                        return AirWave(0.7f + elementnum[4] * 0.3f);
                    }
                break;
                //Tornado
                case 2:
                    // pure fire tornado
                    if ((elementnum[1] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return FireArea(0.6f + elementnum[1] * 0.4f, 0.8f + elementnum[1] * 0.2f, 3, 5*(0.6f + elementnum[1] * 0.4f)); 
                    }
                    // tornado, fire(main) combined with air(secondary)
                    if ((elementnum[1] > 0) && (elementnum[2] == 0) && (elementnum[4] > 0) && (elementnum[1] >= elementnum[4]))                    
                    {                       
                        return FlameStrike(0.5f*(elementnum[1]+ elementnum[4]));                                                
                    }
                    // pure lightning tornado
                    if ((elementnum[2] > 0) && (elementnum[1] == 0) && (elementnum[3] == 0))
                    {
                        return LightningBurst(0.6f + elementnum[3] * 0.4f);
                    }
                    // pure ice tornado
                    if ((elementnum[3] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return IceBreak(0.6f + elementnum[3] * 0.4f, 2 * (0.4f + elementnum[3] * 0.6f),0.9f);
                    }
                    //tornado, ice(main) combined with air(secondary)
                    if ((elementnum[3] > 0) && (elementnum[2] == 0) && (elementnum[4] > 0))
                    {
                        return IceArea(0.6f + elementnum[3] * 0.4f, 0.6f + elementnum[4] * 0.4f, 0.5f, 0.95f);
                    }
                    //pure air tornado
                    if ((elementnum[4] > 0) && (elementnum[3] == 0) && (elementnum[1] == 0))
                    {
                        return Vacuum(0.6f + elementnum[4] * 0.4f, 0.6f + elementnum[4] * 0.4f);
                    }
                    break;
                //Bolt
                case 3:
                    // pure fire ball
                    if ((elementnum[1] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return FireBall(0.6f + elementnum[1] * 0.4f, 3, 5 * (0.6f + elementnum[1] * 0.4f)); 
                    }
                    //bolt, fire(main) combined with air(secondary)
                    if ((elementnum[1] > 0)&&(elementnum[2] == 0) && (elementnum[4] > 0))
                    {
                        return FireBolt(0.5f * (elementnum[1] + elementnum[4])); 
                    }
                    //pure lightning ball
                    if ((elementnum[2] > 0) && (elementnum[1] == 0) && (elementnum[3] == 0))
                    {
                        return LightningBolt(0.6f + elementnum[2] * 0.4f); 
                    }
                    // pure ice ball
                    if ((elementnum[3] > 0) && (elementnum[2] == 0) && (elementnum[4] == 0))
                    {
                        return IceArrow(0.6f + elementnum[3] * 0.4f, 3 * (0.6f + elementnum[3] * 0.4f), 0.3f);
                    }

                    break;
            }
        return 0;        
             
    }


    int FireWave(float damagemodi, float igniteTime, float igniteDamage)
    {
        currentObject = GameObject.Instantiate(fireWave);
        currentObject.transform.position = startPoint.position;
        currentObject.transform.rotation = startPoint.rotation;
        currentObject.GetComponent<WaveDamage>().damage *= damagemodi;
        currentObject.GetComponent<WaveDamage>().SetIgnite(igniteTime, igniteDamage);
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 11;
    }

    int LightningWave(float damagemodi)
    {
        currentObject = GameObject.Instantiate(lightningWave);
        currentObject.transform.position = startPoint.position;
        currentObject.transform.rotation = startPoint.rotation;
        currentObject.GetComponent<WaveDamage>().damage *= damagemodi;        
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 21;
    }
    int IceWave(float damagemodi, float slowTime, float slowRate)
    {
        currentObject = GameObject.Instantiate(iceWave);
        currentObject.transform.position = startPoint.position;
        currentObject.transform.rotation = startPoint.rotation;
        currentObject.GetComponentInChildren<WaveDamage>().damage *= damagemodi;
        currentObject.GetComponentInChildren<WaveDamage>().SetSlow(slowTime, slowRate);
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 31;
    }
    int AirWave(float forcemodi)
    {
        currentObject = GameObject.Instantiate(airWave);
        currentObject.transform.position = startPoint.position;
        currentObject.transform.rotation = startPoint.rotation;
        currentObject.GetComponent<AirWave>().force *= forcemodi;
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 41;
    }

    int FireArea(float damagemodi, float timemodi, float igniteTime, float igniteDamage)
    {
        currentObject = GameObject.Instantiate(fireArea);
        currentObject.transform.position = mousePoint+Vector3.up;
        currentObject.GetComponent<ContinualDamage>().damage *= damagemodi;
        currentObject.GetComponent<ContinualDamage>().duration *= timemodi;
        currentObject.GetComponent<ContinualDamage>().SetIgnite(igniteTime, igniteDamage);
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 12; //mean: 2-tornado mode, 1-pure fire
    }

    int FlameStrike(float damagemodi)
    {
        currentObject = GameObject.Instantiate(flameStrike);
        currentObject.transform.position = mousePoint;
        currentObject.GetComponentInChildren<DealSingleDamage>().damage *= damagemodi;
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 412; //mean: 2-tornado mode, 1-main element fire, 4-secondary element air
    }

    int LightningBurst(float damagemodi)
    {
        currentObject = GameObject.Instantiate(lightningBurst);
        currentObject.transform.position = mousePoint + Vector3.up;
        currentObject.GetComponent<BurstSingleDamage>().damage *= damagemodi;
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 22;//mean: 2-tornado mode, 2-pure lightning
    }

    int IceBreak(float damagemodi, float slowTime, float slowRate)
    {
        currentObject = GameObject.Instantiate(iceBreak);
        currentObject.transform.position = mousePoint + Vector3.up;
        currentObject.GetComponent<BurstSingleDamage>().damage *= damagemodi;
        currentObject.GetComponent<BurstSingleDamage>().SetSlow(slowTime, slowRate);
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 32; //mean: 2-tornado mode, 3-pure ice
    }

    int IceArea(float damagemodi, float timemodi, float slowTime, float slowRate)
    {
        currentObject = GameObject.Instantiate(iceArea);
        currentObject.transform.position = mousePoint;
        currentObject.GetComponent<ContinualDamage>().damage *= damagemodi;
        currentObject.GetComponent<ContinualDamage>().duration *= timemodi;
        currentObject.GetComponent<ContinualDamage>().SetSlow(slowTime, slowRate);
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 432; //mean: 2-tornado mode, 1-pure fire
    }

    int Vacuum(float timemodi, float forcemodi)
    {
        currentObject = GameObject.Instantiate(vacuum);
        currentObject.transform.position = mousePoint + Vector3.up *0.48f;
        currentObject.GetComponent<Vacuum>().duration *= timemodi;
        currentObject.GetComponent<Vacuum>().force *= forcemodi;
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 42; 
    }

    int FireBall(float damagemodi, float igniteTime, float igniteDamage)
    {
        currentObject = GameObject.Instantiate(fireBall);
        currentObject.transform.position = startPoint.position;
        currentObject.transform.rotation = startPoint.rotation;
        currentObject.GetComponent<DealPenetratingDamage>().damage *= damagemodi;        
        currentObject.GetComponent<DealPenetratingDamage>().SetIgnite(igniteTime, igniteDamage);
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 13;
    }

    int FireBolt(float damagemodi)
    {
        currentObject = GameObject.Instantiate(fireBolt);
        currentObject.transform.position = startPoint.position;
        currentObject.transform.rotation = startPoint.rotation;
        currentObject.transform.localScale = new Vector3(0.5f * damagemodi, 0.5f * damagemodi, 0.5f * damagemodi) ;
        currentObject.GetComponentInChildren<DealSingleDamage>().damage *= damagemodi;        
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 413;
        
    }

    int LightningBolt(float damagemodi)
    {
        currentObject = GameObject.Instantiate(lightningBolt);
        currentObject.transform.position = startPoint.position;
        currentObject.transform.rotation = startPoint.rotation;
        currentObject.GetComponent<DealSingleDamage>().damage *= damagemodi;
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 23;

    }

    int IceArrow(float damagemodi, float slowTime, float slowRate)
    {
        currentObject = GameObject.Instantiate(iceArrow);
        currentObject.transform.position = startPoint.position;
        currentObject.transform.rotation = startPoint.rotation;
        currentObject.GetComponent<DealSingleDamage>().damage *= damagemodi;
        currentObject.GetComponent<DealSingleDamage>().SetSlow(slowTime,slowRate);
        GetComponent<CharacterState>().ClearElementSlots();
        GetComponent<CharacterState>().callMagicShape(0);
        return 33;
    }

 

    bool DetectConflict() //true - no conflict  false - conflict
    {
        for (int i = 0; i < 5; i++)
        {
            elementnum[i] = 0;
        }
        for (int i = 0; i < 4; i++)
        {
            elementnum[elementSlots[i]]++;
        }

        if ((elementnum[1] > 0) && (elementnum[3] > 0))
        {
            return false;
        }

        if ((elementnum[2] > 0) && (elementnum[4] > 0))
        {
            return false;
        }
        return true;
    }
}
