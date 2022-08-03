using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    /// <summary>
    /// Класс PhoneBook предоставляет CRUD функциональные методы для работы с абонентами.
    /// </summary>
    public class PhoneBook
    {
        /// <summary>
        /// Объект, в котором хранится единственный экземпляр класса.
        /// </summary>
        private static PhoneBook intance;

        /// <summary>
        /// Имя файла, в котором хранятся данные об абонентах.
        /// </summary>
        private const string nameOfFile = "phonebook.txt";

        /// <summary>
        /// Путь к файлу с данными абонентов.
        /// </summary>
        private string path = Path.Combine(Directory.GetCurrentDirectory(), nameOfFile);

        /// <summary>
        /// Список с абонентами.
        /// </summary>
        internal List<Subscriber> subscribers = new List<Subscriber>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        private PhoneBook()
        {
            if (File.Exists(path))
                using (StreamReader fs = new StreamReader(path, true))
                {
                    while (true)
                    {
                        if (fs.EndOfStream) break;
                        string line = fs.ReadLine();
                        string[] Sub = line.Split(new char[] {'\t'}, StringSplitOptions.RemoveEmptyEntries);

                        Subscriber subscriber = new Subscriber(Sub[0], Sub[1]);
                        subscribers.Add(subscriber);
                    }
                }
            Console.WriteLine(subscribers[1].PhoneNumber);
        }

        /// <summary>
        /// Создает единственный экземпляр класса.
        /// </summary>
        /// <returns>Единственный экземпляр класса.</returns>
        public static PhoneBook GetIntance()
        {
            if (intance == null)
                intance = new PhoneBook();
            return intance;
        }

        /// <summary>
        /// Добавляет абонента.
        /// </summary>
        /// <param name="name">Имя абонента.</param>
        /// <param name="phoneNumber">Номер абонента.</param>
        public void AddSubscriber(string name, string phoneNumber)
        {
            if (ISNewSubscriberCorrect(name, phoneNumber))
            {
                Subscriber newSubscriber = new Subscriber(name, phoneNumber);
                subscribers.Add(newSubscriber);
                Console.WriteLine("Пользователь добавлен");
            }
        }

        /// <summary>
        /// Проверяет данные абонента.
        /// </summary>
        /// <param name="name">Имя абонента.</param>
        /// <param name="phoneNumber">Номер абонента.</param>
        /// <returns>Корректно ли введены данные абонента.</returns>
        private bool ISNewSubscriberCorrect(string name, string phoneNumber)
        {
            if (name == null || phoneNumber == null)
            {
                Console.WriteLine("Отсутствуют данные");
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
        /// Удаляет абонента.
        /// </summary>
        /// <param name="name">Имя абонента.</param>
        /// <returns>Удален ли абонент.</returns>
        public bool DeleteSybscriber(string name)
        {
            bool IsDeleteSucsses = false;
                for (int i = 0; i < subscribers.Count; i++)
                {
                    if (subscribers[i].Name == name)
                    {
                        subscribers.RemoveAt(i);
                        i = -1;
                        IsDeleteSucsses = true;
                    }
                }
            return IsDeleteSucsses;
        }

        /// <summary>
        /// Выводит номер(а) телефона абонента по имени.
        /// </summary>
        /// <param name="name">Имя абонента.</param>
        public void FindSubscriberByName(string name)
        {
            bool IsFound = false;

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
        /// Выводит имя абонента по номеру.
        /// </summary>
        /// <param name="phoneNumber">Номер абонента.</param>
        public void FindSubscriberByNumber(string phoneNumber)
        {
            bool IsFound = false;

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
        /// Записывает данные абонента в файл.
        /// </summary>
        public void WriteToFile()
        {
            using (FileStream file = new FileStream(path, FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(file))
            {
                for (int i = 0; i < subscribers.Count; i++)
                {
                    sw.WriteLine($"{ subscribers[i].Name}\t{ subscribers[i].PhoneNumber}");
                }
            }
        }
    }
}
