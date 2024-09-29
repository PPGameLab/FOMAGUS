using UnityEngine;
using TMPro;

public class StatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI STR_Text;
    public TextMeshProUGUI AGI_Text;
    public TextMeshProUGUI INT_Text;
    public TextMeshProUGUI WIS_Text;
    public TextMeshProUGUI VIT_Text;
    public TextMeshProUGUI DUR_Text;
    public TextMeshProUGUI LUCK_Text;
    public TextMeshProUGUI GREED_Text;
    public TextMeshProUGUI COINS_Text;
    
    private void Start()
    {
        UpdateStatsDisplay();
    }

    private void Update()
    {
        UpdateStatsDisplay();
    }

//        Strength        
//        Agility         
//        Intelligence    
//        Wisdom          
//        Vitality        
//        Durability       
//        Luck            
//        Greed           
//        Coins           

    private void UpdateStatsDisplay()
    {
        var data = GameManager.Instance.playerData;

        if (STR_Text != null)
        {
            STR_Text.text = $"STR: {data.Strength}";
        }

        if (AGI_Text != null)
        {
            AGI_Text.text = $"AGI: {data.Agility}";
        }

        if (INT_Text != null)
        {
            INT_Text.text = $"INT: {data.Intelligence}";
        }

        if (WIS_Text != null)
        {
            WIS_Text.text = $"WIS: {data.Wisdom}";
        }

        if (VIT_Text != null)
        {
            VIT_Text.text = $"VIT: {data.Vitality}";
        }

        if (DUR_Text != null)
        {
            DUR_Text.text = $"DUR: {data.Durability}";
        }

        if (LUCK_Text != null)
        {
            LUCK_Text.text = $"LUCK: {data.Luck}";
        }

        if (GREED_Text != null)
        {
            GREED_Text.text = $"GREED: {data.Greed}";
        }

        if (COINS_Text != null)
        {
            COINS_Text.text = $"COINS: {data.Coins}";
        }
    }
}