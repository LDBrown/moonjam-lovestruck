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
    private AudioSource LaserSound;

    public static bool CanFire = true;
    public static bool IsFiring = true;

	// Use this for initialization
	void Start ()
    {
        FireRateTimer = gameObject.AddComponent<Timer>();
        FireWindowTimer = gameObject.AddComponent<Timer>();
        Laser = gameObject.AddComponent<LineRenderer>();
        Laser.material = new Material(Shader.Find("Unlit/Texture"));
        Laser.widthMultiplier = 0f;
        Laser.numPositions = 2;
        Laser.startColor = Color.green;
        Laser.endColor = Color.green;
        Laser.sortingLayerName = "Lasers";
        LaserSound = gameObject.GetComponent<AudioSource>();
	}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Player.CanFire && GameController.GameStarted)
        {
            FireWindowTimer.StartTimer(FireWindow, 1, FireWindowOnComplete);
            FireLazer();
            Player.IsFiring = true;
            Player.CanFire = false;
        }
    }

    void FireWindowOnComplete(Timer t)
    {
        Player.IsFiring = false;
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
        var isRight = Input.mousePosition.x > Screen.width / 2;
        var offset = isRight ? this.transform.right : -this.transform.right;
        if (Input.touches.Length > 0)
        {
            offset = Input.GetTouch(0).position.x > Screen.width / 2 ? this.transform.right : -this.transform.right;
        }
        var direction = this.transform.forward;
        Laser.widthMultiplier = 0.5f;
        Laser.SetPosition(0, origin + (offset * 2));
        Laser.SetPosition(1, origin + (direction * 200) + offset * 15);

        LaserSound.panStereo = isRight ? 1 : -1;  
        LaserSound.clip = AudioManager.GetInsatnce().GetLaser();
        LaserSound.Play();
    }
}
