using System;
using System.Linq;

public class BoardSlotsGroup
{
    private BoardSlot[] _slots;

    public BoardSlotsGroup(BoardSlot[] slots)
    {
        _slots = slots;
    }

    public bool IsFilled()
    {
        int filledSlotsCount = _slots.Count(slot => slot.HasItem);

        return _slots.Length == filledSlotsCount;
    }

    public void Clear(Action completed)
    {
        SlotsCleaner.Clear(_slots, completed, SlotsCleaner.CleanMethod.Sequentially);
    }
}
