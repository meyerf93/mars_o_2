using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public int Bonus;
	public int Bonus_to_reach;
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public int budget;
    public int InterestPoint;
    public int InterestPointToReach;

    public int ForreuseUpgrade;
    public int BoosterUpgrade;
    public int AnalyserUpgrade;
    public int ExtraFuelUpgrade;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //Initializes the game for each level.
    public void InitGame()
    {
		Bonus_to_reach = 2;
		Bonus = 0;
        budget = 1000;
        ForreuseUpgrade = 0;
        AnalyserUpgrade = 0;
        BoosterUpgrade = 0;
        ExtraFuelUpgrade = 0;
        InterestPoint = 0;
        InterestPointToReach = 3;
    }
 
    //Update is called every frame.
    void Update()
    {

    }

	public void GotWater() {
        Debug.Log("Get water !! ");
		SceneManager.LoadScene("Win");
	}

    public void OutOfEnergy(int interestPoint){
        InterestPoint = interestPoint;
		if(InterestPoint >= InterestPointToReach)
        {
			SceneManager.LoadScene("Win");
		}
		else{
			SceneManager.LoadScene("Fail");
		}
	}


}