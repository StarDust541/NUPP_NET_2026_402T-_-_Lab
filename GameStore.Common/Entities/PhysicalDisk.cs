namespace GameStore.Common.Entities;

public class PhysicalDisk : Product
{
    // Властивості
    public string Edition { get; set; }
    public int WeightGrams { get; set; }
    public bool HasBox { get; set; }

    // Конструктор
    public PhysicalDisk(
        string name,
        string genre,
        decimal price,
        string edition,
        int weightGrams,
        bool hasBox)
        : base(name, genre, price)
    {
        Edition = edition;
        WeightGrams = weightGrams;
        HasBox = hasBox;
    }

    // Перевизначення методу
    public override string GetProductSummary()
    {
        return $"{base.GetProductSummary()} | Physical | {Edition} | {WeightGrams} g | Коробка: {HasBox}";
    }
}