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

	// Use this for initialization
	void Start () {
        IsDead = false;
        Health = GetMaxHealth();
        City = GameObject.FindObjectOfType<City>();
        DamageTimer = gameObject.AddComponent<Timer>();
        DamageTimer.StartTimer(Random.Range(MinFireTime, MaxFireTime), 1, OnTimerComplete);

        var gaze = gameObject.GetComponent<BasicGazeButton>();
        gaze.OnGazeInput.AddListener(OnGazeHit);
	}
	
	// Update is called once per frame
	void Update () {
		if (IsDead)
        {
            gameObject.SetActive(false);
        }
	}

    void OnTimerComplete(Timer t)
    {
        DamageTimer.StartTimer(Random.Range(MinFireTime, MaxFireTime), 1, OnTimerComplete);
        City.DamageCity(DamagePower);
    }

    public void HitShip(int damage)
    {
        if (GameController.GameStarted)
        {
            Debug.Log("Ship HIT!!!");
            Health -= damage;
            if (Health <= 0)
            {
                IsDead = true;
            }
        }
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

        return 5; 
    }

    private int GetDamagePower()
    {
        if(DamagePowers.Length < Level && Level > 0)
        {
            return DamagePowers[Level - 1];
        }

        return 5;
    }
}
