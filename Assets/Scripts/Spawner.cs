using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Clase base para gestionar la generación (spawn) de objetos en el juego.
/// Controla la frecuencia de spawn, la posición aleatoria alrededor del jugador,
/// y evita generar objetos demasiado cerca entre sí o dentro de la vista de la cámara.
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField] private float timer;  // Contador de tiempo para controlar el intervalo de spawn
    [SerializeField] private float spawnInterval;  // Intervalo de tiempo entre cada intento de spawn
    [SerializeField] private int maxSpawnedObjects;  // Límite máximo de objetos que pueden estar activos simultáneamente
    [SerializeField] private float minDistanceBetweenSpawns = 2f;  // Distancia mínima entre dos objetos spawned para evitar que estén muy juntos
        
    protected List<GameObject> spawnedObjects = new List<GameObject>();  // Lista para guardar los objetos ya generados y hacer seguimiento
    private Transform player;  // Referencia al transform del jugador para generar objetos en relación a su posición
    public float minSpawnDistance = 5f;  // Distancia mínima desde el jugador para generar objetos
    public float maxSpawnDistance = 10f; // Distancia máxima desde el jugador para generar objetos
    private Camera mainCamera;  // Referencia a la cámara principal para verificar si la posición de spawn está dentro o fuera del viewport

    protected virtual void Start()
    {
        // Buscar el jugador en la escena mediante su tag "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("No se encontró el jugador con tag 'Player'");
        }

        // Obtener la cámara principal automáticamente
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No se encontró la cámara principal");
        }
    }

    protected virtual void Update()
    {
        // Incrementar el timer con el tiempo transcurrido desde el último frame
        timer += Time.deltaTime;

        // Limpiar la lista de objetos eliminados para evitar referencias nulas
        CleanUpSpawnedNullObjects();

        // Cuando el timer supera el intervalo definido, intentar generar un nuevo objeto
        if (timer >= spawnInterval)
        {
            TrySpawn();
            timer = 0f;  // Reiniciar el timer después de intentar spawn
        }
    }

    /// <summary>
    /// Método que intenta generar un objeto en una posición válida
    /// </summary>
    protected virtual void TrySpawn()
    {
        // Verificar que existan referencias válidas al jugador y la cámara
        if (player != null && mainCamera != null)
        {
            Vector2 spawnPosition;

            int attempts = 10; // Número máximo de intentos para encontrar una posición válida
            for (int i = 0; i < attempts; i++)
            {
                // Solo intentamos spawnear si no hemos llegado al máximo de objetos activos
                if (spawnedObjects.Count < maxSpawnedObjects)
                {
                    // Generar una dirección aleatoria normalizada dentro del círculo unitario
                    Vector2 randomDir = UnityEngine.Random.insideUnitCircle.normalized;

                    // Elegir una distancia aleatoria entre min y max para el spawn desde el jugador
                    float distance = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);

                    // Calcular la posición de spawn sumando la dirección escalada a la posición del jugador
                    spawnPosition = (Vector2)player.position + randomDir * distance;

                    // Convertir la posición a coordenadas viewport para comprobar si está dentro de la cámara
                    Vector3 viewportPoint = mainCamera.WorldToViewportPoint(spawnPosition);
                    // Condición para asegurarse que la posición está fuera de la cámara
                    // y que la posición es válida respecto a otros objetos spawnados
                    if ((viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1)
                        && IsPositionValid(spawnPosition, minDistanceBetweenSpawns))
                    {
                        // Llamar al método para crear el objeto en la posición determinada
                        Spawn(spawnPosition);
                        break;
                    }
                }
            }
        }

        // Mensaje para saber si no se pudo encontrar una posición válida fuera de la cámara
        Debug.Log("No se encontró posición fuera de cámara");
    }

    /// <summary>
    /// Valida que la posición esté suficientemente alejada de otros objetos spawnados
    /// </summary>
    /// <param name="position">Posición a validar</param>
    /// <param name="minDistance">Distancia mínima permitida respecto a otros objetos</param>
    /// <returns>True si la posición es válida, False si está muy cerca de otro objeto</returns>
    private bool IsPositionValid(Vector2 position, float minDistance)
    {
        bool valid = true;

        foreach (var obj in spawnedObjects)
        {
            if (obj != null)
            {
                // Calcular la distancia entre la posición propuesta y el objeto existente
                float dist = Vector2.Distance(position, obj.transform.position);

                // Si la distancia es menor que la mínima permitida, la posición no es válida
                if (dist < minDistance)
                {
                    valid = false;
                }
            }
        }

        return valid;
    }

    /// <summary>
    /// Limpia la lista de objetos spawnados removiendo aquellos que hayan sido destruidos (nulos)
    /// </summary>
    protected void CleanUpSpawnedNullObjects()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] == null)
            {
                spawnedObjects.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Método virtual para spawnear el objeto en la posición dada.
    /// Se debe sobreescribir en clases derivadas para definir qué objeto se instancia.
    /// </summary>
    /// <param name="position">Posición donde se generará el objeto</param>
    protected virtual void Spawn(Vector2 position)
    {
    }
}
