using UnityEngine;
using System.Collections;

public class TriggerOutScene : MonoBehaviour {

    // Use this for initialization
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
