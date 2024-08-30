using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private Sprite[] _skins;
    [SerializeField] private Image[] _points;
    [SerializeField] private Image _imageSkin;
    [SerializeField] private Sprite[] _imagesSkin;

    [SerializeField] private Sprite _equip, _unequip;

    private int _currentSprite;
    public int CurrentSprite
    {
        get => _currentSprite;
        private set
        {
            _points[_currentSprite].sprite = _unequip;
            _currentSprite = value;

            if (_currentSprite == _skins.Length) _currentSprite = 0;
            else if (_currentSprite < 0) _currentSprite = _skins.Length - 1;

            _points[_currentSprite].sprite = _equip;
        }
    }

    public void Next()
    {
        CurrentSprite++;
        Apply();
    }

    public void Preview()
    {
        CurrentSprite--;
        Apply();
    }

    private void Apply()
    {
        _skin.sprite = _skins[CurrentSprite];
        _imageSkin.sprite = _imagesSkin[CurrentSprite];
    }
}