using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicSettings : MonoBehaviour
{
    public AudioClip[] audioClips = new AudioClip[3];
    public TMP_Dropdown dropdown;
    private AudioSource audioSource;
    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (dropdown != null) { dropdown.value = DataManager.Instance.clipIndex; }
        slider.value = DataManager.Instance.musicVolume;
        audioSource.volume = DataManager.Instance.musicVolume;
        audioSource.clip = DataManager.Instance.currentClip;
        audioSource.Play();    
    }

    // Update is called once per frame
    void Update()
    {
        DataManager.Instance.musicVolume = slider.value;
    }

    public void SelectTrack()
    {
        DataManager.Instance.clipIndex = dropdown.value;
        audioSource.clip = audioClips[dropdown.value];
        DataManager.Instance.currentClip = audioClips[dropdown.value];
        audioSource.Play();
    }
    public void SavePlayerData()
    {
        SaveSystem.SavePlayerData();
    }
    public void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        DataManager.Instance.clipIndex = data.clipIndex;
        DataManager.Instance.musicVolume = data.musicVolume;
        audioSource.volume = DataManager.Instance.musicVolume;
        slider.value = DataManager.Instance.musicVolume;
        dropdown.value = DataManager.Instance.clipIndex;
        Debug.Log(data.clipIndex);
        Debug.Log(data.musicVolume);
        audioSource.Play();
    }


}
