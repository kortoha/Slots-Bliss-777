using UnityEngine;

[CreateAssetMenu()]
public class SlotSO : ScriptableObject
{
    public enum TipeOfSlot
    {
        Damage,
        Money,
        Heal
    }

    public TipeOfSlot tipeOfSlot;
    public Sprite slotSprite;
    public int coutOfSomething;
    public GameObject FX;
}
