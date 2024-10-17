using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int Strength         = 1;
    public int Agility          = 1;
    public int Intelligence     = 1;
    public int Wisdom           = 1;
    public int Vitality         = 1;
    public int Durability       = 1;
    public int Luck             = 1;
    public int Greed            = 1;
    public int Coins            = 1;

    public void HardReset()
    {
        Strength        = 1;
        Agility         = 1;
        Intelligence    = 1;
        Wisdom          = 1;
        Vitality        = 1;
        Durability      = 1;
        Luck            = 1;
        Greed           = 1;
        Coins           = 0;
    }


    public void SoftReset(string bonusAttribute)
    {
        Coins = 0;

        switch (bonusAttribute.ToLower())
        {
            case "Strength":
                Strength += 1;
                break;
            case "Agility":
                Agility += 1;
                break;
            case "Intelligence":
                Intelligence += 1;
                break;
            case "Wisdom":
                Wisdom += 1;
                break;
            case "Vitality":
                Vitality += 1;
                break;
            case "Durability":
                Durability += 1;
                break;
            case "Luck":
                Luck += 1;
                break;
            case "Greed":
                Greed += 1;
                break;           
            default:
                Debug.LogWarning("incorrect attribute");
                break;
        }
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        Debug.Log("Добавлено " + amount + " монет. Текущий баланс: " + Coins);
    }
}