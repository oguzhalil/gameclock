using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGameClock : MonoBehaviour
{
    void Start()
    {
        GameClock.Instance.Append(.5f, PrintHelloWorld, payload: 5);
        GameClock.Instance.Append(.1f, PrintUpdateCompleted, onUpdate: PrintUpdate, payload: 5);
        GameClock.Instance.Append(1f, PrintHelloWorldEverySecond, loop: true, payload: 5);
    }

    private void PrintUpdateCompleted()
    {
        var lastClock = GameClock.Instance.lastClock;
        Debug.Log($"UpdateCompleted after {lastClock.fireAfter} seconds");
    }

    private void PrintUpdate(float remainingTime)
    {
        var lastClock = GameClock.Instance.lastClock;
        Debug.Log($"I am gonna fire after {remainingTime} seconds");
    }

    private void PrintHelloWorldEverySecond()
    {
        var lastClock = GameClock.Instance.lastClock;
        Debug.Log($"I am looping. Every {lastClock.fireAfter} seconds");
    }

    private void PrintHelloWorld()
    {
        var lastClock = GameClock.Instance.lastClock;
        Debug.Log($"I am fired after {lastClock.fireAfter} seconds. and my payload is {lastClock.payload}");
    }
}
