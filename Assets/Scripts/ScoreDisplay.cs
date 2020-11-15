using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//usamos el namespace y traemos UI
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    //Creamos variables para guardar los objetos
    Text scoreText;
    GameSession gameSession;
    
    // Start is called before the first frame update
    void Start()
    {
        //Traemos el componente del gameObject que queremos usar. En este caso Text
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        //Podemos acceder a las propiedades y métodos de gamesession y guardarlos en la propiedad text. Lo convertimos en string
        scoreText.text = gameSession.GetScore().ToString();
        
    }
}
