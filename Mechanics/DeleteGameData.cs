using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeleteGameData : MonoBehaviour {

	public void DeleteData()
	{
		PlayerPrefs.DeleteAll ();
		//SceneManager.LoadScene ("LoadingScene",LoadSceneMode.Single);

	}
}
