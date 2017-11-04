using UnityEngine;
using System.Collections;


public class EnemyZombie: MonoBehaviour
{
    public bool attacking;
    public bool dead;
               
	Animator anim;                      // Reference to the animator component.
    

    void Awake ()
	{
        attacking = false;
        dead = false;
        anim = GetComponent<Animator>();
        
    }
	
	void Update ()
	{
        switch (GetComponent<EnemyCommon>().enemyStatement)
        {
            case 0://normal
                attacking = false;
                break;
            case 1://dying
                if(!dead)
                { dead = Die(); }                
                attacking = false;
                break;
            case 2: //attacking    
                attacking = true;            
                break;
            case 3://falling         
                attacking = true;
                break;
            case 4://sinking  
                attacking = true;
                break;


        }
        if (!dead)
            Animating ();
	}



    void Animating ()
	{						
		anim.SetBool ("IsAttacking", attacking);	
	}

    bool Die()
    {
        
        anim.SetTrigger("Die");
        float random = Random.Range(0f,3f);
        if (random < 1f)
            anim.SetInteger("FallOrientation", 1);
        else if (random < 2f)
            anim.SetInteger("FallOrientation", 2);
        else
            anim.SetInteger("FallOrientation", 3);
        return true;    
    }

	
}