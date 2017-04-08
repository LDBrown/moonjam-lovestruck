using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GameController : MonoBehaviour
{

	public GameObject multiTarget;
	private bool targetInitialized;
	private BasicTrackableEventHandler eventHandler;

	// Use this for initialization
	void Start ()
	{
		targetInitialized = false;
		eventHandler = multiTarget.GetComponent<BasicTrackableEventHandler> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!targetInitialized && eventHandler.isTracking) {
			targetInitialized = true;
			Debug.Log ("Target Initialized");
		}

	}
}
