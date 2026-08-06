using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controlador para la nave enemiga. Persigue al jugador y le causa daño al colisionar.
/// </summary>
public class EnemyController : SpaceshipController
{
    [SerializeField] private float collisionDamage;   // Daño que inflige al chocar con el jugador
    [SerializeField] private GameObject experiencePickup;
    [SerializeField] private float experienceDropRate;
    private Transform target;                         // Referencia al jugador para perseguirlo

    /// <summary>
    /// Inicializa el enemigo, llama al base.Start() y define la animación de explosión.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        explosionAnimationName = "ExplosionEnemySpaceship";
    }

    /// <summary>
    /// Update base.
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// FixedUpdate base.
    /// </summary>
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// Lógica de colisión. Si choca con el jugador, le inflige daño y luego se autodestruye.
    /// </summary>
    /// <param name="collision">Colisión detectada</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerSpaceship = collision.gameObject.GetComponent<PlayerController>();

            if (playerSpaceship != null)
            {
                // Le inflige daño al jugador
                playerSpaceship.Damage(collisionDamage);
                // Se autodestruye infligiéndose su vida total como daño
                this.Damage(this.currentHealth);
            }
        }
    }

    /// <summary>
    /// Asigna como objetivo al jugador si aún no fue asignado.
    /// </summary>
    private void AssignTarget()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogWarning("EnemyController: No se encontró un objeto con tag 'Player'.");
            }
        }
    }

    /// <summary>
    /// Movimiento de la nave enemiga. Avanza hacia adelante si tiene un objetivo.
    /// </summary>
    protected override void SpaceshipMovement()
    {
        movement = (target != null) ? transform.up * speed : Vector2.zero;
    }

    /// <summary>
    /// Rotación de la nave enemiga hacia el jugador.
    /// </summary>
    protected override void SpaceshipRotation()
    {
        if (target == null)
        {
            AssignTarget();
        }
        else
        {
            direction = (target.position - transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
    }

    protected override void Explode(string explosionAnimationName)
    {
        base.Explode(explosionAnimationName);
        
        if (Random.value <= experienceDropRate)
        {
            Instantiate(experiencePickup, transform.position, Quaternion.identity);
        }
    }
}
