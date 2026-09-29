namespace GameStore.Common.Entities;

public class DigitalGame : Product
{
    // Властивості
    public double SizeGb { get; set; }
    public string Platform { get; set; }
    public string DownloadLink { get; set; }

    // Конструктор
    public DigitalGame(
        string name,
        string genre,
        decimal price,
        double sizeGb,
        string platform)
        : base(name, genre, price)
    {
        SizeGb = sizeGb;
        Platform = platform;
        DownloadLink = $"https://gamestore.local/download/{Id}";
    }

    // Перевизначення методу
    public override string GetProductSummary()
    {
        return $"{base.GetProductSummary()} | Digital | {SizeGb} GB | {Platform}";
    }
}