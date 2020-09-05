using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //Removemos el SerializeField de waveConfig
    WaveConfig waveConfig; 
    //El tipo es Transform porque queremos afectar la position
    List<Transform> waypoints; 
    //Velocidad del enemigo al moverse
    
    //creamos un índice para los waypoints
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWayPoints(); //Accedemos al método GetWayPoints() del ScriptableObject

        //Afectamos la position del Enemy (ya que este script está asociado a ese GObject) asignado la posicion del waypoint del indice en que estamos (el 0)
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //Creamos este método público para setear el waveConfig (Setter)
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig; //Me refiero a la class con this y seteo la propiedad con el param que me pasen en SetWaveConfig()
    }

    private void Move()
    {
        //Usamos Count para las listas. Si fuera un array usamos length. Restamos uno porque count cuenta desde 1. En este caso son 3 waypoints menos 1. Osea 0,1,2 elementos de la lista.
        if (waypointIndex <= waypoints.Count - 1)
        {   
            //targetPosition es la posicion del waypoint al que nos movemos. El que está activo en el índice ahora.
            var targetPosition = waypoints[waypointIndex].transform.position;

            //Esta es la velocidad. Multiplicamos por Time.deltaTime para que sea frame independent
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;

            //este método usa param1: posicion actual, param2: posicion a la que va, param3: 
            //MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta);
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                movementThisFrame
            );

            if (transform.position == targetPosition)
            {
                //Si la posición del targuet es igual a la posición del GameObject Enemy sumo uno al index.
                waypointIndex++;
            }
        }
        else
        {
            //Si el índice es igual al máximo de count destruímos al enemy
            Destroy(gameObject);
        }
    }
}
