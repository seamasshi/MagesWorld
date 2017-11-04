using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    public GameObject gameController; 

	public GameObject[] pawns;
    public GameObject boss_1;
    
	public Transform[] spawnPoints;
    public float[] spawnPointsOpen; // time remaining before able to spawn next one.
    public Transform bossPoint;

    public Queue<int> delayedPawns;

    public float time;

    int timenodeindex;
    public float[] timenodes;
    public int[] spawnnum;


	// Use this for initialization
	void Start () 
	{
        
        delayedPawns = new Queue<int>();
        timenodeindex = 0;
        time = 0;
        spawnPointsOpen = new float[spawnPoints.Length];
        for (int i = 0; i < spawnPointsOpen.Length; i++)
        {
            spawnPointsOpen[i] = 0;
        }
	}

	

	// Update is called once per frame
	void FixedUpdate () {
        
        time += Time.deltaTime;
        if (timenodeindex < timenodes.Length)
        {
            if (time >= timenodes[timenodeindex])
            {
                timenodeindex++;
                if (timenodeindex == timenodes.Length)
                {
                    SpawnBoss(boss_1);
                }
                else
                {
                    AddPawns(0, spawnnum[timenodeindex - 1]);
                }


            }
        }
        else
        {
            CheckWin();
        }
        SpawnPawns();
       
        
	}

    void AddPawns(int pawnindex, int num)
    {
        for (int i = 0; i < num; i++)
        {
            delayedPawns.Enqueue(pawnindex);
        }
               
    }

    void SpawnPawns()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPointsOpen[i] == 0)
            {
                if (delayedPawns.Count > 0)
                {
                    Instantiate(pawns[delayedPawns.Dequeue()], spawnPoints[i].position, spawnPoints[i].rotation);
                    spawnPointsOpen[i] = 3.5f;
                }
                
            }
            else
            {
                if (spawnPointsOpen[i] > 0)
                {
                    spawnPointsOpen[i] -= Time.deltaTime;
                }                
                if (spawnPointsOpen[i] < 0)
                {
                    spawnPointsOpen[i] = 0;
                }
            }
        }          

    }

    void SpawnBoss(GameObject boss)
    {
        Instantiate(boss, bossPoint.position, bossPoint.rotation);
        //gameController.GetComponent<UIController>().StartDialog();
    }

    void CheckWin()
    {
        GameObject enemyLeft = GameObject.FindGameObjectWithTag("Enemy");
        if (enemyLeft == null)
        {
            Debug.Log("Win");
            Application.LoadLevel(2);
        }

    }
}
