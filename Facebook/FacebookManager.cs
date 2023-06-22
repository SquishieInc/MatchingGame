
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour {

	private static FacebookManager _instance;

	public static FacebookManager Instance

	{
		get{
			if (_instance == null) {
				GameObject fbm = new GameObject ("FBManager");
				fbm.AddComponent<FacebookManager> ();
			}

			return _instance;
		}
	}

	public bool IsLoggedIn { get; set; }
	public string ProfileName { get; set; }
	public Sprite ProfilePic { get; set; }
	//public string AppLinkURL { get; set; }
	private static string AppLinkURL = "https://fb.me/343499246026625";

	void Awake ()
	{

		DontDestroyOnLoad (this.gameObject);
		_instance = this;

		IsLoggedIn = true;
	}

	public void InitFB()
	{
		if (!FB.IsInitialized) {
			FB.Init (SetInit, OnHideUnity);
		} else {
			IsLoggedIn = FB.IsLoggedIn;
		}
		Debug.Log(FacebookManager.Instance.IsLoggedIn);
	}

	void SetInit()
	{
		if (FB.IsLoggedIn) 
		{
//			Debug.Log ("FB is Logged in");
			GetProfile ();
		} 
		else 
		{
//			Debug.Log ("FB is NOT logged in");
		}
		IsLoggedIn = FB.IsLoggedIn;
	}

	void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown) 
		{
			Time.timeScale = 0;
		} 
		else 
		{
			Time.timeScale = 1;
		}
	}

	public void GetProfile()
	{
		FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
		FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
//		FB.GetAppLink (DealWithAppLink);
	}

	void DisplayUsername(IResult result)
	{
		if(result.Error == null)
		{
			ProfileName = "" + result.ResultDictionary ["first_name"];
		}
		else
		{
			Debug.Log (result.Error);
		}
	}

	void DisplayProfilePic(IGraphResult result)
	{
		if (result.Texture != null) 
		{
			ProfilePic = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
		}
		else 
		{

		}
	}
	/*
	void DealWithAppLink (IAppLinkResult result)
	{
		if (!string.IsNullOrEmpty (result.Url)) {
			AppLinkURL = "https://fb.me/1809684675912695";
			Debug.Log (AppLinkURL);
		} else {
			AppLinkURL = "http://squishieinc.com";
		}
	}
	*/

	public void Share()
	{
		FB.FeedShare (
			string.Empty,
			new Uri (AppLinkURL),
			"Try MeM Today",
			"Download today from the App Store",
			"squishieinc.com",
			new Uri ("http://www.squishieinc.com/wp-content/uploads/2017/06/MeMIcon.png"),
			string.Empty,
			ShareCallback
		);
	}

	void ShareCallback (IResult result)
	{
		if(result.Cancelled)
		{
			Debug.Log ("Share Cancelled");
		}
		else if(!string.IsNullOrEmpty(result.Error))
		{
			Debug.Log ("Error on Share");
		}
		else if(!string.IsNullOrEmpty(result.RawResult))
		{
			Debug.Log ("Success on Share");
			//UIcontroller.FacebookReward (25);

		}
	}

	public void Invite()
	{
		FB.Mobile.AppInvite (
			new Uri (AppLinkURL),
			new Uri ("http://www.squishieinc.com/wp-content/uploads/2017/06/MeMIcon.png"),
			InviteCallback
		);
	}

	void InviteCallback(IResult result)
	{
		if(result.Cancelled)
		{
			Debug.Log ("Invite Cancelled");
		}
		else if(!string.IsNullOrEmpty(result.Error))
		{
			Debug.Log ("Error on Invite");
		}
		else if(!string.IsNullOrEmpty(result.RawResult))
		{
			Debug.Log ("Success on Invite");
		//	UIcontroller.FacebookReward (25);
		}
	}

	public void ShareWithUsers()
	{
		FB.AppRequest (
			"I have just completed a game on MeM!",
			null,
			new List<object> () {"app_users"},
			null,
			null,
			null,
			null,
			ShareWithUsersCallback
		);
	}

	void ShareWithUsersCallback(IAppRequestResult result)
	{
		if(result.Cancelled)
		{
			Debug.Log ("Challange Cancelled");
		}
		else if(!string.IsNullOrEmpty(result.Error))
		{
			Debug.Log ("Error on Challange");
		}
		else if(!string.IsNullOrEmpty(result.RawResult))
		{
			Debug.Log ("Challange on Invite");
		//	UIcontroller.FacebookReward (25);
		}
	}
}

