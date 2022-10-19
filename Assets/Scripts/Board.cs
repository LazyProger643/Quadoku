using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> _gameTurnDone;

    private BoardSlot[] _slots;

    public BoardSlot[] Slots => _slots;

    private void Awake()
    {
        _slots = GetComponentsInChildren<BoardSlot>();
    }

    public void OnItemDropped(Slot slot)
    {
        if (slot is BoardSlot boardSlot)
        {
            if (boardSlot.Group > 0)
            {
                BoardSlotsGroup slotsGroup = GetSlotsGroup(boardSlot.Group);

                if (slotsGroup.IsFilled())
                {
                    slotsGroup.Clear(() => _gameTurnDone.Invoke(true));

                    return;
                }
            }

            _gameTurnDone.Invoke(false);
        }
    }

    public void Clear(Action completed)
    {
        SlotsCleaner.Clear(_slots, completed);
    }

    private BoardSlotsGroup GetSlotsGroup(int group)
    {
        return new BoardSlotsGroup(_slots.Where(slot => slot.Group == group).ToArray());
    }
}
