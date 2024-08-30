using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private Sprite[] _skins;
    [SerializeField] private Image _imageSkin;
    [SerializeField] private Sprite[] _imagesSkin;


    public int _currentSprite = 3;
    public int CurrentSprite
    {
        get => _currentSprite;
        private set
        {
            _currentSprite = value;

            if (_currentSprite == _skins.Length) _currentSprite = 0;
            else if (_currentSprite < 0) _currentSprite = _skins.Length - 1;
        }
    }

    public void Next()
    {
        CurrentSprite++;
        Apply();

        Audio.Play(ClipType.changeSkin);
    }

    public void Preview()
    {
        CurrentSprite--;
        Apply();

        Audio.Play(ClipType.changeSkin);
    }

    private void Apply()
    {
        _skin.sprite = _skins[CurrentSprite];
        _imageSkin.sprite = _imagesSkin[CurrentSprite];
    }
}