using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab6
{
    // Клас для водія
    public class Driver : Taxi
    {
        // Виклик конструктора
        public Driver(string name) : base(name) { }

        // Подія сповіщення про нове замовлення
        public event Action<string> OrderReceived; 

        // Метод отримання нового замовлення
        public void AcceptOrder(string destination)
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                Console.WriteLine($"\nВодій {Name} отримав повідомлення: Нове замовлення на поїздку до {destination.ToLower()}.");
                OrderReceived?.Invoke(destination);
            }
            else
            {
                Console.WriteLine($"\nВодій {Name} зараз зайнятий.");
            }
        }

        // Метод завершення поточного замовлення
        public new void CompleteOrder()
        {
            base.CompleteOrder();
            UnsubscribeEvents();
        }

        // Метод відписки від подій
        private void UnsubscribeEvents()
        {
            OrderReceived = null;
        }
    }
}