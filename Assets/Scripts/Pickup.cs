using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private float dropWeight = 1f;
    public float DropWeight => dropWeight;

    protected abstract void ApplyEffectTo(PlayerController player);

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            ApplyEffectTo(player);
            Destroy(gameObject); // Desaparece al ser recogido
        }
    }
}
