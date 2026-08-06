using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que hereda de Spawner para generar asteroides en la escena.
/// </summary>
public class AsteroidSpawner : Spawner
{
    /// <summary>
    /// Prefab del asteroide que será instanciado.
    /// </summary>
    [SerializeField] private GameObject asteroid;

    /// <summary>
    /// Método que instancia un nuevo asteroide en la posición dada.
    /// Este método sobreescribe el método abstracto Spawn definido en la clase base Spawner.
    /// </summary>
    /// <param name="position">Posición en el mundo donde se creará el asteroide.</param>
    protected override void Spawn(Vector2 position)
    {
        // Crea un nuevo asteroide en la posición indicada con rotación por defecto
        GameObject newAsteroid = Instantiate(asteroid, position, Quaternion.identity);

        // Añade el asteroide instanciado a la lista de objetos generados para control posterior
        this.spawnedObjects.Add(newAsteroid);
    }
}
