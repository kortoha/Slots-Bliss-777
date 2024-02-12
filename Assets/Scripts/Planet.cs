using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    [SerializeField] private int _healthPoints;
    [SerializeField] private TextMeshProUGUI _healthPointText;
    [SerializeField] private Image _healthBar;

    [SerializeField] private int _armorPoints;
    [SerializeField] private Image _armorBar;
    [SerializeField] private TextMeshProUGUI _armorPointText;

    [SerializeField] private GameObject _armorPointBar;
    [SerializeField] private GameObject _healthPointBar;

    [SerializeField] private GameObject _explFX;

    [SerializeField] private bool _isHasArmor = false;

    private int _maxHealthPoint;
    private int _maxArmorPoint;

    private void Awake()
    {
        _maxHealthPoint = _healthPoints;
        _maxArmorPoint = _armorPoints;
    }

    private void Update()
    {
        UpdateHealthBar(_healthPoints, _maxHealthPoint, _healthBar);
        
        if(_armorBar != null && _isHasArmor)
        {
            UpdateArmorBar(_armorPoints, _maxArmorPoint, _armorBar);
        }
    }

    public void GetDamage(int damage)
    {
        if(!_isHasArmor)
        {
            if(_healthPoints > 0)
            {
                _healthPoints -= damage;
            }
        }
        else
        {
            if(_armorPoints > 0)
            {
                _armorPoints -= damage;
            }
        }
    }

    public int GetHealth()
    {
        return _healthPoints;
    }

    public void Healing(int healPointCount)
    {
        if(_healthPoints < _maxHealthPoint)
        {
            _healthPoints += healPointCount;
        }

    }

    private void UpdateHealthBar(int currentHealth, int maxHealth, Image barImage)
    {
        _healthPointText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        float healthLavel = (float)currentHealth / maxHealth;

        barImage.fillAmount = healthLavel;

        if (healthLavel <= 0)
        {
            Instantiate(_explFX, transform.position, Quaternion.identity);
            _healthPointBar.SetActive(false);
            AudioSource.PlayClipAtPoint(SoundFX.Instance.destroy, Camera.main.transform.position);
            Destroy(gameObject);
        }

        if (_healthPoints < 0)
        {
            _healthPoints = 0;
        }

        if(_healthPoints >= _maxHealthPoint)
        {
            _healthPoints = _maxHealthPoint;
        }
    }

    private void UpdateArmorBar(int currentArmor, int maxArmor, Image barImage)
    {
        _armorPointText.text = currentArmor.ToString() + " / " + maxArmor.ToString();

        float healthLavel = (float)currentArmor / maxArmor;

        barImage.fillAmount = healthLavel;

        if (_armorPoints <= 0)
        {
            _armorPoints = 0;
            _isHasArmor = false;
            _armorPointBar.SetActive(false);
        }
    }
}
