using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador específico para la nave del jugador.
/// Hereda de SpaceshipController y agrega control por mouse para movimiento y rotación.
/// </summary>
public class PlayerController : SpaceshipController
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image experienceBar;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    [SerializeField] private PowerUpManager powerUpManager;
    [SerializeField] private PlayerWeaponsUI playerWeaponsUI;
    [SerializeField] private GameOverMenu gameOverMenu;
    
    private float minimunDistance;    // Distancia mínima para que la nave comience a moverse hacia el cursor
    protected Animator animator;                       // Animator para el thruster (propulsor)
    private float distance;                            // Distancia actual entre nave y cursor del mouse
    private int level = 1;
    private float currentExperience;
    private float maxExperience = 100;

    [Header("Staring weapon")]
    [SerializeField] private WeaponPowerUp startingWeapon;

    [Header("Weapon Slots")]
    [SerializeField] private Transform frontWeaponSlot;
    [SerializeField] private Transform leftWeaponSlot;
    [SerializeField] private Transform rightWeaponSlot;
    [SerializeField] private Transform backWeaponSlot;

    private GameObject frontWeapon;
    private GameObject leftWeapon;
    private GameObject rightWeapon;
    private GameObject backWeapon;

    /// <summary>
    /// Inicializa el jugador. Llama al Start base, asigna el Animator del thruster y define la animación de explosión.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        // Busca el hijo "Thruster" y obtiene su Animator para controlar animaciones de propulsión
        animator = transform.Find("Thruster").GetComponent<Animator>();
        // Define el nombre de la animación que se reproducirá al explotar
        explosionAnimationName = "ExplosionPlayerSpaceship";
        startingWeapon.ApplyPowerUp(this, WeaponDirection.Front);
        playerWeaponsUI.SetWeaponIcon(startingWeapon.Icon, WeaponDirection.Front);
        UpdateHealthBar();
        UpdateCurrentHealthText();
        UpdateMaxHealthText();
    }

    /// <summary>
    /// Controla el movimiento de la nave en función de la distancia al cursor y el input del mouse.
    /// </summary>
    protected override void SpaceshipMovement()
    {
        // Calcula la distancia actual desde la nave hasta el cursor (en dirección)
        distance = direction.magnitude;

        // Si el botón izquierdo del mouse está presionado y la distancia es mayor que la mínima
        if (Input.GetMouseButton(0) && distance > minimunDistance)
        {
            // Mueve la nave hacia adelante (transform.up) multiplicado por velocidad
            movement = transform.up * speed;
            // Activa animación de propulsor encendido
            animator.SetFloat("Velocity", 1);
        }
        else
        {
            // No mover la nave si no se cumple la condición
            movement = Vector2.zero;
            // Apaga la animación del propulsor
            animator.SetFloat("Velocity", 0);
        }
    }

    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);
        UpdateHealthBar();
        UpdateCurrentHealthText();
        if (currentHealth <= 0)
        {
            gameOverMenu.ShowGameOverMenu();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
        UpdateCurrentHealthText();
    }

    public void ExperienceUp(float experienceAmount)
    {
        currentExperience += experienceAmount;
        
        if (currentExperience >= maxExperience)
        {
            // MOSTRAR la barra al 100% antes de levelear
            currentExperience = maxExperience;
            UpdateExperienceBar();

            // Iniciar rutina para levelear en el siguiente frame
            StartCoroutine(HandleLevelUp());
        }
        else
        {
            UpdateExperienceBar();
        }
    }

    private IEnumerator HandleLevelUp()
    {
        // Mostrar la barra llena por un corto tiempo (ej: 0.2 segundos)
        yield return new WaitForSeconds(0.2f);

        LevelUp(); // esto pone currentExperience en 0 y actualiza la barra
        UpdateExperienceBar();

        // Esperar otro pequeño frame si querés mostrar la barra "vacía" antes de continuar
        yield return new WaitForSeconds(0.05f);

    }


    /// <summary>
    /// Controla la rotación de la nave para que apunte hacia la posición del cursor.
    /// </summary>
    protected override void SpaceshipRotation()
    {
        // Obtiene la posición del mouse en coordenadas del mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calcula la dirección normalizada desde la nave hasta el mouse
        direction = (mousePosition - transform.position).normalized;
        // Convierte la dirección en un ángulo en grados para la rotación del Rigidbody2D
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Devuelve la dirección en la que la nave está "disparando", que es hacia adelante (transform.up).
    /// </summary>
    /// <returns>Vector2 dirección de disparo</returns>
    public Vector2 GetFiringDirection()
    {
        return transform.up;
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = (currentHealth / maxHealth);
    }
    
    private void UpdateExperienceBar()
    {
        experienceBar.fillAmount = (currentExperience / maxExperience);
    }

    private void LevelUp()
    {
        level++;
        currentExperience = 0f;
        maxExperience += 25;
        powerUpManager.ShowPowerUps();
        UpdateLevelShow();
    }

    private void UpdateLevelShow()
    {
        levelNumberText.text = level.ToString();
    }

    private void UpdateCurrentHealthText()
    {
        currentHealthText.text = currentHealth.ToString();
    }

    private void UpdateMaxHealthText()
    {
        maxHealthText.text = maxHealth.ToString();
    }

    public void IncreaseMaxHealth(float healthIncrease)
    {
        maxHealth += healthIncrease;
        UpdateMaxHealthText();
        UpdateHealthBar();
    }

    public void EquipWeapon(GameObject weaponPrefab, WeaponDirection slot)
    {
        switch (slot)
        {
            case WeaponDirection.Front:
                if (frontWeapon != null)
                {
                    Destroy(frontWeapon);
                }
                frontWeapon = Instantiate(weaponPrefab, frontWeaponSlot);
                break;
            case WeaponDirection.Left:
                if (leftWeapon != null)
                {
                    Destroy(leftWeapon);
                }
                leftWeapon = Instantiate(weaponPrefab, leftWeaponSlot);
                break;
            case WeaponDirection.Right:
                if (rightWeapon != null)
                {
                    Destroy(rightWeapon);
                }
                rightWeapon = Instantiate(weaponPrefab, rightWeaponSlot);
                break;
            case WeaponDirection.Back:
                if (backWeapon != null)
                {
                    Destroy(backWeapon);
                }
                backWeapon = Instantiate(weaponPrefab, backWeaponSlot);
                break;
        }
    }
}
