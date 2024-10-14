using UnityEngine;

public class Magic : MonoBehaviour
{
    public PlayerSpells playerSpells;  // Ссылка на скрипт с заклинаниями игрока

    // Префабы заклинаний
    public GameObject fireballPrefab;
    public GameObject fireball2Prefab;

    private float nextFireballTime = 0f;
    private float nextFireball2Time = 0f;

    void Update()
    {
        CastSpells();
    }

    void CastSpells()
    {
        SpellData fireball = playerSpells.GetSpellData("Fireball");
        SpellData fireball2 = playerSpells.GetSpellData("Fireball2");

        if (fireball.level > 0 && Time.time >= nextFireballTime)
        {
            // Кастуем Fireball
            CastSpell(fireball, fireballPrefab);
            nextFireballTime = Time.time + fireball.GetCooldown();
        }

        if (fireball2.level > 0 && Time.time >= nextFireball2Time)
        {
            // Кастуем Fireball2
            CastSpell(fireball2, fireball2Prefab);
            nextFireball2Time = Time.time + fireball2.GetCooldown();
        }
    }

    // Метод кастования заклинания с префабом
    void CastSpell(SpellData spell, GameObject spellPrefab)
    {
        Debug.Log($"Casting {spell.spellName} with damage: {spell.GetDamage()}");
        
        // Создаем префаб заклинания на позиции игрока
        Instantiate(spellPrefab, transform.position, Quaternion.identity);
    }
}
