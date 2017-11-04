using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public float speedBasic = 6f;
    float speedModi;
    
    float speed;           // The speed that the player will move at.
    public MouseMove mousemove;
    public GameObject Boundary_TL, Boundary_BR;
    float[] moveboundary = new float[4]; //the boundary for movement, (lefttop_x lefttop_z rightbot_x rightbot_z)
    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.


    Vector3 moving;
    Vector3 movingTarget;
    

    GameObject gameController;
    public Transform PlayerRespawnPiont;
    
    int cast; //0 - no casting 1- cast wave 2- cast tornado 3-cast bolt
    bool running;
    
    bool recovering;
    bool die;
    public float dieTickTime;
    public float dieTime;
    bool respawn;
    public float respawnTickTime;
    public float respawnTime;
    

    void Awake()
    {
        
        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        speed = speedBasic;
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0];
        
        speedModi = 1;
        moveboundary[0] = Boundary_TL.transform.position.x;
        moveboundary[1] = Boundary_TL.transform.position.z;
        moveboundary[2] = Boundary_BR.transform.position.x;
        moveboundary[3] = Boundary_BR.transform.position.z;

        
        cast = 0;
        running = false;
        die = false;
        recovering = false;
        respawn = false;
        
    }

    void Update()
    {
        if (speedModi < 1.0f)
        {
            speedModi += Time.deltaTime * 0.5f;
        }
        else
        {
            speedModi = 1.0f;
        }        
        speed = speedModi*(speedBasic - gameController.GetComponent<CharacterState>().Out_ElementCountInSlots());


        if (die)
        {
            dieTickTime -= Time.deltaTime;
            if (dieTickTime <= 0)
            {
                dieTickTime = 0;
                die = false;
                transform.position = PlayerRespawnPiont.position;
                transform.rotation = PlayerRespawnPiont.rotation;
                movingTarget = transform.position;
                anim.SetTrigger("Respawn");
                respawn = true;
                respawnTickTime = respawnTime;
                
                
            }
        }

        if (respawn)
        {
            respawnTickTime -= Time.deltaTime;
            if (respawnTickTime <= 0)
            {
                respawn = false;
                anim.SetTrigger("Revive");
                respawnTickTime = 0;
            }
        }

    }

    void FixedUpdate()
    {
        if (cast != 0)
        {
            TurnTo(mousemove.mousepoint); // Turn the player to face the mouse cursor.
        }
        else
        {                        
            Move(moving.x, moving.z);
            if (Vector3.Distance(movingTarget, transform.position) <= 0.2f)
            {
                movingTarget = transform.position;
            }
            moving = movingTarget - transform.position;            
        }
        DetectState();
        Animating();

    }

    

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    
    public void TurnTo(Vector3 targetposition)
    {
        Vector3 playerToMouse = targetposition - transform.position;        
        playerToMouse.y = 0f;
        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);        
        playerRigidbody.MoveRotation(newRotation);
    }

    public void MoveTo(Vector3 targetposition)
    {
        if (targetposition.x <= moveboundary[2] && targetposition.z <= moveboundary[1] && targetposition.x >= moveboundary[0] && targetposition.z >= moveboundary[3])
        {
            movingTarget = targetposition;
            TurnTo(movingTarget);            
            cast = 0;
            gameController.GetComponent<CharacterState>().callMagicShape(0);
        }
        
    }
    public void CastSpell()
    {
        movingTarget = transform.position;
        cast = -1;
    }
    public void EndCastSpell()
    {
        cast = 0;
    }


    void Animating()
    {        
        
        anim.SetInteger("Cast",cast);
        if (!running)
        {
            anim.SetBool("Recovering", recovering);
        }
        else
        {
           
            anim.SetBool("Recovering", false);
        }
        anim.SetBool("IsRunning", running);
    }

    void DetectState()
    {
        running = ((moving.magnitude != 0f) && (cast == 0));
        if (running)
        {
            recovering = false;
        }
    }

    public void SetCast(int castValue)
    {
        cast = castValue;
    }

    public void SetSpeedModi(float newSpeedModi)
    {
        speedModi = newSpeedModi;
    }

    public void PlayerDie()
    {
        movingTarget = transform.position;
        anim.SetTrigger("Die");
        die = true;
        dieTickTime = dieTime;
        gameController.GetComponent<KeyboardInput>().SetLockingTime(respawnTime + dieTime,false);
    }

    public void SetPlayerRecover(bool recover)
    {
        recovering = recover;
    }
}