using UnityEngine;
using UnityEngine.SceneManagement;

public class GameVisualManager : MonoBehaviour
{
    public static GameVisualManager Instance { get; private set; }

    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _pause;

    [SerializeField] private GameObject _planet;

    [SerializeField] private GameObject[] _stafArray;
    [SerializeField] private GameObject[] _winStafArray;

    [SerializeField] private int _scene;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if(_planet == null)
        {
            Invoke("Win", 1);
        }
    }

    public void Pause()
    {
        SoundFX.Instance.PlayTap();
        _game.SetActive(false);
        _pause.SetActive(true);
        Time.timeScale = 0;

        if (!PlayersManager.Instance.IsMiniGameActive())
        {
            foreach (var item in _stafArray)
            {
                item.SetActive(false);
            }
        }
    }

    public void UnPause()
    {
        SoundFX.Instance.PlayTap();
        _game.SetActive(true);
        _pause.SetActive(false);
        Time.timeScale = 1;

        if (!PlayersManager.Instance.IsMiniGameActive())
        {
            foreach (var item in _stafArray)
            {
                item.SetActive(true);
            }
        }
    }

    public void Restart()
    {
        SoundFX.Instance.PlayTap();
        Time.timeScale = 1;
        SceneManager.LoadScene(_scene);
    }

    public void Menu()
    {
        SoundFX.Instance.PlayTap();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void NXT()
    {
        SoundFX.Instance.PlayTap();
        _scene++;
        SceneManager.LoadScene(_scene);
    }

    public void NXTAfter10()
    {
        SoundFX.Instance.PlayTap();
        SceneManager.LoadScene(2);
    }

    public void Win()
    {
        _win.SetActive(true);
        _game.SetActive(false);

        foreach (var item in _winStafArray)
        {
            item.SetActive(false);
        }
    }
}
