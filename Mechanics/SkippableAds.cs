/*
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityEngine.Advertisements;

public class SkippableAds : MonoBehaviour {

	public string androidAdId = "1206948";
	public string iosAdId = "1206949";

	public bool adReady;

	void Start()
	{
		adReady = true;
	}

	void Awake ()
	{
		if (Advertisement.isSupported) 
		{
			if(Application.platform == RuntimePlatform.Android)
			{
				Advertisement.Initialize(androidAdId, false);
			}
			else if(Application.platform == RuntimePlatform.IPhonePlayer)
			{
				Advertisement.Initialize(iosAdId, false);
			}
		}
	}


	public void ShowAd()
	{
		adReady = false;
		if (Advertisement.IsReady())
		{
			//var options = new ShowOptions {resultCallback = HandleShowResult};
			Advertisement.Show ();
		}
	}
}
*/