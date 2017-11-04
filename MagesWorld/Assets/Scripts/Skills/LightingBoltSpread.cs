using UnityEngine;
using System.Collections;

public class LightingBoltSpread : MonoBehaviour {

    public GameObject spread;
    public float damage;
    public float spreadradius;
    void OnDestroy()
    {
        Collider[] spreadColliders = Physics.OverlapSphere(transform.position, spreadradius);
        for (int i = 0; i < spreadColliders.Length; i++)
        {
            if (spreadColliders[i].tag == "Enemy")
            {
                spreadColliders[i].GetComponent<EnemyCommon>().Hit(damage);
                Instantiate(spread, spreadColliders[i].transform.position + Vector3.up* spreadColliders[i].transform.localScale.y, spreadColliders[i].transform.rotation);
            }
        }
        
    }
}
