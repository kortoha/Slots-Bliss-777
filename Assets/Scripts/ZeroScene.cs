using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZeroScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _loadingText;

    private int _loadingProgressCount;

    private void Start()
    {
        StartCoroutine(ZeroPointCoroutine());
    }

    private IEnumerator ZeroPointCoroutine()
    {
        int targetProgress = 100;
        int currentProgress = 0;

        while (currentProgress < targetProgress)
        {
            yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));

            currentProgress++;
            UpdateLoadingText(currentProgress);
        }

        SceneManager.LoadScene(1);
    }

    private void UpdateLoadingText(int progress)
    {
        _loadingText.text = "Loading " + progress + "%";
    }
}
