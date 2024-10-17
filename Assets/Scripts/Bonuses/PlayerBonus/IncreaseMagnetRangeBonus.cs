
using UnityEngine;
public class IncreaseMagnetRangeBonus : BonusBase
{
    private MagnetEffect magnetEffect;

    public IncreaseMagnetRangeBonus(MagnetEffect effect) : base("Повысить радиус магнита")
    {
        magnetEffect = effect;
    }

    public override void Apply()
    {
        magnetEffect.IncreaseMagnetRange(2f); // Увеличиваем радиус магнита
        Debug.Log("Magnet range increased.");
    }
}
