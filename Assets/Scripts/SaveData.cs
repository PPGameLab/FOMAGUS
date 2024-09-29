using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int STR = 1;
    public int AGI = 1;
    public int INT = 1;
    public int VIT = 1;
    public int COINS = 0;

    public void HardReset()
    {
        COINS = 0;
        STR = 1;
        AGI = 1;
        INT = 1;
        VIT = 1;
    }


    public void SoftReset(string bonusAttribute)
    {
        COINS = 0;

        switch (bonusAttribute.ToLower())
        {
            case "str":
                STR += 1;
                break;
            case "agi":
                AGI += 1;
                break;
            case "int":
                INT += 1;
                break;
            case "vit":
                VIT += 1;
                break;
            default:
                Debug.LogWarning("incorrect attribute");
                break;
        }
    }
}