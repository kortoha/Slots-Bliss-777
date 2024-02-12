using System.Collections;
using TMPro;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public static MiniGame Instance { get; private set; }

    [SerializeField] private float _speed = 5;
    [SerializeField] private Transform _planet;
    [SerializeField] private Animator _planetAnimator;
    [SerializeField] private int _winEnergyCount = 10;
    [SerializeField] private BoxCollider2D _spawner;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _winEnergy;
    [SerializeField] private GameObject _mainBG;
    [SerializeField] private GameObject _coinPanel;
    [SerializeField] private float _time = 10;

    [SerializeField] private GameObject[] _gameStufArray;

    [SerializeField] private GameObject[] _meteorsArray;

    private bool _isMove;
    private bool _isSoudnPlayed = false;

    private float _currentTime;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        Instance = this;

        _mainBG.SetActive(false);
        _coinPanel.SetActive(false);

        foreach (var item in _gameStufArray)
        {
            item.SetActive(false);
        }

        _coroutine = StartCoroutine(SpawnMeteorsRoutine());

        _currentTime = _time;
    }

    private void OnDisable()
    {
        _winEnergy.SetActive(false);
        _mainBG.SetActive(true);
        _coinPanel.SetActive(true);
        _isSoudnPlayed = false;
    }

    private void Update()
    {
        if(_currentTime > 0)
        {
            ScreenSwipe();
            MouseSwipe();
        }

        UpdateTime();
    }

    private void ScreenSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPosition = touch.deltaPosition;

                if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y))
                {
                    if (deltaPosition.x > 0)
                    {
                        MoveRight();
                        _isMove = true;

                    }
                    else
                    {
                        MoveLeft();
                        _isMove = true;
                    }
                }
            }
            else
            {
                _isMove = false;
            }
        }
    }

    private void MouseSwipe()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 deltaMousePosition = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (Mathf.Abs(deltaMousePosition.x) > Mathf.Abs(deltaMousePosition.y))
            {
                if (deltaMousePosition.x > 0)
                {
                    MoveRight();
                    _isMove = true;
                }
                else
                {
                    MoveLeft();
                    _isMove = true;
                }
            }
        }
        else
        {
            _isMove = false;
        }
    }

    private void MoveRight()
    {
        _planet.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void MoveLeft()
    {
        _planet.Translate(Vector2.left * _speed * Time.deltaTime);
    }

    private IEnumerator ActivateGame()
    {
        if (!_isSoudnPlayed)
        {
            SoundFX.Instance.PlayAchiveSound();
            _isSoudnPlayed = true;
        }
        StopCoroutine(_coroutine);

        _winEnergy.SetActive(true);

        Meteor[] meteors = GameObject.FindObjectsOfType<Meteor>();

        foreach (var meteor in meteors)
        {
            Destroy(meteor.gameObject);
        }

        yield return new WaitForSeconds(3);

        foreach (var item in _gameStufArray)
        {
            item.SetActive(true);
        }

        PlayersManager.Instance.WinEnergy(_winEnergyCount);
        PlayersManager.Instance.SetMiniGameEnd();
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnMeteorsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (_meteorsArray.Length > 0)
            {
                GameObject randomMeteor = _meteorsArray[Random.Range(0, _meteorsArray.Length)];

                float randomX = Random.Range(_spawner.bounds.min.x, _spawner.bounds.max.x);

                GameObject spawnedMeteor = Instantiate(randomMeteor, new Vector3(randomX, _spawner.transform.position.y), Quaternion.Euler(0, 0, 50));

                spawnedMeteor.transform.parent = transform;
            }
        }
    }

    public void PlanetDamaged()
    {
        _planetAnimator.SetTrigger("Damaged");
        _currentTime = _time;
    }


    private void UpdateTime()
    {
        _currentTime -= Time.deltaTime;

        int rndTime = Mathf.FloorToInt(_currentTime);

        if (rndTime > 0)
        {
            _timerText.text = rndTime.ToString();
        }
        else
        {
            _timerText.text = 0.ToString();

            StartCoroutine(ActivateGame());
        }
    }
}
