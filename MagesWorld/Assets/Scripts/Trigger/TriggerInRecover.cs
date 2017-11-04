using UnityEngine;
using System.Collections;

public class TriggerInRecover : MonoBehaviour {

    public GameObject recoverBuff;
    GameObject gameController;
    public int elementRecoverRate;
    public float recoverDelay;
    float tickTime;
    float EnterTime;
    void Start()
    {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0];
        tickTime = Time.time;
        EnterTime = 0;
    }

   

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (EnterTime == 0)
            {
                EnterTime = Time.time;
            }
            
            if (!gameController.GetComponent<CharacterState>().GetDeadState())
            {
                if (Time.time - EnterTime > recoverDelay)
                {
                    collider.GetComponent<PlayerMovement>().SetPlayerRecover(true);
                    if (Time.time - tickTime > 1)
                    {
                        tickTime = Time.time;
                        Instantiate(recoverBuff, collider.transform.position, collider.transform.rotation);
                        gameController.GetComponent<CharacterState>().GetHeal(3);
                        for (int i = 1; i <= 4; i++)
                        {
                            gameController.GetComponent<CharacterState>().RecoverElement(i, elementRecoverRate);
                        }
                        gameController.GetComponent<KeyboardInput>().SetCallingLock(2);
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            EnterTime = 0;
            collider.GetComponent<PlayerMovement>().SetPlayerRecover(false);
        }
    }
}
