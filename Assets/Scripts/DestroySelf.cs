using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float _timeToDestroySelf;

    private void OnEnable()
    {
        Invoke("DSTRSLF", _timeToDestroySelf);
    }

    private void DSTRSLF()
    {
        Destroy(gameObject);
    }
}
