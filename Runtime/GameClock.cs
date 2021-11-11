using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ideas to improve game clock
// stop and start feature for specific clocks
// delete all clocks feature.
// game pause and continue

[DefaultExecutionOrder(-1000)]
public class GameClock : MonoBehaviour
{
    public static GameClock Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("GameClock").AddComponent<GameClock>();
            }

            return instance;
        }
    }

    private static GameClock instance;
    public ClockParams lastClock;
    public LinkedList<ClockParams> queue = new LinkedList<ClockParams>();

    private void Awake()
    {
        if(instance == null) // first instance
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Append(float fireAfter, System.Action onComplete, bool loop = false, System.Action<float> onUpdate = null, object payload = null)
    {
        ClockParams parameters = new ClockParams();
        parameters.fireAfter = fireAfter;
        parameters.loop = loop;
        parameters.timer = Time.time + fireAfter;
        parameters.payload = payload;
        parameters.onUpdate = onUpdate;
        parameters.onComplete = onComplete;
        queue.AddLast(parameters);
    }

    public void Update()
    {
        var head = queue.First;

        while(head != null)
        {
            var current = head;
            var clock = current.Value;
            lastClock = clock;

            if(Time.time >= clock.timer)
            {
                clock.timer = Time.time + clock.fireAfter;
                current.Value = clock; // we need to apply changes.
                clock.onComplete.Invoke();

                if(!clock.loop)
                {
                    head = current.Next;
                    queue.Remove(current);
                    continue;
                }
            }
            else
            {
                if(clock.onUpdate != null)
                {
                    float remainingTime = clock.timer - Time.time;
                    clock.onUpdate.Invoke(remainingTime);
                }
            }

            head = head.Next;
        }
    }
}

public struct ClockParams
{
    public float timer;
    public float fireAfter;
    public bool loop;
    public System.Action onComplete;
    public System.Action<float> onUpdate;
    public System.Object payload;
}
