using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	public GameObject multiTarget;
	private bool targetInitialized;
	private bool waveActive;
	private bool userChecked;
	private int wavesCleared;
	private int currentWave;
	private BasicTrackableEventHandler eventHandler;
	public BezierSpline[] splines;
    [SerializeField]
    private GameObject[] ships;
    public static bool GameStarted { get; private set; }
    private Timer WaveTimer;
    [SerializeField]
    private Text DefendText;
    [SerializeField]
    private Text CountDown;
    private List<GameObject> activeShips;

	// Use this for initialization
	void Start ()
	{
        activeShips = new List<GameObject>();
        WaveTimer = gameObject.AddComponent<Timer>();
        GameStarted = false;
		targetInitialized = false;
		waveActive = false;
		userChecked = false;
		eventHandler = multiTarget.GetComponent<BasicTrackableEventHandler> ();
		wavesCleared = 0;
		currentWave = 0;
        CountDown.gameObject.SetActive(false);
        AudioManager.GetInsatnce().StartMusic();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!targetInitialized && eventHandler.isTracking) {
			targetInitialized = true;
			Debug.Log ("Target Initialized");
            WaveTimer.StartTimer(5, 1, OnWaveTimer);
            CountDown.gameObject.SetActive(true);
            SpawnWave(0);
		}

		if (targetInitialized & !waveActive) {
			currentWave++;
            CountDown.gameObject.SetActive(true);
            WaveTimer.StartTimer(5, 1, OnWaveTimer);
            SpawnWave(currentWave);
		}

        if (WaveTimer.Running)
        {
            CountDown.text = Mathf.Ceil(WaveTimer.GetTimeLeft()).ToString();
        }

        if (waveActive && GameStarted)
        {
            var shipsActive = 0;
            foreach(var ship in activeShips)
            {
                if (!ship.GetComponent<EnemyShip>().IsDead)
                {
                    shipsActive++;
                    break;
                }
            }

            if (shipsActive <= 0)
            {
                clearWave();
            }
        }

	}

    void OnWaveTimer(Timer t)
    {
        CountDown.gameObject.SetActive(false);
        DefendText.gameObject.SetActive(false);
        GameStarted = true;
    }

	void SpawnWave (int waveNumber)
	{
		waveActive = true;

		SpawnShip (1, 0);
		Debug.Log ("Spawn wave:" + waveNumber);

	}

	void SpawnShip(int level, int spline)
    {
        var ship = GameObject.Instantiate(ships[0]);
        var shipSpline = ship.GetComponent<SplineWalker>();
        shipSpline.spline = splines[spline];
        shipSpline.mode = SplineWalkerMode.Loop;
        shipSpline.duration = 15;
        ship.GetComponent<EnemyShip>().SetStats(level);
        activeShips.Add(ship);
    }

	public int GetCurrentWave ()
	{
		return currentWave;
	}

	public int GetWavesCleared ()
	{
		return wavesCleared;
	}

	public void clearWave ()
	{
		waveActive = false;
		wavesCleared++;
        DefendText.gameObject.SetActive(true);
        GameStarted = false;
	}



}
