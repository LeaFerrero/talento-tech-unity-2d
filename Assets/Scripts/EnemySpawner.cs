using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que hereda de Spawner para generar enemigos en la escena.
/// Permite seleccionar aleatoriamente entre diferentes tipos de enemigos según una probabilidad.
/// </summary>
public class EnemySpawner : Spawner
{
    /// <summary>
    /// Lista de prefabs de enemigos disponibles para instanciar.
    /// </summary>
    [SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] protected StopwatchTimer stopwatchTimer;
    private int lastCheckedMinute = 0;
    private int currentIndex = 0;

    protected override void Update()
    {
        base.Update();
        if (stopwatchTimer.Minutes != lastCheckedMinute)
        {
            lastCheckedMinute = stopwatchTimer.Minutes;

            // Aumenta el índice cíclicamente
            currentIndex = (currentIndex + 1) % enemyPrefabs.Count;

            Debug.Log("Nuevo índice de oleada: " + currentIndex);

            // Acá podrías invocar tu Spawn con ese índice, por ejemplo:
            // SpawnWave(currentIndex);
        }
    }

    /// <summary>
    /// Método que instancia un nuevo enemigo en la posición dada.
    /// Selecciona el prefab según una probabilidad para variar el tipo de enemigo generado.
    /// Sobrescribe el método abstracto Spawn de la clase base Spawner.
    /// </summary>
    /// <param name="position">Posición en el mundo donde se generará el enemigo.</param>
    protected override void Spawn(Vector2 position)
    {
        int index = currentIndex;

        // Obtiene el prefab seleccionado
        GameObject enemyToSpawn = enemyPrefabs[index];

        // Instancia el enemigo en la posición indicada sin rotación
        GameObject newEnemy = Instantiate(enemyToSpawn, position, Quaternion.identity);

        // Añade el enemigo generado a la lista de objetos spawnados para su control
        this.spawnedObjects.Add(newEnemy);
    }
}
