using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrefingManager : MonoBehaviour {

    Text budgetText;

    private void Awake()
    {
        budgetText = GameObject.Find("UpgradeBudget").GetComponent<Text>();
    }

    public void UpdateBudget(int budget)
    {
        budgetText.text = "Budget $" + budget.ToString();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickStartMission()
    {
        SceneManager.LoadScene("Flight");
    }

}
