using DragAndDrop;
using System;
using UnityEngine;

[RequireComponent(typeof(ItemSizeChanger))]
public class Item : Draggable
{
    private ItemSizeChanger _sizeChanger;
    private bool _isVanishing;

    public bool Locked { get; private set; }

    public void Awake()
    {
        _sizeChanger = GetComponent<ItemSizeChanger>();

        GetComponent<RectTransform>().sizeDelta = Vector2.zero;
    }

    public void UpdateInSlot(Slot slot, Action completed = null)
    {
        UpdateState(slot);
        UpdateView(slot, completed);
    }

    public void Destroy(Action completed = null)
    {
        if (_isVanishing == false)
        {
            _isVanishing = true;

            _sizeChanger.Change(0, () =>
            {
                gameObject.SetActive(false);
                transform.SetParent(null);

                Destroy(gameObject);

                completed?.Invoke();
            });
        }
    }

    private void UpdateState(Slot slot)
    {
        Locked = slot is BoardSlot;
    }

    private void UpdateView(Slot slot, Action completed = null)
    {
        if (slot is BoardSlot)
            _sizeChanger.Change(GameParameters.ItemSizeInLockedSlot, completed);
        else
            _sizeChanger.Change(GameParameters.ItemDefaultSize, completed);
    }
}
