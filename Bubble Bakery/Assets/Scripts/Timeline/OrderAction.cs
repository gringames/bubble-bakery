using System;
using Input;
using Orders;
using TMPro;
using UnityEngine;

namespace Timeline
{
    public class OrderAction : MonoBehaviour, IAction
    {
        [Header("Timeline")] [SerializeField] private TimelineParser timelineParser;
        [SerializeField] private InputHandler inputHandler;

        [Header("Order")] [SerializeField] private int minAmount;
        [SerializeField] private int maxAmount;

        [Header("Visuals")] [SerializeField] private GameObject thoughtBubble;
        [SerializeField] private TMP_Text amountText;
        [SerializeField] private GameObject cookieImage;
        [SerializeField] private GameObject muffinImage;
        [SerializeField] private GameObject donutImage;

        [Header("Characters")] [SerializeField]
        private OrderManager cthullu;

        [SerializeField] private OrderManager coral;
        [SerializeField] private OrderManager don;
        [SerializeField] private OrderManager jean;

        private const string AmountX = "x";
        private Unity.Mathematics.Random _random;

        public void Handle(string[] arguments)
        {
            if (arguments.Length < 1)
            {
                Debug.LogError("too few arguments for ORDER!");
                return;
            }
            
            inputHandler.ChangeInputMapTo("Default");

            Debug.Log("starting order.");

            string characterName = arguments[0];
            OrderManager orderManager = GetOrderManagerToName(characterName);
            Debug.Log($"name: {characterName}");

            Order order = SelectRandomOrder();
            int amount = SelectRandomAmount();
            Debug.Log($"order: {order} x{amount}");

            DisplayThoughtBubble(order, amount);
            Debug.Log($"displaying thought bubble");

            orderManager.InitializeOrder(order, amount);
        }

        private OrderManager GetOrderManagerToName(string characterName)
        {
            return characterName switch
            {
                "don" => don,
                "cthullu" => cthullu,
                "coral" => coral,
                "jean" => jean,
                _ => null
            };
        }

        private Order SelectRandomOrder()
        {
            return Order.MUFFIN;
        }

        private int SelectRandomAmount()
        {
            return 1;
            return _random.NextInt(minAmount, maxAmount);
        }

        private void DisplayThoughtBubble(Order order, int amount)
        {
            amountText.text = AmountX + amount;

            GameObject sprite = GetSpriteToOrder(order);
            sprite.SetActive(true);
            amountText.gameObject.SetActive(true);

            thoughtBubble.SetActive(true);
        }

        private GameObject GetSpriteToOrder(Order order)
        {
            return order switch
            {
                Order.COOKIE => cookieImage,
                Order.DONUT => donutImage,
                Order.MUFFIN => muffinImage,
                _ => throw new ArgumentOutOfRangeException(nameof(order), order, $"unknown order: {order}")
            };
        }

        private void ResetOrder()
        {
            thoughtBubble.SetActive(false);
            amountText.gameObject.SetActive(false);
            cookieImage.SetActive(false);
            donutImage.SetActive(false);
            muffinImage.SetActive(false);
            amountText.text = AmountX + "0";
        }

        public void Done()
        {
            Debug.Log($"done");

            ResetOrder();
            InformTimelineToGoOn();
        }

        public void UpdateText(int amount)
        {
            amountText.text = "x " + amount;
        }


        private void InformTimelineToGoOn()
        {
            timelineParser.ParseNextLine();
        }
    }
}