using System;
using System.Collections.Generic;




[Serializable]
public class EnemySpawnEvent
{
    public int Enemy; 
    public float Time; 
}

[Serializable]
public class Wave1
{
    public int Wave;
    public List<EnemySpawnEvent> Enemies; 
}

[Serializable]
public class WavesData
{
    public List<Wave1> Waves; 
}