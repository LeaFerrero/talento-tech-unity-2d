using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el comportamiento de un asteroide en el juego.
/// Cuando colisiona con el jugador o enemigo, aplica daño, cambia su color momentáneamente y reproduce una animación de explosión antes de destruirse.
/// </summary>
public class Ateroid : MonoBehaviour
{
    [Header("Pickup Settings")]
    public List<GameObject> pickupPrefabs; // Asignás prefabs desde el inspector
    public float dropChance = 0.5f; // 50% de probabilidad de soltar algo

    [SerializeField] private float impactDamage;  // Daño que causa al impactar
    private bool isDestroyed = false;  // Indica si el asteroide ya fue destruido para evitar acciones repetidas
    private SpriteRenderer asteroidSpriteRenderer;  // Referencia al SpriteRenderer del asteroide para manipular la visibilidad
    private Transform transformExplosion;  // Transform del objeto hijo que contiene la animación de explosión
    private Animator explosionAnimator;  // Animator para controlar la animación de explosión
    private Collider2D asteroidCollider;  // Collider del asteroide para detectar colisiones
    //private SpriteRenderer mainSpriteRenderer;  // Referencia principal al SpriteRenderer para cambiar color

    // Método llamado al iniciar la escena o cuando se instancia el objeto
    void Start()
    {
        // Buscar el hijo llamado "Explosion" y obtener componentes necesarios
        transformExplosion = transform.Find("Explosion");
        explosionAnimator = transformExplosion.GetComponent<Animator>();
        asteroidSpriteRenderer = GetComponent<SpriteRenderer>();
        asteroidCollider = GetComponent<Collider2D>();
        //mainSpriteRenderer = GetComponent<SpriteRenderer>();

        // Desactivar la animación de explosión al inicio
        if (transformExplosion != null)
        {
            transformExplosion.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("No se encontró el hijo 'Explosion'");
        }
    }

    // Update está vacío porque no se usa lógica cada frame, pero queda para futuras funciones
    void Update()
    {
    }

    /// <summary>
    /// Detecta colisiones con otros objetos 2D.
    /// Si colisiona con jugador o enemigo, aplica daño y comienza el proceso de destrucción con animación.
    /// </summary>
    /// <param name="collision">Collider del objeto que colisionó con el asteroide</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyDamageColor();  // Cambia color a rojo para feedback visual de daño

        // Si el objeto colisionado tiene tag "Player" o "Enemy"
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            // Intentar obtener el script SpaceshipController para aplicar daño
            SpaceshipController spaceshipScript = collision.gameObject.GetComponent<SpaceshipController>();

            if (spaceshipScript != null)
            {
                spaceshipScript.Damage(impactDamage); // Aplicar daño
            }
        }

        // Ejecutar la explosión luego de 0.1 segundos
        Invoke(nameof(Explode), 0.1f);
    }

    /// <summary>
    /// Maneja la destrucción del asteroide:
    /// Desactiva la visualización, collider y activa la animación de explosión,
    /// luego destruye el objeto después de un tiempo para que se vea la animación.
    /// </summary>
    public void Explode()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            Destroy(gameObject, 0.60f); // Destruir objeto tras 0.6 segundos para dejar la animación
            asteroidSpriteRenderer.enabled = false;  // Ocultar sprite del asteroide
            asteroidCollider.enabled = false;  // Desactivar colisiones
            transformExplosion.gameObject.SetActive(true);  // Activar objeto de la explosión

            if (explosionAnimator != null)
            {
                explosionAnimator.Play("ExplosionAsteroid", 0);  // Reproducir animación de explosión
            }

            Invoke(nameof(TrySpawnPickup), 0.5f);
        }
    }

    /// <summary>
    /// Cambia el color del sprite del asteroide a rojo para indicar daño.
    /// </summary>
    void ApplyDamageColor()
    {
        asteroidSpriteRenderer.color = Color.red;
    }

    /// <summary>
    /// Reestablece el color blanco original del sprite.
    /// Actualmente no se llama, pero queda preparado para usar si se quiere revertir el color.
    /// </summary>
    void RevertColor()
    {
        asteroidSpriteRenderer.color = Color.white;
    }

    private void TrySpawnPickup()
    {
        if (pickupPrefabs != null && pickupPrefabs.Count > 0 && UnityEngine.Random.value <= dropChance)
        {
            float totalWeight = 0f;

            foreach (var prefab in pickupPrefabs)
            {
                Pickup pickupScript = prefab.GetComponent<Pickup>();
                if (pickupScript != null)
                {
                    totalWeight += pickupScript.DropWeight;
                }
            }

            float randomValue = UnityEngine.Random.Range(0f, totalWeight);
            float cumulative = 0f;

            foreach (var prefab in pickupPrefabs)
            {
                Pickup pickupScript = prefab.GetComponent<Pickup>();
                if (pickupScript != null)
                {
                    cumulative += pickupScript.DropWeight;
                    if (randomValue <= cumulative)
                    {
                        Instantiate(prefab, transform.position, Quaternion.identity);
                        break;
                    }
                }
            }
        }
        // Si la lista es null o vacía, simplemente no hace nada y sigue
    }
}
