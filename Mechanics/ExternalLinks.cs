using UnityEngine;
using System.Collections;

public class ExternalLinks : MonoBehaviour {

	// Use this for initialization
	public void SkipSoundCLoud () 
	{
		Application.OpenURL("https://soundcloud.com/skipcloud/");
		Debug.Log ("Skip SoundCloud");
	}
	
	// Update is called once per frame
	public void SquishieWebsite () 
	{
		Application.OpenURL("http://squishieinc.com");
		Debug.Log ("SquishieInc");
	}

	public void SkipBandCamp () 
	{
		Application.OpenURL("https://skipcloud.bandcamp.com");
		Debug.Log ("Skip BandCamp");
	}

	public void BenSound () 
	{
		Application.OpenURL("http://www.bensound.com");
		Debug.Log ("BenSound");
	}

	public void RetroruniOS () 
	{
		Application.OpenURL("https://itunes.apple.com/app/retrorun/id1099991381");
		Debug.Log ("BenSound");
	}

	public void RetrorunAndroid () 
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.SquishieInc.Runner");
		Debug.Log ("BenSound");
	}

	public void CMAFacebookLink()
	{
		Application.OpenURL("https://www.facebook.com/ChildrensMusicAcademyireland");
		Debug.Log ("CMAFB");
	}
}
