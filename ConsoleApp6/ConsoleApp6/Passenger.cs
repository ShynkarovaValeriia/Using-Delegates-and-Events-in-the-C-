using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    // Клас для пасажира
    public class Passenger : Taxi
    {
        public bool IsVip { get; private set; }

        // Подія сповіщення про нове замовлення
        public event Action<string> TaxiArrivalNotified;

        // Конструктор імені та VIP статуса
        public Passenger(string name, bool isVip = false) : base(name)
        {
            IsVip = isVip;
        }

        // Метод отримання сповіщення про прибуття таксі
        public void NotifyTaxiArrival(string driverName)
        {
            Console.WriteLine($"\nКлієнт {Name} отримав повідомлення: Ваше таксі, під’їде за 5 хвилин.");
            TaxiArrivalNotified?.Invoke(driverName);
        }

        // Метод відписки від подій
        public new void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            TaxiArrivalNotified = null;
        }
    }
}