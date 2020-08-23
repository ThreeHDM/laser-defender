using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Creamos un campo para configurar la velocidad de movimiento
    [SerializeField] float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //Tomo el nombre "Horizontal" de Edit->ProjectSettings->Input Manager (En esos controles hay dos Horizontals (joystick y keyboard), con nombrar igual a ambos Unity sabe que me refiero a ambos)
        
        //Para asegurarnos que el juego funcione igual en computadoras veloces/lentas hay que usar time.deltaTime. Unity calcula cuánto tiempo le tomó a la computadora un frame y con eso unificamos el movimiento.

        //Luego de aplicar Time.deltaTime, la velocidad de movimiento desacelera. Para arreglar eso multiplicamos por la propiedad moveSpeed que configuramos previamente.

        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        //la nueva posición en el eje x será nuestra posición actual más una posición delta
        var newXPos = transform.position.x + deltaX;

        //Para que se mueva verticalmente
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;

        //Le pasamos como primer parametro X la nueva posición calculada del cambio almacenado en deltaX y hacemos igual con deltaY
        transform.position = new Vector2(newXPos, newYPos);


    }
}
