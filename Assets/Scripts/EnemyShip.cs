using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShip : MonoBehaviour {

    [SerializeField]
    private int[] DamagePowers = new int[] { };
    private int DamagePower = 5;
    [SerializeField]
    private float MinFireTime;
    [SerializeField]
    private float MaxFireTime;
    private Timer DamageTimer;
    [SerializeField]
    private City City;
    [SerializeField]
    private int Health;
    [SerializeField]
    private int[] MaxHealth = new int[] { };
    public bool IsDead { get; private set; }
    public int Level = 1;
    private AudioSource audioSource;
    private LineRenderer Laser;
    private Timer laserTimer;
    private ParticleSystem particles;

    private Material oriMat;
    private Color origColor;

	// Use this for initialization
	void Start () {
        IsDead = false;
        Health = GetMaxHealth();
        City = GameObject.FindObjectOfType<City>();
        DamageTimer = gameObject.AddComponent<Timer>();

        var gaze = gameObject.GetComponent<BasicGazeButton>();
        gaze.OnGazeInput.AddListener(OnGazeHit);

        audioSource = gameObject.AddComponent<AudioSource>();

        laserTimer = gameObject.AddComponent<Timer>();
        Laser = gameObject.AddComponent<LineRenderer>();
        Laser.material  = new Material(Shader.Find("Particles/Additive"));
        Laser.widthMultiplier = 0f;
        Laser.numPositions = 2;
        Laser.startColor = Color.red;
        Laser.endColor = Color.red;
        particles = gameObject.GetComponentInChildren<ParticleSystem>();

        //oriMat = gameObject.GetComponentInChildren<Material>();
        //origColor = oriMat.color;
    }
	
	// Update is called once per frame
	void Update () {
		if (IsDead)
        {
            gameObject.SetActive(false);
        }

        if (!DamageTimer.Running && GameController.GameStarted)
        {
            DamageTimer.StartTimer(Random.Range(MinFireTime, MaxFireTime), 1, OnTimerComplete);
        }
	}

    void OnTimerComplete(Timer t)
    {
        DamageTimer.StartTimer(Random.Range(MinFireTime, MaxFireTime), 1, OnTimerComplete);
        City.DamageCity(DamagePower);
        Laser.widthMultiplier = .02f;
        Laser.SetPosition(0, this.transform.position);
        Laser.SetPosition(1, City.transform.position);
        laserTimer.StartTimer(.25f, 1, OnLaserEnd);
    }

    public void HitShip(int damage)
    {
        if (GameController.GameStarted)
        {
            //particles.Emit(5); 
            Debug.Log("Ship HIT!!!");
            audioSource.clip = AudioManager.GetInsatnce().GetExplosion();
            audioSource.Play();
            Health -= damage;
            //oriMat.color = Color.red;
            if (Health <= 0)
            {
                IsDead = true;
            }
        }
    }

    private void OnLaserEnd(Timer t)
    {
        Laser.widthMultiplier = 0;
       // oriMat.color = origColor;
    }

    public void OnGazeHit()
    {
        HitShip(1);
    }

    public void SetStats(int level)
    {
        Level = level;
        Health = GetMaxHealth();
        DamagePower = GetDamagePower(); 
    }

    private int GetMaxHealth()
    {
        if (MaxHealth.Length < Level && Level > 0)
        {
            return MaxHealth[Level-1];
        }
        else if (MaxHealth.Length > 0 && Level > 0)
        {
            return MaxHealth[MaxHealth.Length - 1];
        }

        return 5; 
    }

    private int GetDamagePower()
    {
        if(DamagePowers.Length < Level && Level > 0)
        {
            return DamagePowers[Level - 1];
        }
        else if (DamagePowers.Length > 0 && Level > 0)
        {
            return DamagePowers[DamagePowers.Length - 1];
        }

        return 5;
    }
}
