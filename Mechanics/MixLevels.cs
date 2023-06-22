using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {

	public AudioMixer masterMixer;

	public Slider mSlider;
	public Slider bSlider;
	public Slider sSlider;

	private float startMLev;
	private float startBLev;
	private float startSLev;


	void Start()
	{
		startMLev = PlayerPrefs.GetFloat ("MVol", 0);
		startBLev = PlayerPrefs.GetFloat ("BVol", 0);
		startSLev = PlayerPrefs.GetFloat ("SVol", 0);

		masterMixer.SetFloat ("MasterVol", startMLev);
		masterMixer.SetFloat ("BGVol", startBLev);
		masterMixer.SetFloat ("SFXVol", startSLev);

		mSlider.value = startMLev;
		bSlider.value = startBLev;
		sSlider.value = startSLev;
	}

	public void SetMasterLvl(float masterLvl){
		masterMixer.SetFloat ("MasterVol", masterLvl);
		PlayerPrefs.SetFloat ("MVol" , masterLvl);
	}

	public void SetBGLvl(float bgLvl) {
		masterMixer.SetFloat ("BGVol", bgLvl);
		PlayerPrefs.SetFloat ("BVol" , bgLvl);
	}

	public void SetSFXLvl(float sfxLvl) {
		masterMixer.SetFloat ("SFXVol", sfxLvl);
		PlayerPrefs.SetFloat ("SVol" , sfxLvl);
	}

	public void SaveAudioSettings()
	{

	}
}
