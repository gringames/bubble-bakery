﻿using UnityEngine;

namespace Orders
{
    public class Draggable : MonoBehaviour
    {
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

        private void OnMouseUp()
        {
            ResetTransform();
        }

        public void OnMouseDrag()
        {
            transform.position
                = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, DragSmoothingSpeed);
        }

        private void ResetTransform()
        {
            transform.position = _originalTransform.position;
            transform.rotation = _originalTransform.rotation;
            transform.localScale = _originalTransform.localScale;
        }

        private Vector3 GetMousePos()
        {
            var mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            return mousePos;
        }
    }
}