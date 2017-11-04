using UnityEngine;
using System.Collections;

public class MouseMove : MonoBehaviour {
    public int floor_y;
    public Vector3 mousepoint;
    float camRayLength = 200f;
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    


    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");
    }
    	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<KeyboardInput>().locked)
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                mousepoint = floorHit.point;
                mousepoint.y = floor_y;
            }
        } 
    }
}
