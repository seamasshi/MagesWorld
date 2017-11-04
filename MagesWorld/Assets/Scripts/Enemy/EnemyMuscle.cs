using UnityEngine;
using System.Collections;

public class EnemyMuscle : MonoBehaviour {

    public bool attacking;
    public bool running;
    public bool walking;
    public bool dead;
    public bool spawing;
    Animator anim;

    void Awake()
    {
        
        attacking = false;
        dead = false;
        anim = GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetComponent<EnemyCommon>().enemyStatement);
        switch (GetComponent<EnemyCommon>().enemyStatement)
        {
            case 0://normal
                walking = true;
                running = false;
                attacking = false;
                spawing = false;
                break;
            case 1://dying
                if (!dead)
                { dead = Die(); }
                walking = false;
                running = false;                
                attacking = false;
                spawing = false;
                break;
            case 2: //attacking
                walking = false;
                running = false;
                attacking = true;
                spawing = false;
                break;
            case 3://falling         
                attacking = true;
                break;
            case 4://sinking  
                attacking = true;
                break;
            case 5://Spawing 
                walking = false;
                running = false;
                attacking = false;
                spawing = true;
                break;
            case 6://gothit
                GotHit();
                break;
        }
        if (!dead)
            Animating();
    }
    bool Die()
    {
        anim.SetTrigger("Die");
        GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>().ChangeAudioTo(0);
        return true;
        
    }
    void GotHit()
    {
        anim.SetTrigger("GotHit");
    }
    void Animating()
    {
        anim.SetBool("IsAttacking", attacking);
        anim.SetBool("IsWalking", walking);
        anim.SetBool("IsRunning", running);
        anim.SetBool("IsSpawing", spawing);
    }
}
