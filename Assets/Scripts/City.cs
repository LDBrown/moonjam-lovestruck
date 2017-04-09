using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {

    [SerializeField]
    private int Health;
    [SerializeField]
    public static bool IsDead; 
    [SerializeField]
    private int MaxHealth = 100;
    private AudioSource aSource;
    private Timer damageTimer;
    private List<Material> cityMaterials;
    private List<Color> normalColors;

	// Use this for initialization
	void Start () {
        Health = MaxHealth;
        cityMaterials = new List<Material>();
        normalColors = new List<Color>();
        IsDead = false;
        aSource = gameObject.GetComponent<AudioSource>();
        damageTimer = gameObject.AddComponent<Timer>();
        var allMaterials = gameObject.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < allMaterials.Length; i++)
        {
            cityMaterials.Add(allMaterials[i].material);
            normalColors.Add(cityMaterials[i].color);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (IsDead)
        {
            gameObject.SetActive(false);
        }
	}

    public void DamageCity(int damage)
    {
        aSource.clip = AudioManager.GetInsatnce().GetExplosion();
        aSource.Play();
        Health -= damage;

        for (int i = 0; i < cityMaterials.Count; i++)
        {
            cityMaterials[i].color = Color.red;
        }
        
        damageTimer.StartTimer(.25f, 1, OnDamageEnd);
        if (Health <= 0)
        {
            IsDead = true;
        }
    }

    private void OnDamageEnd(Timer t)
    {
        for (int i = 0; i < cityMaterials.Count; i++)
        {
            cityMaterials[i].color = normalColors[i];
        }
    }
}
