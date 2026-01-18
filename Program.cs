using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork9
{
    // 1. Продукт - Складний об'єкт документації
    public class Documentation
    {
        private List<string> _parts = new List<string>();

        public void AddPart(string part) => _parts.Add(part);

        public void Show()
        {
            Console.WriteLine("--- ВМІСТ ДОКУМЕНТАЦІЇ ---");
            foreach (var part in _parts)
            {
                Console.WriteLine(part);
            }
            Console.WriteLine("--------------------------\n");
        }
    }

    // 2. Інтерфейс Будівельника
    public interface IDocumentBuilder
    {
        void AddTitle(string title);
        void AddSection(string sectionName, string content);
        void AddFooter(string footerText);
        Documentation GetDocumentation();
    }

    // 3. Конкретний Будівельник
    public class TechnicalDocBuilder : IDocumentBuilder
    {
        private Documentation _doc = new Documentation();

        public void AddTitle(string title) =>
            _doc.AddPart($"[ГОЛОВНИЙ ЗАГОЛОВОК]: {title.ToUpper()}");

        public void AddSection(string sectionName, string content) =>
            _doc.AddPart($"\nРозділ: {sectionName}\nЗміст: {content}");

        public void AddFooter(string footerText) =>
            _doc.AddPart($"\n(Виноска: {footerText})");

        public Documentation GetDocumentation()
        {
            Documentation result = _doc;
            _doc = new Documentation(); // Скидання для нового об'єкта
            return result;
        }
    }

    // 4. Директор - керує процесом побудови
    public class Director
    {
        public void BuildMinimalDoc(IDocumentBuilder builder)
        {
            builder.AddTitle("Проста інструкція");
            builder.AddSection("Вступ", "Це базовий текст документації.");
        }

        public void BuildFullDoc(IDocumentBuilder builder)
        {
            builder.AddTitle("Повна технічна документація проєкту");
            builder.AddSection("Опис системи", "Опис усіх модулів системи.");
            builder.AddSection("Інсталяція", "Кроки для встановлення ПЗ.");
            builder.AddFooter("Конфіденційно. 2024.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №9: Патерн Builder ===\n");

            var director = new Director();
            var builder = new TechnicalDocBuilder();

            // Створюємо повну документацію через директора
            Console.WriteLine("Створення повної документації:");
            director.BuildFullDoc(builder);
            Documentation fullDoc = builder.GetDocumentation();
            fullDoc.Show();

            // Створюємо документацію вручну (гнучкість патерна)
            Console.WriteLine("Створення кастомної документації вручну:");
            builder.AddTitle("Звіт про тестування");
            builder.AddSection("Результати", "Усі тести пройдено успішно.");
            builder.AddFooter("Автор: Микита.");
            builder.GetDocumentation().Show();

            Console.ReadKey();
        }
    }
}