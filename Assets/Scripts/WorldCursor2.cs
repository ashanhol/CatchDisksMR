using UnityEngine;

public class WorldCursor2 : MonoBehaviour
{
    private Material material;

    private GazeGestureManager gestureMan;

    // Use this for initialization
    void Start()
    {
        // Grab the material that's on the same object as this script.
        material = this.GetComponent<Renderer>().material;
        //Grab the gesture manager. 
        gestureMan = GameObject.FindGameObjectWithTag("GameController").GetComponent<GazeGestureManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram...
            // Make cursor green (or blue if manipulating)
            if(gestureMan.IsManipulating)
                material.color = Color.blue;
            else
            {
                material.color = Color.green;
            }
            // Move the cursor to the point where the raycast hit.
            this.transform.position = hitInfo.point;

        }
        else
        {
            // If the raycast did not hit a hologram, make cursor red/not visible
            this.transform.position = headPosition;
            material.color = Color.red;
        }
    }
}