using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Basic data")]
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private GameObject projectilePrefab;

    // Shotgun-specific
    [Header("Shotgun stats")]
    [SerializeField] private int pelletCount;
    [SerializeField] private float spreadAngle;

    // Bounce
    [Header("Ricochet bounce")]
    [SerializeField] private int maxBounces;

    // Pierce
    [Header("Piercing")]
    [SerializeField] private int maxPierces;

    // Propiedades públicas solo lectura para acceder a los valores
    public WeaponType WeaponType => weaponType;
    public float Speed => speed;
    public float FireRate => fireRate;
    public float Damage => damage;
    public GameObject ProjectilePrefab => projectilePrefab;
    public int PelletCount => pelletCount;
    public float SpreadAngle => spreadAngle;
    public int MaxBounces => maxBounces;
    public int MaxPierces => maxPierces;
}
