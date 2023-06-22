
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.UI;

public class FBScript : MonoBehaviour {

	public GameObject FBLoginButton;
	public GameObject FBShareButton;
	public GameObject FBInviteButton;
	public GameObject FBChallangeButton;

	public GameObject DialogUsername;
	public GameObject DialogProfilePic;

	void Awake() 
	{
		FacebookManager.Instance.InitFB ();
		DealWithFBButton (FB.IsLoggedIn);
	}
		
	public void FBLogin()
	{
		List<string> permissions = new List<string>();
		permissions.Add ("public_profile");

		FB.LogInWithReadPermissions (permissions, AuthCallBack);
	}

	void AuthCallBack(IResult result)
	{
		if (result.Error != null) 
		{
			Debug.Log (result.Error);
		} 
		else 
		{
			if (FB.IsLoggedIn) 
			{
				FacebookManager.Instance.IsLoggedIn = true;
				FacebookManager.Instance.GetProfile ();
				Debug.Log ("AuthCallBack FB is Logged in");
			} 
			else 
			{
				Debug.Log ("AuthCallBack FB is NOT logged in");
			}
			DealWithFBButton (FB.IsLoggedIn);
		}
	}

	void DealWithFBButton (bool isLoggedIn)
	{
		if (isLoggedIn) 
		{
			FBLoginButton.SetActive (false);
			FBShareButton.SetActive (true);
			FBInviteButton.SetActive (true);
			FBChallangeButton.SetActive (true);

			DialogUsername.SetActive (true);
			DialogProfilePic.SetActive (true);

			if (FacebookManager.Instance.ProfileName != null) {
				Text UserName = DialogUsername.GetComponent<Text> ();
				UserName.text = "Hi " + FacebookManager.Instance.ProfileName;
			} 
			else 
			{
				StartCoroutine ("WaitForProfileName");
			}

			if (FacebookManager.Instance.ProfilePic != null) {
				Image ProfilePic = DialogProfilePic.GetComponent<Image> ();
				ProfilePic.sprite = FacebookManager.Instance.ProfilePic;
			} 
			else 
			{
				StartCoroutine ("WaitForProfilePic");
			}
		} 
		else 
		{
			FBLoginButton.SetActive (true);
			FBShareButton.SetActive (false);
			FBInviteButton.SetActive (false);
			FBChallangeButton.SetActive (false);

			DialogUsername.SetActive (false);
			DialogProfilePic.SetActive (false);
		}
	}

	IEnumerator WaitForProfileName()
	{
		while (FacebookManager.Instance.ProfileName == null) {
			yield return null;
		}

		DealWithFBButton (FacebookManager.Instance.IsLoggedIn);
	}

	IEnumerator WaitForProfilePic()
	{
		while (FacebookManager.Instance.ProfilePic == null) {
			yield return null;
		}

		DealWithFBButton (FacebookManager.Instance.IsLoggedIn);
	}

	public void Share()
	{
		FacebookManager.Instance.Share ();
	}

	public void Invite()
	{
		FacebookManager.Instance.Invite ();
	}

	public void ShareWithUsers()
	{
		FacebookManager.Instance.ShareWithUsers ();
	}
}

