using UnityEngine;
public abstract class BonusBase
{
    public string Description { get; private set; }

    public BonusBase(string description)
    {
        Description = description;
    }

    public abstract void Apply();
}
