using UnityEngine;
using System.Collections;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private Transform _upPos;
    [SerializeField] private Transform _downPos;
    [SerializeField] private Transform _stopPos;
    [SerializeField] private float _speed = 5f;

    [SerializeField] private SpriteRenderer _firstSlotRenderer;
    [SerializeField] private SpriteRenderer _secondSlotRenderer;
    [SerializeField] private SpriteRenderer _thirdSlotRenderer;

    [SerializeField] private SpriteRenderer _firstFakeSlotRendererTop;
    [SerializeField] private SpriteRenderer _firstFakeSlotRendererBottom;

    [SerializeField] private SpriteRenderer _secondFakeSlotRendererTop;
    [SerializeField] private SpriteRenderer _secondFakeSlotRendererBottom;

    [SerializeField] private SpriteRenderer _thirdFakeSlotRendererTop;
    [SerializeField] private SpriteRenderer _thirdFakeSlotRendererBottom;

    [SerializeField] private SlotSO[] _slotSOArray;
    [SerializeField] private Planet _planet;

    [SerializeField] private AudioSource _stopSlotSound;

    private AudioSource _slotSound;
    private bool _isFirstSlotSpin = false;
    private bool _isSecondSlotSpin = false;
    private bool _isThirdSlotSpin = false;

    private SlotSO _firstSlotSO;
    private SlotSO _secondSlotSO;
    private SlotSO _thirdSlotSO;

    private SlotSO _firstFakeSlotSOTop;
    private SlotSO _firstFakeSlotSOBottom;

    private SlotSO _secondFakeSlotSOTop;
    private SlotSO _secondFakeSlotSOBottom;

    private SlotSO _thirdFakeSlotSOTop;
    private SlotSO _thirdFakeSlotSOBottom;

    private void Start()
    {
        _slotSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_firstSlotRenderer.gameObject.transform.position.y <= _downPos.position.y)
        {
            _firstSlotSO = _slotSOArray[UnityEngine.Random.Range(0, _slotSOArray.Length)];
            _firstSlotRenderer.sprite = _firstSlotSO.slotSprite;

            _firstFakeSlotSOTop = GetDifferentRandomSlotSO(_firstSlotSO);
            _firstFakeSlotRendererTop.sprite = _firstFakeSlotSOTop.slotSprite;

            _firstFakeSlotSOBottom = GetDifferentRandomSlotSO(_firstSlotSO);
            _firstFakeSlotRendererBottom.sprite = _firstFakeSlotSOBottom.slotSprite;

            _firstSlotRenderer.gameObject.transform.position = new Vector2(_firstSlotRenderer.gameObject.transform.position.x, _upPos.position.y);
        }
        else if (_secondSlotRenderer.gameObject.transform.position.y <= _downPos.position.y)
        {
            _secondSlotSO = _slotSOArray[UnityEngine.Random.Range(0, _slotSOArray.Length)];
            _secondSlotRenderer.sprite = _secondSlotSO.slotSprite;

            _secondFakeSlotSOTop = GetDifferentRandomSlotSO(_secondSlotSO);
            _secondFakeSlotRendererTop.sprite = _secondFakeSlotSOTop.slotSprite;

            _secondFakeSlotSOBottom = GetDifferentRandomSlotSO(_secondSlotSO);
            _secondFakeSlotRendererBottom.sprite = _secondFakeSlotSOBottom.slotSprite;

            _secondSlotRenderer.gameObject.transform.position = new Vector2(_secondSlotRenderer.gameObject.transform.position.x, _upPos.position.y);
        }
        else if (_thirdSlotRenderer.gameObject.transform.position.y <= _downPos.position.y)
        {
            _thirdSlotSO = _slotSOArray[UnityEngine.Random.Range(0, _slotSOArray.Length)];
            _thirdSlotRenderer.sprite = _thirdSlotSO.slotSprite;

            _thirdFakeSlotSOTop = GetDifferentRandomSlotSO(_thirdSlotSO);
            _thirdFakeSlotRendererTop.sprite = _thirdFakeSlotSOTop.slotSprite;

            _thirdFakeSlotSOBottom = GetDifferentRandomSlotSO(_thirdSlotSO);
            _thirdFakeSlotRendererBottom.sprite = _thirdFakeSlotSOBottom.slotSprite;

            _thirdSlotRenderer.gameObject.transform.position = new Vector2(_thirdSlotRenderer.gameObject.transform.position.x, _upPos.position.y);
        }
    }

    private SlotSO GetDifferentRandomSlotSO(SlotSO original)
    {
        SlotSO randomSlotSO;

        do
        {
            randomSlotSO = _slotSOArray[UnityEngine.Random.Range(0, _slotSOArray.Length)];
        } while (randomSlotSO == original);

        return randomSlotSO;
    }

    public void StartSpin()
    {
        if (_planet != null)
        {
            if (!_isFirstSlotSpin && PlayersManager.Instance.GetEnergyPoints() > 0)
            {
                _slotSound.Play();
                _firstSlotSO = null;
                _secondSlotSO = null;
                _thirdSlotSO = null;
                StartCoroutine(SpinFirstSlot());
                Invoke("StartSecondSpeenCoroutine", 0.2f);
                Invoke("StartThirdSpeenCoroutine", 0.4f);
            }
        }
    }

    public void StopSpin()
    {
        if (_isFirstSlotSpin)
        {
            _slotSound.Stop();
            _stopSlotSound.Play();
            PlayersManager.Instance.UseEnergy();
            _firstSlotRenderer.gameObject.transform.position = new Vector2(_firstSlotRenderer.gameObject.transform.position.x, _stopPos.position.y);
            _isFirstSlotSpin = false;

            Invoke("StopSecondSlotSpin", 0.2f);
            _secondSlotRenderer.gameObject.transform.position = new Vector2(_secondSlotRenderer.gameObject.transform.position.x, _stopPos.position.y);

            Invoke("StopThirdSlotSpin", 0.4f);
            _thirdSlotRenderer.gameObject.transform.position = new Vector2(_thirdSlotRenderer.gameObject.transform.position.x, _stopPos.position.y);

            Invoke("ExecuteFirstSlotAction", 0.5f);
            Invoke("ExecuteSecondSlotAction", 1f);
            Invoke("ExecuteThirdSlotAction", 1.5f);
        }
    }

    private IEnumerator SpinFirstSlot()
    {
        _isFirstSlotSpin = true;

        while (_isFirstSlotSpin)
        {
            _firstSlotRenderer.gameObject.transform.Translate(Vector2.down * _speed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator SpinSecondSlot()
    {
        _isSecondSlotSpin = true;

        while (_isFirstSlotSpin)
        {
            _secondSlotRenderer.gameObject.transform.Translate(Vector2.down * _speed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator SpinThirdSlot()
    {
        _isThirdSlotSpin = true;

        while (_isFirstSlotSpin)
        {
            _thirdSlotRenderer.gameObject.transform.Translate(Vector2.down * _speed * Time.deltaTime);

            yield return null;
        }
    }

    private void StopSecondSlotSpin()
    {
        _isSecondSlotSpin = false;
    }

    private void StopThirdSlotSpin()
    {
        _isThirdSlotSpin = false;
    }

    private void StartSecondSpeenCoroutine()
    {
        StartCoroutine(SpinSecondSlot());
    }

    private void StartThirdSpeenCoroutine()
    {
        StartCoroutine(SpinThirdSlot());
    }

    private void ExecuteFirstSlotAction()
    {
        ExecuteSlotAction(_firstSlotSO);
    }

    private void ExecuteSecondSlotAction()
    {
        ExecuteSlotAction(_secondSlotSO);
    }

    private void ExecuteThirdSlotAction()
    {
        ExecuteSlotAction(_thirdSlotSO);
    }

    private void ExecuteSlotAction(SlotSO slotSO)
    {
        if (slotSO != null)
        {
            switch (slotSO.tipeOfSlot)
            {
                case SlotSO.TipeOfSlot.Damage:
                    if (_planet != null)
                    {
                        _planet.GetDamage(slotSO.coutOfSomething);
                        SoundFX.Instance.PlayHitSound();
                        Instantiate(slotSO.FX, _planet.transform.position, Quaternion.identity);
                    }
                    break;
                case SlotSO.TipeOfSlot.Money:
                    PlayersManager.Instance.PickUpMoney(slotSO.coutOfSomething);
                    SoundFX.Instance.PlayMoneyDropSound();
                    Instantiate(slotSO.FX, Vector2.zero, Quaternion.identity);
                    break;
                case SlotSO.TipeOfSlot.Heal:
                    if (_planet != null)
                    {
                        SoundFX.Instance.PlayHealSound();
                        Instantiate(slotSO.FX, _planet.transform.position, Quaternion.identity);
                        _planet.Healing(slotSO.coutOfSomething);
                    }
                    break;
            }
        }
    }
}
