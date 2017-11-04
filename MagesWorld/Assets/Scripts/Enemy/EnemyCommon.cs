using UnityEngine;
using System.Collections;

public class EnemyCommon : MonoBehaviour {

    public Material slowedMaterial;
    public Material normalMaterial;
    public int villagerKillingAbility;
    float speed;
    [Tooltip("Moving speed of this enemy in normal statement")]
    public float basicSpeed;
    [Tooltip("Rotation speed of this enemy")]
    public float rotationSpeed;
    public float attackDistance;

    [Tooltip("0 - time before dealing damage  1 - time after dealing damage")]
    public float[] attackInterval = new float[2]; 
    [Tooltip("Damage per attack of this enemy, 0 if it has an attack effect")]
    public float attackDamage;
    float attackingTime;// time from start of attack
    bool attackingState;//true - after dealing damage  false - before dealing damage
    [Tooltip("TRUE if this enemy's attack will spawn an effect")]
    public bool attackInstantiate;
    [Tooltip("GameObject for attack effect,Null if no attack effect")]
    public GameObject attackObject;
    [Tooltip("The distance form transform to this attack effect, often equals to attackDistance ,0 if no attack effect")]
    public float attackInsPosOffset;

    [Tooltip("Z-axis of battle field boundary")]
    public float[] bridgeBoundary = new float[2];
    [Tooltip("Y-axis of Lower delete boundary")]
    public float riverboundary;
    public float lifeMax;
    public float life;
    public int enemyStatement;          // 0-normal, 1-dying, 2-attacking, 3-falling, 4-sinking 5-Spawning 6-Gothit
    public float dyingtime;
    public float spawingtime;

    float attraction; //to decide weather the enemy will attact the player.
    [Tooltip("This represents how easily an enemy will be attracted")]
    public float attractionmodi; 
    [Tooltip("In this distance enemy will be attracted")]
    public float attractDistance;
    Vector3 toplayer;
    float attractiontime; // after hit, the attraction will last for at least several seconds 

    bool hit; // used to ensure that statement 6 will last for more than one frame

    //parameters for some debuffs
    public float ignitedTime, slowedTime; //The remaining time for debuff, 0 = not debuffed.
    public float ignitedDamage;
    public float slowedRate;
    float tickTime;
    int slowedState;// 0-not slowed, 1-slowed.
    public GameObject ignited;
    GameObject ignitedObject;  

    Transform player;                   // find the position of the player
    Transform castle;                   // find the position of castle
    Transform target;                   // determine which is this enemy's goal

    public GameObject pp;

    Vector3 orientation;                //used to Turning() store the vector from this to target
    Vector3 movement;                   //used in Move()
    
    Rigidbody enemyRigidbody;           // Reference to the zombie's rigidbody.
    Collider thiscollider;             
    
