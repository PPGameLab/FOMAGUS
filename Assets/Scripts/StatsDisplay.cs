using UnityEngine;
using TMPro;

public class StatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI STR_Text;
    public TextMeshProUGUI AGI_Text;
    public TextMeshProUGUI INT_Text;
    public TextMeshProUGUI VIT_Text;
    public TextMeshProUGUI COINS_Text;
    private void Start()
    {
        UpdateStatsDisplay();
    }

    private void Update()
    {
        UpdateStatsDisplay();
    }

    private void UpdateStatsDisplay()
    {
        var data = GameManager.Instance.playerData;

        if (STR_Text != null)
        {
            STR_Text.text = $"STR: {data.STR}";
        }

        if (AGI_Text != null)
        {
            AGI_Text.text = $"AGI: {data.AGI}";
        }

        if (INT_Text != null)
        {
            INT_Text.text = $"INT: {data.INT}";
        }

        if (VIT_Text != null)
        {
            VIT_Text.text = $"VIT: {data.VIT}";
        }

        if (COINS_Text != null)
        {
            COINS_Text.text = $"COINS: {data.COINS}";
        }
    }
}