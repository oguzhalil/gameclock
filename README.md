# Stash Quick Start

@NOTE: for custom classes make sure you added System.Serializable attribute 
```csharp 
[System.Serializable]
public class Foo 
{
   public int bar;
}
```
## Stash Usage 

#### Initialize
Initialize local stash object with given **unique** identifier. 
<br></br>
Either at playerprefs or persistentpath location.
<br></br>
Register a method for error handling.
```csharp
 stash = Stash.PlayerPrefs(stashId, OnStashError);
 // or stash = Stash.PersistentPath(stashId, OnStashError);

 void OnStashError(StashError error)
 {
     Debug.LogError(error);
 }
```
#### Set 
You can save any object supported by binary formatter. 
<br></br>
int,byte,float,string,classes,struct etc.
<br></br>
 `Set<T>(string key, T value)` creates or updates given key-value pair 
```csharp 
 stash.Set("someInteger", 5);
 GameConfig writeConfig = new GameConfig();
 stash.Set("gameConfig", writeConfig);
```
#### Get 
For fail safe `Get<T>(string key, T defaultValue)` method requires defaultValue<br>
If given key-value pair is not exist then defaultValue will be returned
```csharp 
 int integer = stash.Get("someInteger" , 0);
 GameConfig readConfig = stash.Get("gameConfig", new GameConfig());
```
#### Save Stash 
Stash is not saved to disk until you call `stash.Save()` method
<br></br>
stash.Save() blocks UIThread until operation is completed.
<br></br>
NOTE: Dont save huge files(10 mb or more) when processing exit message (OS) may suspend your save operation
<br></br>
you may end up with broken save file.
```csharp 
// can be called any time during the game.
 stash.Save(); 

  // on application killed - make sure stash is saved.
 private void OnApplicationQuit()
 {
    stash.Save();
 }

 // on application suspended (home button) - make sure stash is saved.
 private void OnApplicationPause(bool pause)
 {
    if(pause)
    {
        stash.Save();
    }
 }
```