using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Order
    {
        public int OrderId { get; private set; }
        public Passenger Passenger { get; private set; }
        public Driver Driver { get; private set; }
        public string Destination { get; private set; }
        public string Status { get; private set; }

        // Подія завершення замовлення
        public event Action OrderCompleted;

        // Конструктор створення замовлення
        public Order(int orderId, Passenger passenger, string destination)
        {
            OrderId = orderId;
            Passenger = passenger;
            Destination = destination;
            Status = "Очікує водія";
        }

        // Призначити водія на замовлення
        public void AssignDriver(Driver driver)
        {
            Driver = driver;
            Status = "Призначено водія";
            NotifyParticipants();
        }

        // Метод завершення замовлення
        public void CompleteOrder()
        {
            Status = "Завершено";
            OrderCompleted?.Invoke();
            UnsubscribeEvents();
        }

        // Метод сповіщення учасників
        private void NotifyParticipants()
        {
            Passenger.NotifyTaxiArrival(Driver.Name);
            Driver.AcceptOrder(Destination);
        }

        // Метод відписки від подій
        private void UnsubscribeEvents()
        {
            OrderCompleted = null;
        }

        // Метод виводу інформації
        public void DisplayOrderInfo()
        {
            Console.WriteLine($"\nНомер замовлення: {OrderId}");
            Console.WriteLine($"Клієнт: {Passenger.Name} ({(Passenger.IsVip ? "VIP клієнт" : "Звичайний пасажир")})");
            Console.WriteLine($"Пункт призначення: {Destination}");
            Console.WriteLine($"Статус: {Status}");
            Console.WriteLine($"Водій: {(Driver != null ? Driver.Name : "Не призначено")}");
        }
    }
}