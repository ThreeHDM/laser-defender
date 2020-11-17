using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    //Creamos variables para guardar los objetos
    Text healthText;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        //Traemos el componente del gameObject que queremos usar. En este caso Text
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Podemos acceder a las propiedades y métodos de gamesession y guardarlos en la propiedad text. Lo convertimos en string
        healthText.text = player.GetHealth().ToString();

    }
}
