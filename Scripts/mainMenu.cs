using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject settingsSymbol; 
    public GameObject ScoreTxtCanvas;
   


    private void Update()
    {
        // Check if the Esc key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the settings menu
            ToggleSettingsMenu();
        }
    }

    public void Level1()
    {
        SceneManager.LoadScene(2); 
    }

    public void Level2()
    {
        SceneManager.LoadScene(3);
    }

    public void ScoreLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {

        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    } 
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % 4; 
        SceneManager.LoadScene(nextSceneIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleSettingsMenu()
    {
        if (settings != null && settingsSymbol != null)
        {
            // Toggle the active state of the settings container
            bool isActive = settings.activeSelf;
            settings.SetActive(!isActive);
            settingsSymbol.SetActive(isActive);
            ScoreTxtCanvas.SetActive(isActive);
            Debug.Log("Settings menu has been " + (isActive ? "deactivated" : "activated"));
        }
        else
        {
            Debug.LogError("Settings container is not assigned!");
        }
    }


}
