using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LoadingBarScript : MonoBehaviour {

	AsyncOperation ao;
	public string sceneToLoad;
	public GameObject loadingScreenBackground;
	public Slider progBar;
	public Text loadingText;
	public GameObject splashBackground;
	public GameObject splashPanel;


	IEnumerator Start()
	{
		SplashScreen.Begin();
		while (/*Application.isShowingSplashScreen*/!SplashScreen.isFinished)
		{
			SplashScreen.Draw();
			yield return null;
		}

		if (SplashScreen.isFinished) 
		{
			SquishieSplash ();
		}
	}

	/*
	// Use this for initialization
	void Start () {
		FirstLevelToLoad ();
	}*/

	void SquishieSplash()
	{
		splashPanel.SetActive (true);
		Invoke ("FirstLevelToLoad", 2);

	}


	public void FirstLevelToLoad()
	{
		splashBackground.SetActive (false);
		splashPanel.SetActive (false);
		loadingScreenBackground.SetActive (true);
		progBar.gameObject.SetActive (true);
		loadingText.gameObject.SetActive (true);

		loadingText.text = "Loading....";

		#if UNITY_IOS
		StartCoroutine (LoadLevelWithRealProgress ("MeMLeveliOS"));

		#elif UNITY_ANDROID
		StartCoroutine (LoadLevelWithRealProgress ("MeMLevelAndroid"));

		#endif

	}

	public void LevelToLoad(string levelNeededToLoad)
	{
		loadingScreenBackground.SetActive (true);
		progBar.gameObject.SetActive (true);
		loadingText.gameObject.SetActive (true);

		loadingText.text = "Loading....";

		StartCoroutine (LoadLevelWithRealProgress (levelNeededToLoad));
	}


	IEnumerator LoadLevelWithRealProgress(string sceneName)
	{
		yield return new WaitForSeconds (1f);

		ao = SceneManager.LoadSceneAsync (sceneName);
		ao.allowSceneActivation = false;

		while (!ao.isDone) 
		{
			progBar.value = ao.progress;

			if (ao.progress == 0.9f) 
			{
				progBar.value = 1f;
				//loadingText.text = "Press 'F' to Continue!";
				loadingText.text = "Loaded!";
				/*
					if(Input.GetKeyDown(KeyCode.F))
					{
					ao.allowSceneActivation = true;
					}
					*/
				yield return new WaitForSeconds (1f);
				ao.allowSceneActivation = true;

			}
			Debug.Log (ao.progress);
			yield return null;
		}
	}
}
