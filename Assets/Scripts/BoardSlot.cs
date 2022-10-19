using UnityEngine;

public class BoardSlot : Slot
{
    [SerializeField, Min(0)] private int _group;

    public int Group => _group;
}
