using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

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

	// Use this for initialization
	void Start ()
	{
		targetInitialized = false;
		waveActive = false;
		userChecked = false;
		eventHandler = multiTarget.GetComponent<BasicTrackableEventHandler> ();
		wavesCleared = 0;
		currentWave = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!targetInitialized && eventHandler.isTracking) {
			targetInitialized = true;
			Debug.Log ("Target Initialized");
			StartCoroutine (SpawnWave (3.0f, 1));
		}

		if (targetInitialized & !waveActive) {
			currentWave++;
			StartCoroutine (SpawnWave (5.0f, currentWave));
		}



	}

	IEnumerator SpawnWave (float delay, int waveNumber)
	{
		waveActive = true;

		yield return new WaitForSeconds (delay);

		SpawnShip (1, 1, 1);
		Debug.Log ("Spawn wave:" + waveNumber);

	}

	void SpawnShip(int level, int spline)

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
	}



}
