using UnityEngine;
using UnityEngine.UI;

public class SNDManager : MonoBehaviour
{
    private const string KEY = "Key";

    public static SNDManager Instance { get; private set; }

    private bool _isOK = false;

    [SerializeField] private Image _soundImg;
    [SerializeField] private Sprite _sound;
    [SerializeField] private Sprite _mute;

    private void Awake()
    {
        Instance = this;

        _isOK = PlayerPrefs.GetInt(KEY, 0) == 1;

        AudioListener.pause = _isOK;

        UpdateImg();

    }

    private void UpdateImg()
    {
        if (_soundImg != null)
        {
            _soundImg.sprite = _isOK ? _mute : _sound;
        }
    }

    public void ToggleSND()
    {
        SoundFX.Instance.PlayTap();
        _isOK = !_isOK;
        AudioListener.pause = _isOK;

        UpdateImg();

        PlayerPrefs.SetInt(KEY, _isOK ? 1 : 0);
        PlayerPrefs.Save();
    }
}
