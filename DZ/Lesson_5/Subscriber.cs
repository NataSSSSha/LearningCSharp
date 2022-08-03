using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    /// <summary>
    /// Класс Subscriber содержит информацию об абонетах и их телефонных номерах.
    /// </summary>
    internal class Subscriber
    {
        /// <summary>
        /// абонент
        /// </summary>
        private string name;

        public string Name { get { return name; } }
        /// <summary>
        /// номер телефона абонента
        /// </summary>
        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { this.phoneNumber = value; }
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="name">абонент</param>
        /// <param name="phoneNumber">номер телефона</param>
        public Subscriber(string name, string phoneNumber)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
        }
    }
}
