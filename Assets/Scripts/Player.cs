using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float FireRate = 0.5f;
    private float FireWindow = .05f;
    private Timer FireRateTimer;
    private Timer FireWindowTimer;
    private LineRenderer Laser;
    private int FireSide = 0;

    public static bool CanFire = true;

	// Use this for initialization
	void Start ()
    {
        FireRateTimer = gameObject.AddComponent<Timer>();
        FireWindowTimer = gameObject.AddComponent<Timer>();
        Laser = gameObject.AddComponent<LineRenderer>();
        Laser.material = new Material(Shader.Find("Particles/Additive"));
        Laser.widthMultiplier = 0f;
        Laser.numPositions = 2;
        Laser.startColor = Color.green;
        Laser.endColor = Color.green;
	}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Player.CanFire)
        {
            FireWindowTimer.StartTimer(FireWindow, 1, FireWindowOnComplete);
            FireLazer();
        }
    }

    void FireWindowOnComplete(Timer t)
    {
        Player.CanFire = false;
        Laser.widthMultiplier = 0;
        FireRateTimer.StartTimer(FireRate, 1, FireRateOnComplete);
    }

    void FireRateOnComplete(Timer t)
    {
        Player.CanFire = true;
    }

    void FireLazer()
    {
        var origin = this.transform.position;
        var offset = FireSide++ % 2 == 0 ? this.transform.right : -this.transform.right;
        var direction = this.transform.forward;
        Debug.Log("Start: " + (origin + (offset * 2).ToString() + " End: " + (origin + (direction * 200)).ToString()));
        Laser.widthMultiplier = 0.5f;
        Laser.SetPosition(0, origin + (offset * 2));
        Laser.SetPosition(1, origin + (direction * 200) + offset * 15);
    }
}
