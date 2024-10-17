using UnityEngine;

public class IncreaseSpeedBonus : BonusBase
{
    private PlayerController playerController;

    public IncreaseSpeedBonus(PlayerController controller) 
        : base("Повысить скорость персонажа")
    {
        playerController = controller;
    }

    public override void Apply()
    {
        playerController.speed += 1f;  // Увеличиваем скорость
        Debug.Log("Скорость персонажа увеличена");
    }
}
