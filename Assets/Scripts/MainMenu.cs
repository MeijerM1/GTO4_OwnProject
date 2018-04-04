using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void QuitGame()
	{
		Application.Quit();
	}

	public void LoadScene(int index)
	{
		SceneManager.LoadScene(index);
	}
}
