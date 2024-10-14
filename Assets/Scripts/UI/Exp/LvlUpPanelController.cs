using UnityEngine;
using UnityEngine.UI;

public class LvlUpPanelController : MonoBehaviour
{
    public GameObject lvlUpPanel;  // Ссылка на панель уровня
    public Button fireballUpgradeButton;  // Кнопка для улучшения Fireball
    public Button fireball2UpgradeButton;  // Кнопка для улучшения Fireball2
    public Button cooldownReductionButton;  // Кнопка для уменьшения кулдауна

    private PlayerSpells playerSpells;  // Ссылка на скрипт заклинаний игрока

    void Start()
    {
        lvlUpPanel.SetActive(false);  // Панель скрыта при старте игры
        playerSpells = FindObjectOfType<PlayerSpells>();  // Получаем ссылку на заклинания игрока

        // Подписываем кнопки на соответствующие методы
        fireballUpgradeButton.onClick.AddListener(UpgradeFireball);
        fireball2UpgradeButton.onClick.AddListener(UpgradeFireball2);
        cooldownReductionButton.onClick.AddListener(ReduceCooldown);
    }

    // Метод для показа панели
    public void ShowPanel()
    {
        Time.timeScale = 0;  // Останавливаем игру
        lvlUpPanel.SetActive(true);  // Показываем панель
    }

    // Метод для скрытия панели и продолжения игры
    public void HidePanel()
    {
        lvlUpPanel.SetActive(false);  // Скрываем панель
        Time.timeScale = 1;  // Возобновляем игру
    }

    // Улучшить Fireball
    void UpgradeFireball()
    {
        SpellData fireball = playerSpells.GetSpellData("Fireball");
        if (fireball.level < fireball.maxLevel)
        {
            fireball.level++;
            Debug.Log("Fireball улучшен до уровня " + fireball.level);
        }
        HidePanel();
    }

    // Улучшить Fireball2
    void UpgradeFireball2()
    {
        SpellData fireball2 = playerSpells.GetSpellData("Fireball2");
        if (fireball2.level < fireball2.maxLevel)
        {
            fireball2.level++;
            Debug.Log("Fireball2 улучшен до уровня " + fireball2.level);
        }
        HidePanel();
    }

    // Уменьшить кулдаун всех заклинаний
    void ReduceCooldown()
    {
        foreach (SpellData spell in playerSpells.playerSpells)
        {
            spell.cooldown = Mathf.Max(spell.cooldown - 0.5f, 1f);  // Уменьшаем кулдаун, но не меньше 1 секунды
        }
        Debug.Log("Кулдауны всех заклинаний уменьшены");
        HidePanel();
    }
}
