using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayersManager : MonoBehaviour
{
    public static PlayersManager Instance { get; private set; }

    [SerializeField] private int _energyPoints;
    [SerializeField] private TextMeshProUGUI _energyPointsText;
    [SerializeField] private TextMeshProUGUI _moneyScoreText;
    [SerializeField] private Image _energyBar;
    [SerializeField] private int _energyPointPrice = 100;
    [SerializeField] private Animator _coinScoreAnimator;
    [SerializeField] private TextMeshProUGUI _energyPointPriceTextText;

    [SerializeField] private GameObject _miniGame;

    [SerializeField] private int _moneyCount = 0;

    private int _maxEnergyPoints;

    private bool _isMiniGameActive = false;

    private void Awake()
    {
        Instance = this;

        _maxEnergyPoints = _energyPoints;
    }

    private void Start()
    {
        _energyPointPriceTextText.text = _energyPointPrice.ToString();
    }

    private void Update()
    {
        UpdateEnergyBar(_energyPoints, _maxEnergyPoints, _energyBar);
        UpdateMoneyScore();

        if (_energyPoints == 0)
        {
            if (!_isMiniGameActive)
            {
                StartCoroutine(StartMiniGame());
            }
        }
    }

    private void UpdateEnergyBar(int currentEnergyPoints, int maxEnergyPoints, Image barImage)
    {
        _energyPointsText.text = currentEnergyPoints.ToString() + " / " + maxEnergyPoints.ToString();

        float healthLavel = (float)currentEnergyPoints / maxEnergyPoints;

        barImage.fillAmount = healthLavel;

        if (_energyPoints < 0)
        {
            _energyPoints = 0;
        }
    }

    private void UpdateMoneyScore()
    {
        _moneyScoreText.text = _moneyCount.ToString();
    }

    public int GetEnergyPoints()
    {
        return _energyPoints;
    }

    public void UseEnergy()
    {
        _energyPoints--;
    }

    public void WinEnergy(int count)
    {
        _energyPoints += count;
    }

    public void Buy(int price)
    {
        if (_moneyCount >= price)
        {
            _moneyCount -= price;
        }
        else if (_moneyCount < price)
        {
            _coinScoreAnimator.SetTrigger("NoMoney");
        }
    }

    public void PickUpMoney(int score)
    {
        _moneyCount += score;
    }

    public int GetMoneyScore()
    {
        return _moneyCount;
    }

    public void BuyEnergy()
    {
        if (_energyPoints != _maxEnergyPoints)
        {
            if (_moneyCount >= _energyPointPrice)
            {
                SoundFX.Instance.PlayEnergySound();
                Buy(_energyPointPrice);
                _energyPoints++;
            }
            else
            {
                SoundFX.Instance.PlayFailSound();
                _coinScoreAnimator.SetTrigger("NoMoney");
            }
        }
        else
        {
            SoundFX.Instance.PlayFailSound();
            _coinScoreAnimator.SetTrigger("NoMoney");
        }

    }

    private IEnumerator StartMiniGame()
    {
        yield return new WaitForSeconds(3f);
        _miniGame.SetActive(true);
        _isMiniGameActive = true;
    }

    public void SetMiniGameEnd()
    {
        _isMiniGameActive = false;
    }

    public bool IsMiniGameActive()
    {
        return _isMiniGameActive;
    }
}
