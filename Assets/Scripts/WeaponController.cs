using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Clase base para controladores de armas.
/// Contiene lógica general para disparo automático y atributos del arma.
/// </summary>
public class WeaponController : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;
    //[SerializeField] protected PlayerController playerSpaceship; // Referencia a la nave del jugador para obtener posición y dirección de disparo

    private float timer; // Temporizador para controlar el tiempo entre disparos

    /// <summary>
    /// Se ejecuta al iniciar el objeto (actualmente vacío).
    /// </summary>
    void Start()
    {
        timer = weaponData.FireRate;
    }

    /// <summary>
    /// Update es llamado una vez por frame.
    /// Llama a ManageFire() para manejar la lógica del disparo automático.
    /// </summary>
    protected virtual void Update()
    {
        ManageFire();
    }

    /// <summary>
    /// Incrementa el temporizador y dispara cuando se alcanza el tiempo indicado por fireRate.
    /// </summary>
    private void ManageFire()
    {
        timer += Time.deltaTime; // Aumenta el contador con el tiempo real entre frames

        // Si pasó suficiente tiempo desde el último disparo...
        if (timer >= weaponData.FireRate)
        {
            Shoot();     // Ejecuta el disparo
            timer = 0f;  // Reinicia el contador
        }
    }

    /// <summary>
    /// Método virtual para disparar (debe ser sobreescrito por clases hijas como BlasterController).
    /// </summary>
    protected virtual void Shoot()
    {
        GameObject projectile = Instantiate(weaponData.ProjectilePrefab, transform.position, transform.rotation);

        // Obtiene el script del proyectil para configurar sus propiedades
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(weaponData);
        }
    }
}
