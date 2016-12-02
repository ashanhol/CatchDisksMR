using UnityEngine;
using System.Collections;

public class ObjectActions : MonoBehaviour {

    private GameController gameController;

    // Use this for initialization
    void Start()
    {

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Point")
        {
           gameController.AddScore();

        }
    }
}
