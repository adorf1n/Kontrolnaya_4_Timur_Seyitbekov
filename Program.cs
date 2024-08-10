using System;

class Program
{
    static void Main(string[] args)
    {
        CatManager.LoadCats("cats.json");

        while (true)
        {
            Console.Clear();
            CatManager.DisplayCats();

            Console.WriteLine("1. Добавить кота");
            Console.WriteLine("2. Покормить кота");
            Console.WriteLine("3. Поиграть с котом");
            Console.WriteLine("4. Полечить кота");
            Console.WriteLine("5. Выйти и сохранить");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddNewCat();
                    break;
                case "2":
                    InteractWithCat("Feed");
                    break;
                case "3":
                    InteractWithCat("Play");
                    break;
                case "4":
                    InteractWithCat("Treat");
                    break;
                case "5":
                    CatManager.SaveCats("cats.json");
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    static void AddNewCat()
    {
        try
        {
            Console.Write("Введите имя кота: ");
            string name = Console.ReadLine();

            Console.Write("Введите возраст кота: ");
            int age = int.Parse(Console.ReadLine());

            CatManager.AddCat(name, age);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void InteractWithCat(string action)
    {
        try
        {
            Console.Write("Введите имя кота: ");
            string name = Console.ReadLine();

            Cat cat = CatManager.FindCatByName(name);
            if (cat == null)
            {
                Console.WriteLine("Кот не найден.");
                return;
            }

            switch (action)
            {
                case "Feed":
                    cat.Feed();
                    break;
                case "Play":
                    cat.Play();
                    break;
                case "Treat":
                    cat.Treat();
                    break;
            }

            cat.ApplyRandomEvent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