    void Awake()
    {
        speed = basicSpeed;
        pp = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        castle = GameObject.FindGameObjectWithTag("Castle").transform;
        enemyRigidbody = GetComponent<Rigidbody>();
        thiscollider = GetComponent<CapsuleCollider>();
        attackingTime = 0;
        enemyStatement = 5;             // 0-normal, 1-dying, 2-attacking, 3-falling, 4-sinking 5-Spawning 6-Gothit
        life = lifeMax;
        hit = false;
        attraction = 0;
        ChangeTarget(1);

        ignitedTime = 0;
        slowedTime = 0;
        ignitedDamage = 0;
        slowedRate = 0;
        tickTime = 0;
        slowedState = 0;
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        //=============================================================
        // deal with debuffs.
        //1.ignited
        if (ignitedTime > 0)
        {
            if (ignitedObject == null)
            {
                ignitedObject = GameObject.Instantiate(ignited);
                ignitedObject.transform.position = transform.position;
                ignitedObject.transform.rotation = transform.rotation;
                ignitedObject.transform.SetParent(transform);
            }
            else
            {
                if (!ignitedObject.activeSelf)
                { ignitedObject.SetActive(true); }
                
            }
            ignitedTime -= Time.deltaTime;
            if ((Time.time - tickTime) > 1)
            {
                LifeChange(-ignitedDamage);
                tickTime = Time.time;
            }

        }
        else
        {
            if (ignitedObject != null)
            {
                if (ignitedObject.activeSelf)
                { ignitedObject.SetActive(false); }
            }               
            ignitedTime = 0;            
            ignitedDamage = 0;
            tickTime = Time.time;
        }
        //2.slowed
        if (slowedTime > 0)
        {
            if (slowedState == 0)
            {
                slowedState = 1;
                Renderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].material = slowedMaterial;
                }
            }
            slowedTime -= Time.deltaTime;            
        }
        else
        {
            if (slowedState == 1)
            {
                slowedState = 0;
                Renderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].material = normalMaterial;
                }
            }
                slowedTime = 0;
            slowedRate = 0;
        }
        speed = basicSpeed * (1 - slowedRate);
        //end deal with debuffs.
        //=============================================================


        if ((enemyStatement != 1) && (enemyStatement != 3) && (enemyStatement != 4)) //if not dying not falling not sinking
        {
            if (transform.position.z < bridgeBoundary[0] || transform.position.z > bridgeBoundary[1])
            {
                OutBridge();
            }
            if (life < 0)
            {
                Die();
            }
            AttractionChange();
        }

        switch (enemyStatement)
        {

            case 0://normal
                Turning();
                Move(orientation.x, orientation.z);
                if (orientation.magnitude <= attackDistance)
                {
                    enemyStatement = 2;
                    attackingTime = 0;
                    attackingState = false;
                }
                                
                break;
            case 1://dying
                Destroy(gameObject,dyingtime);
                break;
            case 2: //attacking
                Turning();
                AttackPlayer();
                
                break;
            case 3://falling
                if (transform.position.y < riverboundary)   //when into the river -> sinking
                {                    
                    thiscollider.enabled = true;    
                    enemyStatement = 4;
                    enemyRigidbody.drag = 14;   //sinking slowly
                }
                break;
            case 4://sinking
                if (enemyRigidbody.drag < 20)
                    enemyRigidbody.drag += 5f * Time.deltaTime; //  sinking slower
                break;
            case 5:
                Turning();
                spawingtime -= Time.deltaTime;
                if (spawingtime <= 0)
                    enemyStatement = 0;
                break;
            case 6:
                if (!hit)
                {
                    enemyStatement = 0;
                }
                else
                {
                    hit = false;
                }                
                break;
        }
       
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        enemyRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        orientation = target.position - transform.position;
        orientation.y = 0f;
        if (target == castle)
        {
            orientation.z = 0f;
        }        
        
        Quaternion newRotation = Quaternion.LookRotation(orientation);
        if (enemyStatement != 5)
        { enemyRigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed)); }
        

    }
    void OutBridge()
    {
        enemyRigidbody.constraints = RigidbodyConstraints.None;
        enemyRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        thiscollider.enabled = false;
        enemyStatement = 3;
        enemyRigidbody.drag = 1;

    }

    public void LifeChange(float value)
    {
        life += value;
    }

    public void Hit(float value)
    {
        LifeChange(-value);
        if ((enemyStatement != 5)&&(value >= 50))
        {
            enemyStatement = 6; //GotHit
            hit = true;
        }
        
        attraction += (value + 20f) * attractionmodi;
        attractiontime = 2f + value * 0.05f;
    }

    public bool ChangeTarget(int t) //change target between castle and player 0-player, 1-castle
    {
        if (t == 0)
        {
            target = player;
            return true;
        }
        if (t == 1)
        {
            target = castle;
            return true;
        }
        return false;
    }

    void AttackPlayer()
    {
        if (enemyStatement == 2 && target == player) // Make sure that this enemy is attacking player 
        {
            attackingTime += Time.deltaTime;
            if (attackingTime >= attackInterval[0])
            {
                if (!attackingState)
                {
                    if (attackInstantiate) //if this enemy has attack effect
                    {
                        Instantiate(attackObject,transform.position + transform.forward * attackInsPosOffset,transform.rotation);
                    }
                    else //normal enemy
                    {
                        if (orientation.magnitude <= attackDistance)  // deal damage only when player is in range. 
                        {
                            GameObject.FindGameObjectWithTag("GameController").GetComponent<CharacterState>().GetDamage(attackDamage);
                        }
                    }                               
                    attackingState = true;
                }
                if (attackingTime >= (attackInterval[0] + attackInterval[1]))
                {
                    if (attackingState)
                    {
                        if (orientation.magnitude > attackDistance)
                        { enemyStatement = 0; }   
                        
                        attackingState = false;
                        attackingTime = 0;
                    }
                }
            }  
        }
    }

    public void Die()
    {        
        enemyRigidbody.constraints = RigidbodyConstraints.FreezeAll;        
        thiscollider.enabled = false;
        enemyRigidbody.drag = 100;
        enemyRigidbody.mass = 100;
        enemyStatement = 1;
    }

    void AttractionChange()
    {
        toplayer = player.position - transform.position;
        if (toplayer.magnitude <= attractDistance)
        {
            if(attraction <=60 )
            attraction += Time.deltaTime * attractionmodi * (attractDistance - toplayer.magnitude) * (attractDistance - toplayer.magnitude);
        }
        else
        {           
            if ((attractiontime <= 0)&&(toplayer.magnitude >= 1.2f * attractDistance))
            {
                attraction -= Time.deltaTime * 0.4f * (toplayer.magnitude - attractDistance);
            }
        }

        if (attractiontime > 0)
        {
            attractiontime -= Time.deltaTime;
        }
        

        if (attraction > 100)
            attraction = 100;
        if (attraction < 0)
            attraction = 0;
        if (attraction >= 50)
        {
            ChangeTarget(0);
        }
        else
        {
            ChangeTarget(1);
        }    
    }

    public float getAttraction()
    {
        return attraction;
    }

    public void DebuffIgnite(float time, float damage)//add an ignite debuff on this unit
    {
        if (ignitedTime < time)
        {            
            ignitedTime = time+0.1f; // if time is an integer, ignite will invoke exactly time times.            
        }
        if (ignitedDamage < damage)
        {
            ignitedDamage = damage;
        }

    }

    public void DebuffSlow(float time, float rate)//add an slow debuff on this unit
    {
        if (slowedTime < time)
        {
            slowedTime = time;
        }
        if (slowedRate < rate)
        {
            slowedRate = rate;
        }
    }
}
