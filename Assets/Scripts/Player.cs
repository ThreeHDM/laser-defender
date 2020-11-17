using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    //// CONFIGURATION PARAMETERS -------------------------------
    [Header("Player")] // agrega un header
    //Creamos un campo para configurar la velocidad de movimiento
    [SerializeField] float moveSpeed = 10f;
    //Creamos un campo para configurar el padding de los límites al movimiento
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSoud;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    [Header("Projectile")] // agrega un header
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    //Creamos la variable para almacenar la corutina de disparo
    Coroutine firingCoroutine;

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

    

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer){return;}
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSoud, Camera.main.transform.position, deathSoundVolume); 
    }

    public int GetHealth()
    {
        return health;
    }

    private void Fire()
    {
        //A GetButtonDown le pasamos un string con el nombre del input. Para ver el input hay que ir a Edit->ProjectSettings->Input Manager
        if (Input.GetButtonDown("Fire1"))
        {
            //Comenzamos la Couroutine y la almacenamos en la variable de tipo Couroutine
            firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            /*
            //Este método detiene a todas las coroutines. Es peligroso pues detiene todo
            StopAllCoroutines();
            */

            //Este método detiene una coroutine específica
            StopCoroutine(firingCoroutine);
            
        }
    }

    //Creamos el metodo courotine y colocamos la instanciacion del objeto GameObject laser
    IEnumerator FireContinously()
    {
        //Una vez que se llama FireContinously todo dentro del while se repetirá siempre
        while (true)
        {
            //Instanciamos el GameObject con el método Instantiate y creando una nueva variable de tipo gameObject. El "as" sirve para reasegurar que es un GObject.
            //El primer param es el gameObject
            //El segundo param es la posición donde lo instanciamos. Allí queremos que esté.
            //El tercer param es la rotación. En este caso Quaternion.identity -> usa la rotación que tiene por default.
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            //Una vez instanciado el objeto laser accedemos al componente RigidBody2d y afectamos su velocidad con un objeto Vector2 de x = 0 e y = projectileSpeed
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
        
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

    
}
