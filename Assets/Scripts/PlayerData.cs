using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float musicVolume;
    public int clipIndex;
    public int score;
    public PlayerData()
    {
        musicVolume = DataManager.Instance.musicVolume;
        clipIndex = DataManager.Instance.clipIndex;
    }
}
