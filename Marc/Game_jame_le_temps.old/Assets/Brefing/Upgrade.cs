using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour {

    public Type type;

    public enum Type { foreuse, booster, analyser, extrafuel };

    private BrefingManager brefingManager;
    private Button plus;
    private Button moins;
    private Text level;
    private Text cost;
    private int value;

    // Use this for initialization
    void Start () {
        brefingManager = GameObject.Find("BrefingManager").GetComponent<BrefingManager>();
        brefingManager.UpdateBudget(GameManager.instance.budget);
        plus = transform.Find("Plus").GetComponent<Button>();
        moins = transform.Find("Moins").GetComponent<Button>();
        level = transform.Find("Level").GetComponent<Text>();
        cost = transform.Find("Cost").GetComponent<Text>();
        level.text = value.ToString();

        int levelCost = 0;
        if(type==Type.foreuse)
        {
            levelCost = GameSettings.ForeuseCost;
            value = GameManager.instance.ForreuseUpgrade;
        }
        else if (type == Type.booster)
        {
            levelCost = GameSettings.BoosterCost;
            value = GameManager.instance.BoosterUpgrade;
        }
        else if (type == Type.analyser)
        {
            levelCost = GameSettings.AnalyserCost;
            value = GameManager.instance.AnalyserUpgrade;
        }
        else if (type == Type.extrafuel)
        {
            levelCost = GameSettings.ExtraFuelCost;
            value = GameManager.instance.ExtraFuelUpgrade;
        }

        cost.text = "$" + levelCost;

        plus.onClick.AddListener(() =>
        {
            if (GameManager.instance.budget - levelCost >= 0)
            {
                value++;
                GameManager.instance.budget -= levelCost;
                UpdateValue();
                level.text = value.ToString();
                brefingManager.UpdateBudget(GameManager.instance.budget);
            }
        });

        moins.onClick.AddListener(() =>
        {
            if (value > 0)
            {
                value--;
                GameManager.instance.budget += levelCost;
                UpdateValue();
                level.text = value.ToString();
                brefingManager.UpdateBudget(GameManager.instance.budget);
            }
        });

    }

    private void UpdateValue()
    {
        if (type == Type.foreuse)
        {
            GameManager.instance.ForreuseUpgrade = value;
        }
        else if (type == Type.booster)
        {
            GameManager.instance.BoosterUpgrade = value;
        }
        else if (type == Type.analyser)
        {
            GameManager.instance.AnalyserUpgrade = value;
        }
        else if (type == Type.extrafuel)
        {
            GameManager.instance.ExtraFuelUpgrade = value;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
