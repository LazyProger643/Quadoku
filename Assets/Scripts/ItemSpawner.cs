using System;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _itemPrefab;

    public bool TrySpawn(Slot slot, out Item item)
    {
        item = null;

        if (slot.HasItem == false)
            item = Spawn(slot);

        return item != null;
    }

    public Item TrySpawn(Slot slot)
    {
        if (slot.HasItem == false)
            return Spawn(slot);

        return null;
    }

    public Item Spawn(Slot slot)
    {
        if (slot.HasItem)
            throw new InvalidOperationException($"The slot (id {slot.GetInstanceID()}) already contains an item.");

        Item item = Instantiate(_itemPrefab);

        item.transform.SetParent(slot.ItemHolder.transform);
        item.transform.localPosition = Vector3.zero;
        item.UpdateInSlot(slot);

        return item;
    }
}