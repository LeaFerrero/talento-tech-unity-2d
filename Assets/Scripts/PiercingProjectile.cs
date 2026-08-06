using UnityEngine;

public class PiercingProjectile : Projectile
{
    private int piercesDone = 0;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Pickup"))
        {
            if (other.CompareTag("Enemy"))
            {
                SpaceshipController enemy = other.GetComponent<SpaceshipController>();
                if (enemy != null)
                {
                    enemy.Damage(weaponData.Damage);
                    piercesDone++;

                    if (piercesDone >= weaponData.MaxPierces)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
