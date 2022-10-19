using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragAndDrop
{
    public abstract class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, ICanvasRaycastFilter
    {
        private bool _dragging;

        public static event Action<PointerEventData, Draggable> BeginDrag;
        public static event Action<PointerEventData, Draggable> Drag;
        public static event Action<PointerEventData, Draggable> EndDrag;

        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            return _dragging == false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            bool cancel = false;

            OnBeginDrag(ref cancel);

            if (cancel)
            {
                eventData.pointerDrag = null;
            }
            else
            {
                BeginDrag?.Invoke(eventData, this);
                _dragging = true;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDrag();
            Drag?.Invoke(eventData, this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndDrag();
            EndDrag?.Invoke(eventData, this);
            _dragging = false;
        }

        protected virtual void OnBeginDrag(ref bool cancel) { }

        protected virtual void OnDrag() { }

        protected virtual void OnEndDrag() { }
    }
}