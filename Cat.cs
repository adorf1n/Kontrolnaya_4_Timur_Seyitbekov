using System;

public class Cat
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int Satiety { get; private set; }
    public int Mood { get; private set; }
    public int Health { get; private set; }

    private ILifeLevelStrategy _lifeLevelStrategy;

    public event Action<string> MaxSatietyReached;
    public event Action<string> MaxMoodReached;
    public event Action<string> MaxHealthReached;

    public Cat(string name, int age, ILifeLevelStrategy lifeLevelStrategy)
    {
        Name = name;
        Age = age;
        Satiety = 10;
        Mood = 10;
        Health = 10;
        _lifeLevelStrategy = lifeLevelStrategy;
    }

    public double AverageLifeLevel => (Satiety + Mood + Health) / 3.0;

    public void Feed()
    {
        Satiety = Math.Min(100, Satiety + _lifeLevelStrategy.IncreaseValue);
        Mood = Math.Min(100, Mood + _lifeLevelStrategy.IncreaseValue);
        Health = Math.Max(0, Health - _lifeLevelStrategy.DecreaseValue);

        CheckMaxValues();
    }

    public void Play()
    {
        Satiety = Math.Max(0, Satiety - _lifeLevelStrategy.DecreaseValue);
        Mood = Math.Min(100, Mood + _lifeLevelStrategy.IncreaseValue);
        Health = Math.Max(0, Health - _lifeLevelStrategy.DecreaseValue);

        CheckMaxValues();
    }

    public void Treat()
    {
        Mood = Math.Max(0, Mood - _lifeLevelStrategy.DecreaseValue);
        Health = Math.Min(100, Health + _lifeLevelStrategy.IncreaseValue);

        CheckMaxValues();
    }

    private void CheckMaxValues()
    {
        if (Satiety == 100) MaxSatietyReached?.Invoke($"{Name} достиг максимального уровня сытости!");
        if (Mood == 100) MaxMoodReached?.Invoke($"{Name} достиг максимального уровня настроения!");
        if (Health == 100) MaxHealthReached?.Invoke($"{Name} достиг максимального уровня здоровья!");
    }

    public void ApplyRandomEvent()
    {
        Random random = new Random();
        int eventChance = random.Next(100);

        if (eventChance < 10) 
        {
            Console.WriteLine($"{Name} отравился!");
            Satiety = Satiety;
            Mood = Math.Max(0, Mood - 30);
            Health = Math.Max(0, Health - 30);
        }
        else if (eventChance < 20)
        {
            Console.WriteLine($"{Name} получил травму!");
            Mood = Math.Max(0, Mood - 20);
            Health = Math.Max(0, Health - 20);
        }

        if (Health == 0)
        {
            Console.WriteLine($"{Name} умер.");
            CatManager.RemoveCat(this);
        }
    }
}
