using System;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    private void Awake()
    {
        Item[] items = GetComponentsInChildren<Item>();

        if (items.Length > 1)
            throw new InvalidOperationException($"The item holder (id {GetInstanceID()}) contains more than one item.");
    }

    public bool TryGetItem(out Item item)
    {
        item = null;

        Item[] items = GetComponentsInChildren<Item>();

        if (items.Length != 1)
            return false;

        item = items[0];

        return true;
    }

    public bool HasItem()
    {
        return TryGetItem(out _);
    }
}
