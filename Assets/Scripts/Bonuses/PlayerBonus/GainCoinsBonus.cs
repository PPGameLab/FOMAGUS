using UnityEngine;

public class GainCoinsBonus : BonusBase
{
    private SaveData saveData;
    private int coinsAmount;

    public GainCoinsBonus(SaveData data, int coins) : base("Получить " + coins + " монет")
    {
        saveData = data;
        coinsAmount = coins;
    }

    public override void Apply()
    {
        saveData.AddCoins(coinsAmount);
        SaveSystem.SaveGame(saveData); // Сохраняем изменения в данных
    }
}
