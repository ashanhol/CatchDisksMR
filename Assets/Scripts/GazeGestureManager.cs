using UnityEngine;
using UnityEngine.VR.WSA.Input;


public class GazeGestureManager : MonoBehaviour
{
    public static GazeGestureManager Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    public bool IsManipulating { get; private set; }

    // Use this for initialization
    void Awake()
    {
        Instance = this;

        // Set up a GestureRecognizer to detect Manipulation gestures.
        recognizer = new GestureRecognizer();

        // Add the ManipulationTranslate GestureSetting to the recognizer's RecognizableGestures.
        recognizer.SetRecognizableGestures(
            GestureSettings.ManipulationTranslate);

        // Register for the Manipulation events on the ManipulationRecognizer.
        recognizer.ManipulationStartedEvent += Recognizer_ManipulationStartedEvent;
        recognizer.ManipulationUpdatedEvent += Recognizer_ManipulationUpdatedEvent;
        recognizer.ManipulationCompletedEvent += Recognizer_ManipulationCompletedEvent;
        recognizer.ManipulationCanceledEvent += Recognizer_ManipulationCanceledEvent;


        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };

        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }


    //Manipulation events
    private void Recognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        if (FocusedObject != null)
        {
            IsManipulating = true;

            FocusedObject.SendMessageUpwards("PerformManipulationStart", position);
        }
    }

    private void Recognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        if (FocusedObject != null)
        {
            IsManipulating = true;

            FocusedObject.SendMessageUpwards("PerformManipulationUpdate", position);
        }
    }

    private void Recognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        IsManipulating = false;
    }

    private void Recognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        IsManipulating = false;
    }

}