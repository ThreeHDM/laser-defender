using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    [SerializeField] float speedOfSpin  = 1f;
    // Update is called once per frame
    void Update()
    {
        //Rotamos el indice z (los dos primeros son x,y). A z le pasamos la velocidad x el Tiempo Delta para que sea frame rate independent (This means, no matter how fast or slow a machine is, DeltaTime is smaller or larger accordingly, basically adjusting calculations always to / per second)
        transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
    }
}
