using UnityEngine;

public class Meteor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Planet"))
        {
            SoundFX.Instance.PlayFailSound();
            MiniGame.Instance.PlanetDamaged();
        }
    }
}
