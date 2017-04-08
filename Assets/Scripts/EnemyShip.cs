using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start () {
        Health = MaxHealth;
        City = GameObject.FindObjectOfType<City>();
        DamageTimer = gameObject.AddComponent<Timer>();
        DamageTimer.StartTimer(Random.Range(MinFireTime, MaxFireTime), 1, OnTimerComplete);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTimerComplete(Timer t)
    {
        DamageTimer.StartTimer(Random.Range(MinFireTime, MaxFireTime), 1, OnTimerComplete);
        City.DamageCity(DamagePower);
    }
}
