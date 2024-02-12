using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject tutorialPanel;

    [Header("Arrays")]
    [SerializeField] private GameObject[] levelPages;
    [SerializeField] private GameObject[] tutorialPages;

    [Header("LevelButtons")]
    [SerializeField] private Button[] _btns;

    private int currentLevelIndex = 0;
    private int currentTutorialPageIndex = 0;

    private void Start()
    {
        SetupLevelButtons();
    }

    public void OpenLevelPanel()
    {
        SoundFX.Instance.PlayTap();
        startPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void OpenTutorialPanel()
    {
        SoundFX.Instance.PlayTap();
        tutorialPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void CloseTutorialPanel()
    {
        SoundFX.Instance.PlayTap();
        tutorialPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void CloseLevelPanel()
    {
        SoundFX.Instance.PlayTap();
        levelPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void TutorialNextPage()
    {
        SoundFX.Instance.PlayTap();
        if (currentTutorialPageIndex < tutorialPages.Length - 1)
        {
            tutorialPages[currentTutorialPageIndex].SetActive(false);
            currentTutorialPageIndex++;
            tutorialPages[currentTutorialPageIndex].SetActive(true);
        }
    }

    public void TutorialPreviousPage()
    {
        SoundFX.Instance.PlayTap();
        if (currentTutorialPageIndex > 0)
        {
            tutorialPages[currentTutorialPageIndex].SetActive(false);
            currentTutorialPageIndex--;
            tutorialPages[currentTutorialPageIndex].SetActive(true);
        }
    }

    public void LevelsNextPage()
    {
        SoundFX.Instance.PlayTap();
        if (currentLevelIndex < levelPages.Length - 1)
        {
            levelPages[currentLevelIndex].SetActive(false);
            currentLevelIndex++;
            levelPages[currentLevelIndex].SetActive(true);
        }
    }

    public void LevelsPreviousPage()
    {
        SoundFX.Instance.PlayTap();
        if (currentLevelIndex > 0)
        {
            levelPages[currentLevelIndex].SetActive(false);
            currentLevelIndex--;
            levelPages[currentLevelIndex].SetActive(true);
        }
    }

    public void PlaySelectedLevel(int levelIndex)
    {
        SoundFX.Instance.PlayTap();
        SceneManager.LoadScene(levelIndex);
    }

    private void SetupLevelButtons()
    {
        for (int i = 0; i < _btns.Length; i++)
        {
            int id = i + 1;
            SetButtonText(_btns[i], id);
        }
    }

    private void SetButtonText(Button btn, int id)
    {
        TextMeshProUGUI btnsText = btn.GetComponentInChildren<TextMeshProUGUI>();

        if (btnsText != null)
        {
            btnsText.text = id.ToString();
        }
    }
}
