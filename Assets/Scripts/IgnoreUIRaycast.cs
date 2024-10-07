using UnityEngine;
using UnityEngine.EventSystems;

public class IgnoreUIRaycast : MonoBehaviour, ICanvasRaycastFilter
{
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        // Этот объект не должен блокировать UI касания
        return false;
    }
}
