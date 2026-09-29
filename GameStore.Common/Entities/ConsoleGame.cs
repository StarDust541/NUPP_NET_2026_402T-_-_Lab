namespace GameStore.Common.Entities;

public class ConsoleGame : Product
{
    // Властивості
    public string ConsoleName { get; set; }
    public bool Multiplayer { get; set; }
    public int AgeRating { get; set; }

    // Конструктор
    public ConsoleGame(
        string name,
        string genre,
        decimal price,
        string consoleName,
        bool multiplayer,
        int ageRating)
        : base(name, genre, price)
    {
        ConsoleName = consoleName;
        Multiplayer = multiplayer;
        AgeRating = ageRating;
    }

    // Перевизначення методу
    public override string GetProductSummary()
    {
        return $"{base.GetProductSummary()} | Console: {ConsoleName} | Multiplayer: {Multiplayer} | PEGI {AgeRating}";
    }
}