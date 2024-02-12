using UnityEngine;

public class Deactivator : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Deactivate", 0.5f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
