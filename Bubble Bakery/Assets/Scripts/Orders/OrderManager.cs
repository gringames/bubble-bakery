﻿using Timeline;
using UnityEngine;

namespace Orders
{
    public class OrderManager : MonoBehaviour
    {
        [SerializeField] private AudioClip happySound;
        [SerializeField] private AudioClip grumpySound;
        [SerializeField] private OrderAction orderAction;

        private Order _order;
        private int _amount;

        private int _receivedAmount = 0;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        public void InitializeOrder(Order order, int amount)
        {
            _order = order;
            _amount = amount;
            
            Debug.Log($"order is {amount}x {order}");

            _boxCollider2D.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"{name} on trigger enter {other.name}");
            if (!other.CompareTag("Goody")) return;

            Draggable goody = other.GetComponent<Draggable>();
            CheckGoody(goody);
        }
        

        private void CheckGoody(Draggable goody)
        {
            if (goody.order != _order)
            {
                // TODO: play grumpy sound
                Debug.Log("wrong item!");
                return;
            }

            // TODO: play happy sound
            _receivedAmount++;
            orderAction.UpdateText(_receivedAmount);

            if (_receivedAmount < _amount) return;

            Destroy(goody.gameObject);
            Debug.Log("enough");
            _boxCollider2D.enabled = false;
            orderAction.Done();
        }
    }
}