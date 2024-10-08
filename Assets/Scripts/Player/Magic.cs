using UnityEngine;

public class Magic : MonoBehaviour
{
    public FireballSpell fireballSpellPrefab;
    public float spellCooldown = 5f;  // Кулдаун заклинания
    private float nextCastTime = 0f;

    void Update()
    {
        if (Time.time >= nextCastTime)
        {
            CastSpell();
            nextCastTime = Time.time + spellCooldown;
        }
    }

    // Этот метод не принимает аргументов
    public void CastSpell()
    {
        Instantiate(fireballSpellPrefab, transform.position, Quaternion.identity);
        Debug.Log("Fireball spell cast!");
    }
}
