using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;


public class PlayerController : MonoBehaviour
{
    public enum Mode { idle, moving, jumping, deployingForreuse, undeployingForreuse, deployingAnalyser, undeployingAnalyser };
    private Mode mode;

    public float speedX;
    public float speedY;
    public float impulseX;
    public float impulseY;

    public GameObject spritePlayer;
    private GameObject holeMaker;
    private GameObject analyser;

    public Tilemap tilemap;

    private Rigidbody2D rigidbody;
    public Slider FuelSlider;
    private StackPanel stackPanel;

    private float elapsed;
    private float duration;
    private Vector3 startPos;
    private Vector3 targetPos;
    //private bool actionMode;
    private Toggle actionToggle;
    private Toggle forreuseToggle;
    private Toggle analyserToggle;
    private float Fuel;
    private float MaxFuel;
    private float dirx = 1;
    private int InterestPointCollected;

	private Text InterestPointMaxText;
	private Text InterestPointText;

    public void ActionModeToggle(bool mod)
    {
        SetMode(Mode.idle);
    }

    public void Awake()
    {
        holeMaker = GameObject.Find("HoleMaker").gameObject;
        analyser = GameObject.Find("Analyser").gameObject;
        actionToggle = GameObject.Find("ActionToggle").GetComponent<Toggle>();
        forreuseToggle = GameObject.Find("Hole").GetComponent<Toggle>();
        analyserToggle = GameObject.Find("Analyse").GetComponent<Toggle>();
        stackPanel = GameObject.Find("StackPanel").GetComponent<StackPanel>();

		InterestPointMaxText = GameObject.Find("PointOfInterestMax").GetComponent<Text>();
		InterestPointText = GameObject.Find("PointOfInterest").GetComponent<Text>();
    }

    // Use this for initialization
    public void Start()
    {
        holeMaker.SetActive(false);
        analyser.SetActive(false);
        rigidbody = this.GetComponent<Rigidbody2D>();
        MaxFuel = GameSettings.ExtraFuelBase + GameManager.instance.ExtraFuelUpgrade * GameSettings.ExtraFuelLevelDif;
        Fuel = MaxFuel;
        FuelSlider.maxValue = MaxFuel;
        FuelSlider.value = Fuel;
    }

    void SetMode(Mode newMode)
    {
        mode = newMode;
        elapsed = 0;
    }

    bool ManualMode()
    {
        return actionToggle.isOn;
    }

    //--------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------

    public void MoveTo(int dx, int dy, Mode newMode)
    {
        if (ConsumeMovement(GameSettings.MovementBase))
        {
            SetMode(newMode);
            rigidbody.AddForce(new Vector2(dx * impulseX, dy * impulseY), ForceMode2D.Impulse);
        }
    }

    public void DoActionLeft()
    {
        dirx = -1;
        MoveTo(-1, 0, Mode.moving);
    }

    public void DoActionRight()
    {
        dirx = 1;
        MoveTo(1, 0, Mode.moving);
    }

    public void DoActionJump()
    {
        MoveTo((int)dirx, 1, Mode.jumping);
    }

    public void DoActionForreuseOn()
    {
        holeMaker.SetActive(true);
        SetMode(Mode.deployingForreuse);
    }

    public void DoActionForreuseOff()
    {
        holeMaker.SetActive(false);
        SetMode(Mode.undeployingForreuse);
    }

    public void DoActionAnalyserOn()
    {
        analyser.SetActive(true);
        SetMode(Mode.deployingAnalyser);
    }

    public void DoActionAnalyserOff()
    {
        analyser.SetActive(false);
        SetMode(Mode.undeployingAnalyser);
    }

    //--------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------

    public void ActionLeft()
    {
        if(ManualMode()) {
            DoActionLeft();
        }
        else
        {
            stackPanel.AddAction("left");
        }
    }

    public void ActionRight()
    {
        if(ManualMode())
        {
            DoActionRight();
        }
        else
        {
            stackPanel.AddAction("right");
        }
    }

    public void ActionJump()
    {
        if(ManualMode())
        {
            DoActionJump();
        }
        else
        {
            stackPanel.AddAction("jump");
        }
    }

    public void ActionForrer()
    {
        if(ManualMode())
        {
            if (forreuseToggle.isOn) DoActionForreuseOn();
            else DoActionForreuseOff();
        }
        else
        {
            if (forreuseToggle.isOn) stackPanel.AddAction("forreuse-on");
            else stackPanel.AddAction("forreuse-off");
        }
    }

    public void ActionAnalyse()
    {
        if(ManualMode())
        {
            if (analyserToggle.isOn) DoActionAnalyserOn();
            else DoActionAnalyserOff();
        }
        else
        {
            if (analyserToggle.isOn) stackPanel.AddAction("analyser-on");
            else stackPanel.AddAction("analyser-off");
        }

    }

    bool leftPressed = false;
    public void ActionLeftPressed()
    {
        leftPressed = true;
    }

    public void ActionLeftRealease()
    {
        leftPressed = false;
    }

