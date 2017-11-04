using UnityEngine;
using System.Collections;

public class HitDestroy : MonoBehaviour {

	public float destroyTime;
	void Awake () {
        Invoke("DestroyThis", destroyTime);

    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
