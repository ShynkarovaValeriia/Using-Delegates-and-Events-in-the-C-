using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Program
    {
        private static List<Taxi> Taxis = new List<Taxi>();
        private static List<Order> Orders = new List<Order>();
        private static int orderCounter = 1;

        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("\nОберіть дію:");
                Console.WriteLine("1. Створити замовлення");
                Console.WriteLine("2. Зареєструвати клієнта");
                Console.WriteLine("3. Зареєструвати водія");
                Console.WriteLine("4. Показати статус замовлень");
                Console.WriteLine("5. Завершити замовлення");
                Console.WriteLine("0. Вийти");
                Console.Write("\nВведіть цифру: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        CreateOrder();
                        break;
                    case "2":
                        RegisterPassenger();
                        break;
                    case "3":
                        RegisterDriver();
                        break;
                    case "4":
                        DisplayOrderStatus();
                        break;
                    case "5":
                        CompleteOrder();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        private static void RegisterPassenger()
        {
            Console.WriteLine("\nРеєстрування клієнта");
            Console.Write("Введіть ім'я клієнта: ");
            string name = Console.ReadLine();

            Console.Write("Це VIP клієнт? (Так/Ні): ");
            bool isVip = Console.ReadLine()?.ToLower() == "так";

            var passenger = new Passenger(name, isVip);
            Taxis.Add(passenger);

            Console.WriteLine($"Клієнта {name} зареєстровано.");
        }

        private static void RegisterDriver()
        {
            Console.WriteLine("\nРеєстрування водія");
            Console.Write("Введіть ім'я водія: ");
            string name = Console.ReadLine();

            var driver = new Driver(name);
            Taxis.Add(driver);

            Console.WriteLine($"Водія {name} зареєстровано.");
        }

        private static void CreateOrder()
        {
            var availableDrivers = Taxis.OfType<Driver>().Where(d => d.IsAvailable).ToList();
            var availablePassengers = Taxis.OfType<Passenger>().ToList();

            if (availablePassengers.Count == 0 || availableDrivers.Count == 0)
            {
                Console.WriteLine("Спочатку зареєструйте клієнта та водія.");
                return;
            }

            Passenger passenger = ChoosePassenger(availablePassengers);
            if (passenger == null)
            {
                Console.WriteLine("Немає доступних клієнтів.");
                return;
            }

            Driver driver = ChooseDriver(availableDrivers);
            if (driver == null)
            {
                Console.WriteLine("Немає доступних водіїв.");
                return;
            }

            string destination = "";
            bool validDestination = false;
            while (!validDestination)
            {
                Console.WriteLine("\nОберіть пункт призначення:");
                Console.WriteLine("1. Аеропорт");
                Console.WriteLine("2. Центр міста");
                Console.WriteLine("3. Вокзал");
                Console.WriteLine("4. Парк");
                Console.WriteLine("5. Лікарня");
                Console.WriteLine("6. Готель");
                Console.Write("\nВведіть цифру: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        destination = "Аеропорт";
                        validDestination = true;
                        break;
                    case "2":
                        destination = "Центр міста";
                        validDestination = true;
                        break;
                    case "3":
                        destination = "Вокзал";
                        validDestination = true;
                        break;
                    case "4":
                        destination = "Парк";
                        validDestination = true;
                        break;
                    case "5":
                        destination = "Лікарня";
                        validDestination = true;
                        break;
                    case "6":
                        destination = "Готель";
                        validDestination = true;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }

            int orderId = orderCounter++;
            var order = new Order(orderId, passenger, destination);
            order.AssignDriver(driver);

            order.OrderCompleted += () =>
            {
                Console.WriteLine($"\nЗамовлення {orderId} завершено. Водія {driver.Name} звільнено.");
                availableDrivers.Add(driver);
                Orders.Remove(order);
            };

            Orders.Add(order);
            Console.WriteLine($"\nВодія {driver.Name} призначено на замовлення {orderId} для пасажира {passenger.Name} до {destination.ToLower()}.");
        }

        private static void CompleteOrder()
        {
            Console.Write("\nВведіть номер замовлення для завершення: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                var order = Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    order.CompleteOrder();
                }
                else
                {
                    Console.WriteLine("\nЗамовлення не знайдено.");
                }
            }
            else
            {
                Console.WriteLine("\nНевірний номер замовлення.");
            }
        }

        private static Passenger ChoosePassenger(List<Passenger> availablePassengers)
        {
            Passenger vipPassenger = availablePassengers.FirstOrDefault(p => p.IsVip);
            return vipPassenger ?? availablePassengers.FirstOrDefault();
        }

        private static Driver ChooseDriver(List<Driver> availableDrivers)
        {
            return availableDrivers.FirstOrDefault();
        }

        private static void DisplayOrderStatus()
        {
            Console.WriteLine("\nСтатус активних замовлень:");
            if (Orders.Count == 0)
            {
                Console.WriteLine("\nНемає активних замовлень.");
                return;
            }

            foreach (var order in Orders)
            {
                order.DisplayOrderInfo();
            }
        }
    }
}