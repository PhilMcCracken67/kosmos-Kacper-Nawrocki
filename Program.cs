using System;
using System.Collections.Generic;

// --- INTERFEJSY ---
public interface IBaseBuilder
{
    string BuildSecretBase();
}

// --- 1. ABSTRAKCJA ---
public abstract class SpaceObject
{
    public string Name { get; protected set; }

    private int _dangerLevel;

    // WALIDACJA (Właściwość)
    public int DangerLevel
    {
        get { return _dangerLevel; }
        set
        {
            if (value < 1 || value > 10)
                throw new ArgumentException("Hej! Poziom zagrożenia musi być od 1 do 10!");
            _dangerLevel = value;
        }
    }

    public SpaceObject(string name, int dangerLevel)
    {
        Name = name;
        DangerLevel = dangerLevel; 
    }

    public abstract string GetVibe();

    // PRZECIĄŻENIE METOD
    public void Scan()
    {
        Console.WriteLine($"🔭 Szybki skan {Name}: Wygląda bezpiecznie...");
    }

    public void Scan(bool deepScan)
    {
        if (deepScan)
            Console.WriteLine($"📡 Głęboki skan {Name}: Wykryto potwory i zaginione statki!");
        else
            Scan();
    }
}

// --- 2. DZIEDZICZENIE I POLIMORFIZM ---
public class CoolPlanet : SpaceObject
{
    public bool HasAliens { get; private set; }
    public string Weather { get; private set; }

    // PRZECIĄŻENIE KONSTRUKTORÓW
    public CoolPlanet(string name, int dangerLevel, bool hasAliens, string weather)
        : base(name, dangerLevel)
    {
        HasAliens = hasAliens;
        Weather = weather;
    }

    
    public CoolPlanet(string name)
        : base(name, 1) 
    {
        HasAliens = true;
        Weather = "Słońce i przyjemny wiaterek";
    }

    public override string GetVibe() => $"Planeta. Pogoda: {Weather}. Obcy: {(HasAliens ? "Są!" : "Brak")}.";
}

public class TreasureAsteroid : SpaceObject
{
    public int TonsOfGold { get; private set; }

    public TreasureAsteroid(string name, int dangerLevel, int gold)
        : base(name, dangerLevel)
    {
        TonsOfGold = gold;
    }

    public override string GetVibe() => $"Asteroida pełna kasy. Znaleziono {TonsOfGold} ton złota!";
}

// --- DZIEDZICZENIE I INTERFEJSY ---
public class SciFiWorld : CoolPlanet, IBaseBuilder
{
    public string SecretTech { get; private set; }

    public SciFiWorld(string name, int dangerLevel, bool hasAliens, string weather, string tech)
        : base(name, dangerLevel, hasAliens, weather)
    {
        SecretTech = tech;
    }

    public string BuildSecretBase()
    {
        return $"🏗️ Baza założona na {Name}! Kradniemy technologię: {SecretTech}.";
    }
}

// --- 3. ENKAPSULACJA ---
public class SpaceExplorer
{
    public string Nickname { get; private set; }
    public int SpaceCredits { get; private set; }

    public SpaceExplorer(string nickname, int credits)
    {
        Nickname = nickname;
        SpaceCredits = credits;
    }
}

// --- 4. KOMPOZYCJA i METODY STATYCZNE ---
public class SpaceAdventure
{
    public SpaceExplorer Explorer { get; private set; }
    public SpaceObject Destination { get; private set; }

    public SpaceAdventure(SpaceExplorer explorer, SpaceObject destination)
    {
        Explorer = explorer;
        Destination = destination;
    }
}

public static class GalacticFederation
{
    public static List<SpaceAdventure> Adventures = new List<SpaceAdventure>();

    public static void SendOnAdventure(SpaceExplorer explorer, SpaceObject destination)
    {
        if (destination.DangerLevel > 8)
            Console.WriteLine($"⚠️ UWAGA: {explorer.Nickname} wyrusza na {destination.Name}. To ostra jazda!");

        Adventures.Add(new SpaceAdventure(explorer, destination));
    }
}

// --- KLASA ZARZĄDCZA ---
public class StarMap
{
    private List<SpaceObject> _objects = new List<SpaceObject>();

    public void AddToMap(SpaceObject obj) => _objects.Add(obj);

    public void PrintCoolMap()
    {
        Console.WriteLine("\n🗺️ --- MAPA GALAKTYKI --- 🗺️");
        foreach (var obj in _objects)
        {
            Console.WriteLine($"🌌 {obj.Name} (Zagrożenie: {obj.DangerLevel}/10) -> {obj.GetVibe()}");
        }
    }
}

// --- MAIN ---
class Program
{
    static void Main(string[] args)
    {
        StarMap map = new StarMap();

        CoolPlanet arrakis = new CoolPlanet("Arrakis", 9, true, "Zabójcze burze piaskowe");
        CoolPlanet naboo = new CoolPlanet("Naboo"); // Użycie prostego konstruktora
        TreasureAsteroid rock = new TreasureAsteroid("El Dorado", 5, 9999);
        SciFiWorld pandora = new SciFiWorld("Pandora", 8, true, "Świecący las", "Lewitujące skały");

        map.AddToMap(arrakis);
        map.AddToMap(naboo);
        map.AddToMap(rock);
        map.AddToMap(pandora);

        map.PrintCoolMap();

        Console.WriteLine("\n--- SKANOWANIE ---");
        naboo.Scan();
        arrakis.Scan(true);

        Console.WriteLine("\n--- AKCJE SPECJALNE ---");
        Console.WriteLine(pandora.BuildSecretBase());

        Console.WriteLine("\n--- WYSYŁAMY ODKRYWCÓW ---");
        SpaceExplorer quill = new SpaceExplorer("Star-Lord", 500);
        GalacticFederation.SendOnAdventure(quill, arrakis);
    }
}