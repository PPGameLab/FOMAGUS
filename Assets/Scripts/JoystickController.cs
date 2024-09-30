using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FreeMoveJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Image joystickBackground;
    public Image joystickHandle;
    private Vector2 inputVector;
    private Vector2 joystickStartPosition; // Стартовая позиция джойстика
    
    private void Start()
    {
        // Оставляем начальное положение джойстика
        joystickStartPosition = joystickBackground.rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Проверяем, нажимает ли игрок на UI элемент (например, кнопки паузы или меню)
        if (EventSystem.current.IsPointerOverGameObject(eventData.pointerId))
        {
            return; // Если нажатие происходит на UI элементе, не активируем джойстик
        }

        // Перемещаем фон джойстика в позицию касания
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out position);
        joystickBackground.rectTransform.anchoredPosition = position;

        // Перемещаем рукоятку джойстика
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Проверяем, нажимает ли игрок на UI элемент (например, кнопки паузы или меню)
        if (EventSystem.current.IsPointerOverGameObject(eventData.pointerId))
        {
            return; // Если нажатие происходит на UI элементе, не активируем джойстик
        }

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground.rectTransform, eventData.position, eventData.pressEventCamera, out position);

        // Нормализуем вектор для ограничения движения джойстика
        position.x = position.x / (joystickBackground.rectTransform.sizeDelta.x / 2);
        position.y = position.y / (joystickBackground.rectTransform.sizeDelta.y / 2);

        inputVector = new Vector2(position.x, position.y);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        // Перемещаем рукоятку джойстика
        joystickHandle.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBackground.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBackground.rectTransform.sizeDelta.y / 2));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Возвращаем рукоятку джойстика в центр фона и сбрасываем положение
        inputVector = Vector2.zero;
        joystickHandle.rectTransform.anchoredPosition = Vector2.zero;
        joystickBackground.rectTransform.anchoredPosition = joystickStartPosition; // Возвращаем фон на исходную позицию
    }

    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }
}
