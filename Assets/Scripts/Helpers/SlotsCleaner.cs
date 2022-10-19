using System;
using System.Linq;

public static class SlotsCleaner
{
    public enum CleanMethod
    {
        Parallel,
        Sequentially
    }

    public static void Clear(Slot[] slots, Action completed, CleanMethod cleanMethod = CleanMethod.Parallel)
    {
        int occupiedSlotsCount = slots.Count(slot => slot.HasItem);

        if (occupiedSlotsCount == 0)
        {
            completed?.Invoke();

            return;
        }

        switch (cleanMethod)
        {
            case CleanMethod.Parallel:
                ClearParallel(slots, completed);
                break;
            case CleanMethod.Sequentially:
                ClearSequentially(slots, completed);
                break;
        }
    }

    public static void ClearParallel(Slot[] slots, Action completed)
    {
        Slot[] occupiedSlots = slots.Where(slot => slot.HasItem).ToArray();
        int occupiedSlotsCount = occupiedSlots.Length;

        void OnSlotCleared()
        {
            occupiedSlotsCount--;

            if (occupiedSlotsCount == 0)
                completed?.Invoke();
        }

        foreach (BoardSlot slot in occupiedSlots)
            slot.Clear(OnSlotCleared);
    }

    public static void ClearSequentially(Slot[] slots, Action completed)
    {
        Slot[] occupiedSlots = slots.Where(slot => slot.HasItem).ToArray();

        void ClearSlot(int index)
        {
            if (index == occupiedSlots.Length - 1)
                occupiedSlots[index].Clear(completed);
            else
                occupiedSlots[index].Clear(() => ClearSlot(index + 1));
        }

        ClearSlot(0);
    }
}
