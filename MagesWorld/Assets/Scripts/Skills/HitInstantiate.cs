using UnityEngine;
using System.Collections;

public class HitInstantiate : MonoBehaviour {
    public GameObject Hit;
    void OnDestroy()
    {
        Instantiate(Hit,transform.position,transform.rotation);
    }

}
