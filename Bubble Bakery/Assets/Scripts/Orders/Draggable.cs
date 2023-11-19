using System;
using UnityEngine;

namespace Orders
{
    public class Draggable : MonoBehaviour
    {
        [HideInInspector] public bool wasDropped = false;
        public Order order;
        
        private Camera _mainCam;
        private Vector3 _dragOffset;
        private const float DragSmoothingSpeed = 1000f;
        private Transform _originalTransform;

        private void Awake()
        {
            _mainCam = Camera.main;
            _originalTransform = transform;
        }

        private void OnMouseDown()
        {
            wasDropped = false;
            _dragOffset = transform.position - GetMousePos();
        }

        private void OnMouseUp()
        {
            wasDropped = true;
            Invoke(nameof(WaitForCollisionElseReset), 1);
            
        }

        private void WaitForCollisionElseReset()
        {
            transform.position = _originalTransform.position;
            transform.rotation = _originalTransform.rotation;
            transform.localScale = _originalTransform.localScale;
        }

        private void OnMouseDrag()
        {
            transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, DragSmoothingSpeed);
        }

        private Vector3 GetMousePos()
        {
            var mousePos = _mainCam.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            mousePos.z = 0;
            return mousePos;
        }
    }
}