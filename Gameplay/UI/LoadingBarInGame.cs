using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingBarInGame : MonoBehaviour {

	AsyncOperation ao;
	public GameObject loadingScreenBackground;
	public Slider progBar;
	public Text loadingText;
	public Sprite[] backgroundImage;

	private string levelName;


	public void LevelToLoad(string levelNeededToLoad, int index = 0)
	{
		Debug.Log ("LOADINGBARINGAME INDEX = " + index);
		loadingScreenBackground.SetActive (true);
		loadingScreenBackground.GetComponent<Image> ().sprite = backgroundImage [index];
		progBar.gameObject.SetActive (true);
		loadingText.gameObject.SetActive (true);

		loadingText.text = "Loading....";

		StartCoroutine (LoadLevelWithRealProgress (levelNeededToLoad));
	}

	public void ReloadLevelToLoad()
	{
		Scene scene = SceneManager.GetActiveScene ();
		levelName = scene.name;

		loadingScreenBackground.SetActive (true);
		progBar.gameObject.SetActive (true);
		loadingText.gameObject.SetActive (true);

		loadingText.text = "Loading....";

		//StartCoroutine (LoadLevelWithRealProgress (levelName));
		StartCoroutine (LoadLevelWithRealProgress ("MeMLeveliOS"));
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
				loadingText.text = "Loaded!";
				yield return new WaitForSeconds (1f);
				ao.allowSceneActivation = true;

			}
			Debug.Log (ao.progress);
			yield return null;
		}
	}
}