using UnityEngine;
using System.Collections;

public class GestureCommands : MonoBehaviour {

    private Vector3 manipulationPreviousPosition;

    private GazeGestureManager gestureMan;

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // If the sphere has no Rigidbody component, add one to enable physics.
        if (!this.GetComponent<Rigidbody>())
        {
            var rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    void PerformManipulationStart(Vector3 position)
    {
        manipulationPreviousPosition = position;
    }

    void PerformManipulationUpdate(Vector3 position)
    {
        //Grab the GazeGestureManager script to reference properites
        gestureMan = transform.parent.gameObject.GetComponent<GazeGestureManager>();

        if (gestureMan.IsManipulating)
        { 
            Vector3 moveVector = Vector3.zero;
           //Calculate the moveVector as position - manipulationPreviousPosition.
            moveVector = position - manipulationPreviousPosition;

            //Update the manipulationPreviousPosition with the current position.
            manipulationPreviousPosition = position;

            //Increment this transform's position by the moveVector.
            transform.position += moveVector * 3;

        }
    }

}
