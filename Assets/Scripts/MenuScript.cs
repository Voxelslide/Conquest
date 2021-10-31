using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
 
	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}

	public void QuitGame()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene(0);
	}


}
