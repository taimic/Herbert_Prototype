using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour { 
    // The current application index
    private int currentApplicationIndex;

	// Use this for initialization
	void Start () {
        currentApplicationIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentApplicationIndex < 0) currentApplicationIndex = 0;

	}

    // Loads the next level
    public void loadNextLevel() {
        int nextApplicationIndex = currentApplicationIndex + 1;
        if (nextApplicationIndex >= SceneManager.sceneCountInBuildSettings) nextApplicationIndex = 0;

        SceneManager.LoadSceneAsync(nextApplicationIndex);
    }
}