using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
        
    }

    public void LoadGame()
    {
        // Se puede cargar una escena con strings o con el scene index. El problema con los strings es que no son escalables. Si cambia el nombre de la escena no cambia
        SceneManager.LoadScene("Game");
        Debug.Log("test");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
