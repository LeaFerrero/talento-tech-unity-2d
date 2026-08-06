using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Comportamiento del proyectil disparado por el jugador.
/// Se mueve en una dirección, tiene velocidad y puede dañar enemigos.
/// </summary>
public class Projectile : MonoBehaviour
{
    protected Vector2 direction; // Dirección hacia donde se moverá el proyectil
    public float desotroyAfterSeconds;  // Tiempo tras el cual el proyectil se destruirá automáticamente (en segundos)
    protected WeaponData weaponData;
    protected Rigidbody2D rb;  // Referencia al componente Rigidbody2D para aplicar movimiento físico

    /// <summary>
    /// Se ejecuta al iniciar el objeto. Programa la autodestrucción del proyectil y obtiene el Rigidbody2D.
    /// </summary>
    protected virtual void Start()
    {
        // Destruye el proyectil tras un tiempo determinado
        Destroy(gameObject, desotroyAfterSeconds);

        // Obtiene el componente Rigidbody2D del objeto
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Se llama una vez por frame (vacío en este caso, no se utiliza).
    /// </summary>
    protected virtual void Update()
    {
        // No se usa, pero se mantiene por si se necesita lógica futura
    }

    /// <summary>
    /// Se llama en intervalos fijos, ideal para aplicar movimiento basado en física.
    /// </summary>
    protected virtual void FixedUpdate()
    {
        // Asigna la velocidad al Rigidbody para mover el proyectil
        if (weaponData != null)
        {
            rb.velocity = transform.up * weaponData.Speed * Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// Se ejecuta cuando el proyectil entra en contacto con otro collider con "Is Trigger" activado.
    /// </summary>
    /// <param name="other">Collider del objeto con el que colisionó</param>
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Ignora colisiones con el jugador
        if (!other.CompareTag("Player") && !other.CompareTag("Pickup"))
        {
            // Si colisiona con un enemigo, le aplica daño
            if (other.CompareTag("Enemy"))
            {
                SpaceshipController enemy = other.GetComponent<SpaceshipController>();
                if (enemy != null)
                {
                    enemy.Damage(weaponData.Damage); // Aplica daño al enemigo
                }
                // Destruye el proyectil en cualquier otro caso que no sea el jugador
            }
            Destroy(gameObject);
        }
    }
    public virtual void Initialize(WeaponData weaponData)
    {
        this.weaponData = weaponData;
    }
}
