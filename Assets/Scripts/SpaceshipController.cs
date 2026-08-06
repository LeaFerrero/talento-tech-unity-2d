using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controlador base para una nave espacial. Maneja movimiento, rotación, daños y explosión.
/// Está pensado para ser extendido por clases hijas que implementen el movimiento y rotación específicos.
/// </summary>
public class SpaceshipController : MonoBehaviour
{
    [Header("Spaceship Stats")]
    [SerializeField] protected int speed;             // Velocidad de movimiento de la nave
    [SerializeField] protected float maxHealth;       // Vida maxima de la nave
    protected float currentHealth;   // Vida actual de la nave
    protected bool isDestoyed = false;                // Estado para evitar acciones tras destrucción
    protected Transform explosionTransform;           // Referencia al objeto hijo de explosión
    protected List<Transform> thrusterTransforms = new List<Transform>(); // Lista de propulsores para controlar efectos visuales
    protected Animator explosionAnimator;             // Animador para la explosión
    protected SpriteRenderer mainSpriteRenderer;      // Sprite principal de la nave
    protected Vector2 direction;                      // Dirección actual de la nave
    protected Vector2 movement = new Vector2(0, 0);   // Vector de movimiento calculado
    protected float rotationCorrection = 90f;         // Corrección para la rotación del sprite, para alinear la dirección
    protected Rigidbody2D rb;                         // Referencia al Rigidbody2D para física
    protected float angle;                            // Ángulo de rotación actual calculado
    protected string explosionAnimationName;          // Nombre de la animación de explosión a reproducir
    protected Collider2D spaceshipCollider;           // Collider de la nave para colisiones
 

    /// <summary>
    /// Inicialización de referencias, búsqueda de propulsores y configuración inicial.
    /// </summary>
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FindThrusters();  // Encuentra y almacena los hijos con tag "Thruster"
        mainSpriteRenderer = GetComponent<SpriteRenderer>();
        explosionTransform = transform.Find("Explosion");
        spaceshipCollider = GetComponent<Collider2D>();
        currentHealth = maxHealth;

        if (explosionTransform != null)
        {
            explosionAnimator = explosionTransform.GetComponent<Animator>();
            explosionTransform.gameObject.SetActive(false);  // Oculta la explosión al inicio
        }
        else
        {
            Debug.LogWarning("No se encontró el hijo 'Explosion'");
        }
    }

    /// <summary>
    /// Actualización por frame: control de rotación y movimiento (implementado en clases derivadas).
    /// </summary>
    protected virtual void Update()
    {
        SpaceshipRotation();
        SpaceshipMovement();
    }

    /// <summary>
    /// Actualización física para aplicar movimiento y rotación (fija para consistencia física).
    /// </summary>
    protected virtual void FixedUpdate()
    {
        ApplyRotation();
        ApplyMovement();
    }

    /// <summary>
    /// Aplica el vector de movimiento al Rigidbody2D. Detiene la nave si está destruida.
    /// </summary>
    protected virtual void ApplyMovement()
    {
        if (isDestoyed)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = movement * Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// Aplica la rotación calculada al Rigidbody2D, corrigiendo por el offset del sprite.
    /// </summary>
    protected virtual void ApplyRotation()
    {
        rb.MoveRotation(angle - rotationCorrection);
    }

    /// <summary>
    /// Método público para infligir daño a la nave.
    /// Aplica daño, cambia temporalmente el color y maneja la destrucción si la vida llega a cero.
    /// </summary>
    /// <param name="damageAmount">Cantidad de daño a aplicar</param>
    public virtual void Damage(float damageAmount)
    {
        ApplyDamageColor();

        // Vuelve al color original luego de 0.1 segundos
        Invoke(nameof(RevertColor), 0.1f);

        this.currentHealth -= damageAmount;

        if (currentHealth <= 0 && !isDestoyed)
        {
            currentHealth = 0;
            Explode(explosionAnimationName);
        }
    }


    /// <summary>
    /// Método para calcular el movimiento de la nave (debe ser implementado en clases hijas).
    /// </summary>
    protected virtual void SpaceshipMovement()
    {
        // Implementar en subclases
    }

    /// <summary>
    /// Método para calcular la rotación de la nave (debe ser implementado en clases hijas).
    /// </summary>
    protected virtual void SpaceshipRotation()
    {
        // Implementar en subclases
    }

    /// <summary>
    /// Cambia el color del sprite principal a rojo para indicar daño.
    /// </summary>
    void ApplyDamageColor()
    {
        mainSpriteRenderer.color = Color.red;
    }

    /// <summary>
    /// Revierte el color del sprite principal a blanco (color original).
    /// </summary>
    void RevertColor()
    {
        mainSpriteRenderer.color = Color.white;
    }

    /// <summary>
    /// Maneja la explosión de la nave: desactiva componentes, reproduce animación y destruye el objeto.
    /// </summary>
    /// <param name="explosionAnimationName">Nombre de la animación de explosión a reproducir</param>
    protected virtual void Explode(string explosionAnimationName)
    {
        isDestoyed = true;
        mainSpriteRenderer.enabled = false;
        spaceshipCollider.enabled = false;

        // Desactivar todos los propulsores visuales
        foreach (Transform thruster in thrusterTransforms)
        {
            thruster.gameObject.SetActive(false);
        }

        // Mostrar y reproducir la animación de explosión
        explosionTransform.gameObject.SetActive(true);
        explosionAnimator.Play(explosionAnimationName, 0);

        // Destruir el objeto nave después de 0.45 segundos para que termine la animación
        Destroy(gameObject, 0.45f);
    }

    /// <summary>
    /// Busca entre los hijos todos los objetos con tag "Thruster" y los agrega a la lista thrusterTransforms.
    /// </summary>
    protected void FindThrusters()
    {
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            if (child.CompareTag("Thruster"))
            {
                thrusterTransforms.Add(child);
            }
        }
    }
}
