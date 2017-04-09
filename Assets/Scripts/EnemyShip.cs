using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShip : MonoBehaviour {

    [SerializeField]
    private int DamagePower;
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
    private int MaxHealth = 5;
    [SerializeField]
    private bool IsDead;

	// Use this for initialization
	void Start () {
        IsDead = false;
        Health = MaxHealth;
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
        Debug.Log("Ship HIT!!!");
        Health -= damage;
        if (Health <= 0)
        {
            IsDead = true;
        }
    }

    public void OnGazeHit()
    {
        HitShip(1);
    }
}
