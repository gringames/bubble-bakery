using System;
using UnityEngine;

namespace Input
{
    public class DragAndDrop : MonoBehaviour
    {
        private Vector3 _dragOffset;

        private void OnMouseDown()
        {
            _dragOffset = transform.position - GetMousePos();
        }

        private void OnMouseDrag()
        {
            transform.position = GetMousePos() + _dragOffset;
        }

        private Vector3 GetMousePos()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            mousePos.z = 0;
            return mousePos;
        }
    }
}