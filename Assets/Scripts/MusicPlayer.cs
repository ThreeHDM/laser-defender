using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Awake es lo primero que sucede en el juego
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        //GetType() obtiene el tipo de la clase en este caso MusicPlayer
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            //Destruimos el objeto en el que estamos
            Destroy(gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
