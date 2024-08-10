using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class CatManager
{
    private static List<Cat> _cats = new List<Cat>();

    public static void AddCat(string name, int age)
    {
        ILifeLevelStrategy strategy = age <= 5 ? new LifeLevelStrategyYoung() :
                                      age <= 10 ? new LifeLevelStrategyAdult() :
                                      (ILifeLevelStrategy)new LifeLevelStrategyOld();

        Cat newCat = new Cat(name, age, strategy);
        _cats.Add(newCat);
    }

    public static void RemoveCat(Cat cat)
    {
        _cats.Remove(cat);
    }

    public static void SaveCats(string filePath)
    {
        string json = JsonConvert.SerializeObject(_cats, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static void LoadCats(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            _cats = JsonConvert.DeserializeObject<List<Cat>>(json);
        }
        else
        {
            Console.WriteLine("Файл с котами не найден.");
        }
    }

    public static Cat FindCatByName(string name)
    {
        return _cats.Find(cat => cat.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public static void DisplayCats()
    {
        _cats.Sort(new CatComparer());

        Console.WriteLine("Имя\tВозраст\tСытость\tНастроение\tЗдоровье\tСредний уровень жизни");
        foreach (var cat in _cats)
        {
            Console.WriteLine($"{cat.Name}\t{cat.Age}\t{cat.Satiety}\t{cat.Mood}\t{cat.Health}\t{cat.AverageLifeLevel:F2}");
        }
    }
}
