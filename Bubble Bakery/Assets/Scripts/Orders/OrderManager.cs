using UnityEngine;

namespace Orders
{
    public class OrderManager : MonoBehaviour
    {
        [SerializeField] private Order order;
        [SerializeField] private int amount;

        private int _receivedAmount = 0;
        
        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Goody")) return;

            Draggable goody = other.GetComponent<Draggable>();
        
            if (!goody.wasDropped) return;
        
            // TODO: check if right goody was given
            if (goody.order != order)
            {
                // TODO: maybe play grumpy sound
                return;
            }
            
            // TODO: maybe play happy sound
            _receivedAmount++;

            if (_receivedAmount >= amount)
            {
                Debug.Log("enough");
                // TODO: inform timeline parser
                Destroy(goody.gameObject);
            }
        }
    }
}