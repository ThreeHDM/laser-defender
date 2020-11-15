using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
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
        //Cuando se llame este método (LoadgameOver) llamamos una corrutina
        StartCoroutine(WaitAndLoad());
        
    }

    IEnumerator WaitAndLoad()
    {
        //se espera la cantidad de segundos y luego se carga la escena
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
