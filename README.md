# Basic Game Clock For Unity

I found myself writing this code over and over again. So here is basic game clock for calling function after period of time.

## Game Clock Usage 

```csharp 
void Start()
    {
	// calls the method once after .5f seconds with payload 5
        GameClock.Instance.Append(.5f, PrintHelloWorld, payload: 5);
	// calls the method until .1f seconds is passed.
        GameClock.Instance.Append(.1f, PrintUpdateCompleted, onUpdate: PrintUpdate, payload: 5);
	// call the methods every second.
        GameClock.Instance.Append(1f, PrintHelloWorldEverySecond, loop: true, payload: 5);
    }

    private void PrintUpdateCompleted()
    {
        var lastClock = GameClock.Instance.lastClock; // access parameters of the clock
	int number = (int)GameClock.Instance.payload; // access the payload
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
```
