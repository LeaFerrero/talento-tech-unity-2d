using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile
{
    private bool isDestroyed = false;  
    private SpriteRenderer rocketSpriteRenderer; 
    private Transform transformExplosion;  
    private Animator explosionAnimator;
    private Animator moveAnimator;
    private Collider2D rocketCollider;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rocketSpriteRenderer = GetComponent<SpriteRenderer>();
        transformExplosion = transform.Find("Explosion");
        explosionAnimator = transformExplosion.GetComponent<Animator>();
        rocketCollider = GetComponent<Collider2D>();
        moveAnimator = transform.Find("Thruster").GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void FixedUpdate()
    {
        if (!isDestroyed)
        {
            base.FixedUpdate();
            moveAnimator.SetFloat("Velocity", 1);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
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
            Explode();
        }
    }

    public void Explode()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            rb.velocity = Vector2.zero;
            Destroy(gameObject, 0.60f);
            rocketSpriteRenderer.enabled = false;  
            rocketCollider.enabled = false;  
            transformExplosion.gameObject.SetActive(true); 

            if (explosionAnimator != null)
            {
                explosionAnimator.Play("ExplosionRocket", 0);  // Reproducir animación de explosión
            }
        }
    }
}
