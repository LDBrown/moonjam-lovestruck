using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {

    [SerializeField]
    private int Health;
    [SerializeField]
    private bool IsDead; 
    [SerializeField]
    private int MaxHealth = 100;

	// Use this for initialization
	void Start () {
        Health = MaxHealth;
        IsDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DamageCity(int damage)
    {
        Health -= damage;
        if (Health >= 0)
        {
            IsDead = true;
        }
    }
}
