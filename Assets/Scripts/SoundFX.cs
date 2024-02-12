using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public static SoundFX Instance { get; private set; }

    public AudioClip achive;
    public AudioClip[] exp;
    public AudioClip energy;
    public AudioClip fail;
    public AudioClip heal;
    public AudioClip moneyDrop;
    public AudioClip tap;
    public AudioClip destroy;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayTap()
    {
        AudioSource.PlayClipAtPoint(tap, Camera.main.transform.position);
    }

    public void PlayHitSound()
    {
        AudioSource.PlayClipAtPoint(exp[Random.Range(0, exp.Length)], Camera.main.transform.position, 0.3f);
    }

    public void PlayMoneyDropSound()
    {
        AudioSource.PlayClipAtPoint(moneyDrop, Camera.main.transform.position, 0.3f);
    }

    public void PlayAchiveSound()
    {
        AudioSource.PlayClipAtPoint(achive, Camera.main.transform.position);
    }

    public void PlayEnergySound()
    {
        AudioSource.PlayClipAtPoint(energy, Camera.main.transform.position, 0.5f);
    }

    public void PlayFailSound()
    {
        AudioSource.PlayClipAtPoint(fail, Camera.main.transform.position, 0.3f);
    }

    public void PlayHealSound()
    {
        AudioSource.PlayClipAtPoint(heal, Camera.main.transform.position, 0.3f);
    }
}
