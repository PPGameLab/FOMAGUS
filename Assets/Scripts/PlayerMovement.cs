using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FreeMoveJoystick joystick;
    public float moveSpeed = 5f;

    private void Update()
    {
        float moveX = joystick.Horizontal();
        float moveY = joystick.Vertical();

        Vector3 move = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(move);
    }
}
