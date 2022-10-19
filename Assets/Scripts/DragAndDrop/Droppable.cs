using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragAndDrop
{
    public abstract class Droppable : MonoBehaviour, IDropHandler
    {
        public static event Action<PointerEventData, Draggable, Droppable> Drop;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out Draggable draggable))
            {
                OnDrop(draggable);

                Drop?.Invoke(eventData, draggable, this);
            }
        }

        protected virtual void OnDrop(Draggable draggable) { }
    }
}