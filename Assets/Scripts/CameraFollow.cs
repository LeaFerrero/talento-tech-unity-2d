using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el seguimiento de la cámara hacia un objetivo (normalmente el jugador),
/// con un desplazamiento configurable y movimiento suavizado para evitar saltos bruscos.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // Objetivo a seguir (normalmente el jugador)
    [SerializeField] private float offsetX;  // Desplazamiento horizontal de la cámara respecto al objetivo
    [SerializeField] private float offsetY;  // Desplazamiento vertical de la cámara respecto al objetivo
    [SerializeField] private float smoothSpeed = 0.125f;  // Velocidad de suavizado del movimiento de cámara (entre 0 y 1)
    private GameObject player;  // Referencia temporal al jugador en caso de no asignarse target manualmente
    private float offsetZ = -10f;  // Desplazamiento fijo en Z para mantener la cámara detrás de la escena 2D

    // Método llamado al iniciar la escena
    void Start()
    {
        AssignTarget();  // Asignar automáticamente el objetivo si no fue asignado desde el inspector
    }

    // Update vacío, podría usarse para otras lógicas si se desea
    void Update()
    {
    }

    // LateUpdate se usa para actualizar la posición de la cámara después de que todos los objetos se hayan movido
    private void LateUpdate()
    {
        CameraMovement();  // Actualizar posición de la cámara
    }

    /// <summary>
    /// Asigna el objetivo a seguir buscando el objeto con tag "Player" si no está asignado manualmente.
    /// </summary>
    void AssignTarget()
    {
        if (target == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogWarning("CameraFollow: No se encontró un objeto con tag 'Player'.");
            }
        }
    }

    /// <summary>
    /// Mueve la cámara suavemente hacia la posición deseada, que es la posición del objetivo más el offset.
    /// </summary>
    void CameraMovement()
    {
        if (target != null && target.gameObject != null)
        {
            // Definir el vector offset en X, Y y Z
            Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);

            // Calcular la posición deseada sumando el offset a la posición del objetivo
            Vector3 desiredPosition = target.position + offset;

            // Suavizar la transición entre la posición actual y la deseada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Asignar la posición suavizada a la cámara
            transform.position = smoothedPosition;
        }
    }
}
