using UnityEngine;
using UnityEngine.Events;
using DragAndDrop;

[DisallowMultipleComponent]
public sealed class DragAndDropController : BaseDragAndDropController<Item, Slot>
{
    [SerializeField] private Transform _draggingContainer;
    [Space, Header("Events")]
    [SerializeField] private UnityEvent<Slot> _itemDropped;

    private Transform _lastParent;
    private Vector3 _lastPosition;

    protected override void OnBeginDrag(Item item, ref bool cancel)
    {
        if (item.Locked)
        {
            cancel = true;

            return;
        }

        _lastParent = item.transform.parent;
        _lastPosition = item.transform.position;

        item.transform.SetParent(_draggingContainer);
    }

    protected override void OnDrop(Item item, Slot slot)
    {
        if (slot.CanAccept(item) && slot.HasItem == false)
        {
            item.transform.SetParent(slot.ItemHolder.transform);
            item.transform.localPosition = Vector3.zero;
            item.UpdateInSlot(slot, () => _itemDropped.Invoke(slot));
        }
    }

    protected override void OnEndDrag(Item item, bool dropped)
    {
        if (item.transform.parent == _draggingContainer)
        {
            item.transform.SetParent(_lastParent);
            item.transform.position = _lastPosition;
        }
    }
}
