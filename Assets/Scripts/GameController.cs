using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject fallingObject;

    public Text Score;

    private int score; 

    // Use this for initialization
    void Start()
    {
        score = 0;
        Score.text = "Score: " + score.ToString();
        Instantiate(fallingObject, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z+2), Random.rotation);

        StartCoroutine(SpawnWaves());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnWaves()
    {
        var cam = Camera.main;
        while(true)
        { 
            Vector3 spawnPosition = new Vector3(Random.Range(cam.transform.position.x - 1, cam.transform.position.x + 1), cam.transform.position.y + 1.4f, Random.Range(cam.transform.position.z, cam.transform.position.z + 3));
     
            Instantiate(fallingObject, spawnPosition, Random.rotation);

            yield return new WaitForSeconds(3f);
        }
    }

    public void AddScore()
    {
        score++;
        Score.text = "Score: " + score.ToString();
    }
}