    bool rightPressed = false;
    public void ActionRightPressed()
    {
        rightPressed = true;
    }

    public void ActionRightRealease()
    {
        rightPressed = false;
    }

    bool jumpPressed = false;
    public void ActionJumpPressed()
    {
        jumpPressed = true;
    }

    public void ActionJumpRealease()
    {
        jumpPressed = false;
    }

    private bool FuelConso(float value,int interestPoint)
    {
        if(Fuel>=value)
        {
            Fuel -= value;
            InterestPointCollected += interestPoint;
            return true;
        }
        return false;
    }        

    public bool ConsumeForrage()
    {
        float conso = GameSettings.ForeuseBase - GameManager.instance.ForreuseUpgrade * GameSettings.ForeuseLevelDif;
        return FuelConso(conso,0);
    }

    public bool ConsumeMovement(float v)
    {
        float conso = v;
        return FuelConso(conso,0);
    }
    public bool ConsumeAnalyse()
    {
        float conso = GameSettings.AnalyserBase - GameManager.instance.AnalyserUpgrade * GameSettings.AnalyserLevelDif;
        return FuelConso(conso,GameSettings.AnalyseInterestPoint);
    }
    public bool ConsumeWaterAnalyse()
    {
        float conso = GameSettings.AnalyserBase - GameManager.instance.AnalyserUpgrade * GameSettings.AnalyserLevelDif;
        return FuelConso(conso,GameSettings.WaterInterestPoint);
    }

    public void Update()
    {
        FuelSlider.value = Fuel;
		InterestPointMaxText.text = "/" + GameManager.instance.InterestPointToReach;
		InterestPointText.text = InterestPointCollected.ToString();

    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        float mx = Input.GetAxis("Horizontal");
        float my = Input.GetAxis("Vertical");
        if (mx > 0) { dirx = 1; }
        else if (mx < 0) { dirx = -1; }
        
        if(ManualMode()) // Manuel
        {
            if (leftPressed) mx = -1;
            if (rightPressed) mx = 1;
            if (jumpPressed) { my = 1; mx = dirx; }
            //Debug.Log("mx " + mx+ " right "+rightPressed);
            if ((mx != 0 || my != 0) && ConsumeMovement(GameSettings.MovementBase * Time.deltaTime * GameSettings.AutoManualRatio))
            {
                rigidbody.AddForce(new Vector2(mx * speedX, my * speedY));
            }
            else
            {
                //Debug.Log("Can't move");
            }
            if(Input.GetKeyDown(KeyCode.K))
            {
                ActionForrer();
            }
        }
        else
        {
            elapsed += Time.deltaTime;
            if (mode == Mode.moving)
            {
                float vel = rigidbody.velocity.magnitude;
                if (vel == 0)
                {
                    SetMode(Mode.idle);
                    stackPanel.ConsumeAction();
                    //Debug.Log("Stop");
                }
                else
                {
                    //Debug.Log(vel);
                }
            }
            else if(mode == Mode.jumping)
            {
                if(elapsed >= GameSettings.DoubleJumpDelay)
                {
                    SetMode(Mode.idle);
                    stackPanel.ConsumeAction();
                }
            }
            else if(mode == Mode.deployingForreuse)
            {
                SetMode(Mode.idle);
                stackPanel.ConsumeAction();
            }
            else if (mode == Mode.deployingAnalyser)
            {
                SetMode(Mode.idle);
                stackPanel.ConsumeAction();
            }
            else if (mode == Mode.undeployingForreuse)
            {
                SetMode(Mode.idle);
                stackPanel.ConsumeAction();
            }
            else if (mode == Mode.undeployingAnalyser)
            {
                SetMode(Mode.idle);
                stackPanel.ConsumeAction();
            }
            else
            {
                string action = stackPanel.GetAction();
                if (action == "left") DoActionLeft();
                else if (action == "right") DoActionRight();
                else if (action == "jump") DoActionJump();
                else if (action == "forreuse-on") DoActionForreuseOn();
                else if (action == "forreuse-off") DoActionForreuseOff();
                else if (action == "analyser-on") DoActionAnalyserOn();
                else if (action == "analyser-off") DoActionAnalyserOff();
                ////Debug.Log(mx + " " + my);
                //if (mx != 0 || my != 0)
                //{
                //    if (mx > 0) { mx = 1; }
                //    else if (mx < 0) { mx = -1; }
                //    if (my > 0) { my = 1; mx = dirx; }
                //    //else if (my < 0) { my = -1; my = dirx; }
                //    MoveTo((int)mx, (int)my);
                //}
            }
        }

        Vector3 scale = spritePlayer.transform.localScale;
        scale.x = dirx;
        spritePlayer.transform.localScale = scale;
        if (!FuelConso(GameSettings.TimeConso * Time.deltaTime, 0)) Fuel = 0;
        if(Fuel<=0)
        {
            GameManager.instance.OutOfEnergy(InterestPointCollected);
        }
    }
}