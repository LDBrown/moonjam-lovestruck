using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    [SerializeField]
    private float CurrentTime;
    [SerializeField]
    private float EndTime;
    [SerializeField]
    private int Loops; 
    [SerializeField]
    private int NumberOfLoops;
    [SerializeField]
    private bool Running;

    public delegate void TimerCallback(Timer t);
    public TimerCallback OnCompleteCallback;

    // Update is called once per frame
    void Update () {
        if (Running)
        {
            CurrentTime += Time.deltaTime;
            if (CurrentTime >= EndTime)
            {
                OnComplete();
            }
        }
	}

    public void StartTimer(float duration, int loops=1, TimerCallback onComplete=null)
    {
        EndTime = duration;
        Loops = 0;
        NumberOfLoops = loops;
        Running = true;
        OnCompleteCallback = onComplete;
    }

    public void PauseTimer()
    {
        Running = false;
    }

    public void ResumeTimer()
    {
        Running = true;
    }

    void OnComplete()
    {
        CurrentTime = 0;
        Loops++;

        if (Loops >= NumberOfLoops && NumberOfLoops > 0) 
        {
            Running = false;
        }

        if (OnCompleteCallback != null)
        {
            OnCompleteCallback(this);
        }
    }
}
