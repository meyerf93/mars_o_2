using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void OnClickTime()
    {
        SceneManager.LoadScene("Brefing");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
