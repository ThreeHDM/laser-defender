﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Creamos un campo para configurar la velocidad de movimiento
    [SerializeField] float moveSpeed = 10f;
    //Creamos un campo para configurar el padding de los límites al movimiento
    [SerializeField] float padding = 1f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        //creamos los limites para que el usuario no pueda moverse fuera de la cámara
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries()
    {
        // Almacenamos la cámara principal en gameCamera. Es de tipo Camara
        Camera gameCamera = Camera.main;

        //ViewportToWorldPoint -> convierte la posición de algo en su relación a lo que ve la cámara a un valor de espacio
        //fijamos un minimo y un máximo de x (de 0 a 1)
        //el método toma un vector3(x,y,z) - acá solo usamos x por eso dejamos en 0 los demás.
        //se almacena cuál es el "world space value" de x en relación con el ViewportValue de x en la cámara
        //pasamos el value del padding en minimo se lo sumamos (porque es negativo) en máximo se lo restamos)
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        //Eje Y
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
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
        //Mathf.Clamp - > Clamps the given value between the given minimum float and maximum float values. Returns the given value if it is within the min and max range.
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        //Para que se mueva verticalmente
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        //Le pasamos como primer parametro X la nueva posición calculada del cambio almacenado en deltaX y hacemos igual con deltaY
        transform.position = new Vector2(newXPos, newYPos);


    }
}