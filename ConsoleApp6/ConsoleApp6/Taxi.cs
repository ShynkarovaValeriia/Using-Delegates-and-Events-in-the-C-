using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    // Абстрактний клас
    public abstract class Taxi
    {
        public string Name { get; private set; }
        public bool IsAvailable { get; protected set; }

        // Подія отримання нового замовлення
        public event Action<string> OrderAssigned;

        // Конструктор
        public Taxi(string name)
        {
            Name = name;
            IsAvailable = true;
        }

        // Метод завершення поточного замовлення
        public void CompleteOrder()
        {
            IsAvailable = true;
            Console.WriteLine($"{Name} завершив поїздку та готовий до нового замовлення.");
            UnsubscribeEvents();
        }

        // Метод відписки від подій
        protected void UnsubscribeEvents()
        {
            OrderAssigned = null;
        }
    }
}