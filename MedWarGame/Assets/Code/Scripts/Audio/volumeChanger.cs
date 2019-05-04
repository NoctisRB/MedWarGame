using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeChanger : MonoBehaviour {

    // Use this for initialization

    public Slider volume;
    public Slider sfxVol;
    public Slider musicVol;

	void Start () {
        volume.value = AudioManager.generalVolume;
        sfxVol.value = AudioManager.sfxVolume;
        musicVol.value = AudioManager.musicVolume;
	}
	
	// Update is called once per frame
	void Update () {
        AudioManager.generalVolume = volume.value;
        AudioManager.sfxVolume = sfxVol.value;
        AudioManager.musicVolume = musicVol.value;
	}
}
