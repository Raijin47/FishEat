using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Sub-Classes
    [System.Serializable]
    public class UIButtonEvent : UnityEvent<PointerEventData.InputButton> { }
    #endregion

    [SerializeField] private RectTransform _rectTransform;

    private readonly Vector2 PressedSize = new(0.9f, 0.9f);

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        _rectTransform.localScale = PressedSize;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        _rectTransform.localScale = Vector2.one;
    }

    private void OnValidate()
    {
        _rectTransform ??= GetComponent<RectTransform>();
    }
}