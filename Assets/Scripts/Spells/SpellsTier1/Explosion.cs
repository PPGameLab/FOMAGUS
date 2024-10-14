using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Уничтожаем взрыв через 1 секунду
    }
}
