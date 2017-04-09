using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private AudioClip Music;
    [SerializeField]
    private AudioClip[] Lasers;
    private AudioSource MusicSource;
    private static AudioManager instance;

    public static AudioManager GetInsatnce()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<AudioManager>();
        }

        return instance;
    } 

	// Use this for initialization
	void Start () {
        MusicSource = gameObject.AddComponent<AudioSource>();
        MusicSource.clip = Music;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void StartMusic()
    {
        MusicSource.Play();
        MusicSource.loop = true;
    } 

    public void StopMusic()
    {
        MusicSource.Stop();
    }

    public AudioClip GetLaser()
    {
        var random = (int)(Mathf.Floor(Random.Range(0, Lasers.Length)));
        return Lasers[random];
    }
}
