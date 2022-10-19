using System;
using UnityEngine;
using DragAndDrop;

[DisallowMultipleComponent]
public class Slot : Droppable
{
    [SerializeField] private ItemHolder _itemHolder;

    public bool HasItem => _itemHolder.HasItem();
    public ItemHolder ItemHolder => _itemHolder;

    public bool CanAccept(Draggable draggable)
    {
        return draggable is Item;
    }

    public void Clear(Action completed)
    {
        if (ItemHolder.TryGetItem(out Item item))
            item.Destroy(completed);
        else
            completed?.Invoke();
    }
}
