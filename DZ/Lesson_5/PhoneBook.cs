using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    internal class PhoneBook
    {
        /// <summary>
        /// Объект, в котором хранится единственный экземпляр класса
        /// </summary>
        private static PhoneBook intance;

        /// <summary>
        /// Путь к тфайлу с данными абонентов
        /// </summary>
        private string path = Path.Combine(Directory.GetCurrentDirectory(), "phonebook.txt");

        /// <summary>
        /// Список с абонентами
        /// </summary>
        List<Subscriber> subscribers = new List<Subscriber>();

        /// <summary>
        /// Конструктор
        /// </summary>
        private PhoneBook()
        {
            string newName = "Subscriber";
            string newPhoneBook;
            int countLineInFile = 0;
            if (File.Exists(path))
                using (StreamReader fs = new StreamReader(path, true))
                {
                    while (true)
                    {
                        if (fs.EndOfStream) break;

                        if (countLineInFile % 2 == 0)
                            newName = fs.ReadLine();

                        if (countLineInFile % 2 == 1)
                        {
                            newPhoneBook = fs.ReadLine();
                            Subscriber subscriber = new Subscriber(newName, newPhoneBook);
                            subscribers.Add(subscriber);
                        }
                        countLineInFile++;
                    }
                }
            Menu();
        }

        /// <summary>
        /// Создает единственный экземпляр класса
        /// </summary>
        /// <returns>единственный экземпляр класса</returns>
        public static PhoneBook GetIntance()
        {
            if (intance == null)
                intance = new PhoneBook();
            return intance;
        }

        /// <summary>
        /// Меню
        /// </summary>
        private void Menu()
        {
            int numberOfMenu;
            do
            {
                Console.WriteLine("Выбери действие:\n" +
                    "1 - добавить контакт\n" +
                    "2 - удалить контакт\n" +
                    "3 - найти контакт по имени\n" +
                    "4 - найти контакт по номеру\n" +
                    "5 - завершить программу");

                bool result = int.TryParse(Console.ReadLine(), out numberOfMenu);
                if (result)
                    switch (numberOfMenu)
                    {
                        case 1:
                            AddSubscriber();
                            break;
                        case 2:
                            DeleteSybscriber();
                            break;
                        case 3:
                            FindNuberOfSubscriber();
                            break;
                        case 4:
                            FindNameOfSubscriber();
                            break;
                        case 5:
                            WriteToFile();
                            break;
                        default:
                            Console.WriteLine("Неверный номер");
                            break;
                    }
                else
                    Console.WriteLine("Неверное значение");
            }
            while (numberOfMenu != 5);
        }

        /// <summary>
        /// Добавляет абонента
        /// </summary>
        private void AddSubscriber()
        {
            Console.WriteLine("Введите имя:");
            string? newName = Console.ReadLine();
            Console.WriteLine("Введите номер:");
            string? newPhoneNumber = Console.ReadLine();

            if (ISNewSubscriberCorreect(newName, newPhoneNumber))
            {
                Subscriber newSubscriber = new Subscriber(newName, newPhoneNumber);
                subscribers.Add(newSubscriber);
                Console.WriteLine("Пользователь добавлен");
            }
        }

        /// <summary>
        /// Проверяет данные абонента
        /// </summary>
        /// <param name="name">имя абонента</param>
        /// <param name="phoneNumber">номер б</param>
        /// <returns>корректно ли введены данные абонента</returns>
        private bool ISNewSubscriberCorreect(string name, string phoneNumber)
        {
            if (name == null || phoneNumber == null)
            {
                Console.WriteLine("Отсутствуют данные");
                return false;
            }

            if (!(int.TryParse(phoneNumber, out int result)))
            {
                Console.WriteLine("Введенное значение не является номером");
                return false;
            }

            for (int i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].PhoneNumber == phoneNumber)
                {
                    Console.WriteLine("Такой номер уже существует");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Удаляет абонента
        /// </summary>
        private void DeleteSybscriber()
        {
            bool IsDeleteSucsses = false;
            if (subscribers.Count == 0)
                Console.WriteLine("Пока нет ни одного абонента:(");
            else
            {
                Console.WriteLine("Введите имя абонента, которого хотите удалить");
                string? name = Console.ReadLine();
                for (int i = 0; i < subscribers.Count; i++)
                {
                    if (subscribers[i].Name == name)
                    {
                        Console.WriteLine($"Контакт {subscribers[i].Name} ," +
                            $" номер {subscribers[i].PhoneNumber}  успешно удален");
                        subscribers.RemoveAt(i);
                        i = -1;
                        IsDeleteSucsses = true;
                    }
                }
                if (!IsDeleteSucsses)
                    Console.WriteLine("Абонент не найден");
            }
        }

        /// <summary>
        /// Выводит номер(а) телефона абонента по имени
        /// </summary>
        private void FindNuberOfSubscriber()
        {
            bool IsFound = false;
            Console.WriteLine("Введите имя абонента:");
            string? name = Console.ReadLine();
            for (int i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].Name == name)
                {
                    Console.Write("Номер абонента: ");
                    Console.WriteLine(subscribers[i].PhoneNumber);
                    IsFound = true;
                }
            }
            if (!IsFound)
                Console.WriteLine("Абонент не найден");
        }

        /// <summary>
        /// Выводит имя абонента по номеру
        /// </summary>
        private void FindNameOfSubscriber()
        {
            bool IsFound = false;
            Console.WriteLine("Введите номер абонента:");
            string? phoneNumber = Console.ReadLine();
            for (int i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].PhoneNumber == phoneNumber)
                {
                    Console.Write("Имя абонента: ");
                    Console.WriteLine(subscribers[i].Name);
                    IsFound = true;
                }
            }
            if (!IsFound)
                Console.WriteLine("Абонент не найден");
        }

        /// <summary>
        /// Записывает данные абонента в файл
        /// </summary>
        private void WriteToFile()
        {
            using (FileStream file = new FileStream(path, FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(file))
            {
                for (int i = 0; i < subscribers.Count; i++)
                {
                    sw.WriteLine(subscribers[i].Name);
                    sw.WriteLine(subscribers[i].PhoneNumber);
                }
            }
        }
    }
}
