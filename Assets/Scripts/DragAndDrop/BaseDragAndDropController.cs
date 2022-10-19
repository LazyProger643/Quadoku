using UnityEngine;
using UnityEngine.EventSystems;

namespace DragAndDrop
{
    public abstract class BaseDragAndDropController<TDraggable, TDroppable> : MonoBehaviour
        where TDraggable : Draggable
        where TDroppable : Droppable
    {
        private TDraggable _draggable;
        private bool _dropped;

        private void OnEnable()
        {
            Draggable.BeginDrag += OnBeginDrag;
            Draggable.Drag += OnDrag;
            Draggable.EndDrag += OnEndDrag;
            Droppable.Drop += OnDrop;
        }

        private void OnDisable()
        {
            Draggable.BeginDrag -= OnBeginDrag;
            Draggable.Drag -= OnDrag;
            Draggable.EndDrag -= OnEndDrag;
            Droppable.Drop -= OnDrop;
        }

        private void OnBeginDrag(PointerEventData eventData, Draggable draggable)
        {
            bool cancel = false;

            OnBeginDrag(draggable as TDraggable, ref cancel);

            if (cancel)
            {
                eventData.pointerDrag = null;

                return;
            }

            _dropped = false;
            _draggable = draggable as TDraggable;
        }

        private void OnDrag(PointerEventData eventData, Draggable draggable)
        {
            OnDrag(draggable as TDraggable);

            _draggable.transform.position = Input.mousePosition;
        }

        private void OnDrop(PointerEventData eventData, Draggable draggable, Droppable droppable)
        {
            _dropped = true;

            OnDrop(draggable as TDraggable, droppable as TDroppable);
        }

        private void OnEndDrag(PointerEventData eventData, Draggable draggable)
        {
            OnEndDrag(draggable as TDraggable, _dropped);

            _draggable = null;
        }

        protected virtual void OnBeginDrag(TDraggable draggable, ref bool cancel) { }
        protected virtual void OnDrag(TDraggable draggable) { }
        protected virtual void OnDrop(TDraggable draggable, TDroppable droppable) { }
        protected virtual void OnEndDrag(TDraggable draggable, bool dropped) { }
    }
}
