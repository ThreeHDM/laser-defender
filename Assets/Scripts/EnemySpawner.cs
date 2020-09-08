using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs; //creamos esta prop para almacenar las configs de las waves
    [SerializeField] int startingWave = 0; // el indice de las waves

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    //creamos una coroutine para invocar las waves
    private IEnumerator SpawnAllWaves()
    {
        //Escribimos un bucle for. Creamos waveIndex y lo inicializamos igual a la propiedad startingWave. Como waveconfigs es una lista podemos usar Count para conocer su numero de elementos
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    //Creamos la coroutine (Es un método que suspende su ejecución hasta que se cumpla una condición). Pasamos el waveconfig de la current wave como param
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {



            //Debug.Log(enemyCount);

            //Instanciamos el Enemy con Instantiate()
            //El primer param de Instantiate es el gameObject
            //El segundo param es la posición donde lo instanciamos. Allí queremos que esté.
            //El tercer param es la rotación. En este caso Quaternion.identity -> usa la rotación que tiene por default.
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWayPoints()[0].transform.position, //retornamos el waypoint 0 del way config porque esta es la posición inicial. GetWayPoints retorna una lista y por eso usamos los brackets.
                Quaternion.identity);

            //Accedemos al componente EnemyPathing y luego al método público SetWaveConfig y pasamos el waveConfig en su param.
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

             //Debug.Log(waveConfig.GetTimeBetweenSpawns());
             yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }

        
    }
}
