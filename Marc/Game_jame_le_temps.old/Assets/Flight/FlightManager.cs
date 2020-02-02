using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlightManager : MonoBehaviour {

    public float duration;
    float elapsed;

    public GameObject Mars;
    public GameObject MarsStartPos;
    public GameObject Rocket;
    public GameObject Blink;

    Vector3 endPosition;
    Vector3 startPosition;
    Vector3 endScale;
    Vector3 startScale;

	// Use this for initialization
	void Start () {
        endScale = Mars.transform.localScale;
        endPosition = Mars.transform.position;
        startScale = MarsStartPos.transform.localScale;
        startPosition = MarsStartPos.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        float ratio = elapsed / duration;
        Mars.transform.localScale = Vector3.Lerp(startScale, endScale, ratio);
        Mars.transform.position = Vector3.Lerp(startPosition, endPosition, ratio);
        Blink.SetActive((int)(ratio * 10) % 2 == 0);
        if (elapsed >= duration)
        {
            //SceneManager.LoadScene("Flight");
            SceneManager.LoadScene("Game");
            Destroy(this);
        }
	}
}
