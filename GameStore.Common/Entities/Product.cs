using System.Text.Json.Serialization;

namespace GameStore.Common.Entities;

// Делегат для повідомлення про зміну ціни
public delegate void PriceChangedHandler(
	string productName,
	decimal oldPrice,
	decimal newPrice);

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(DigitalGame), "digital")]
[JsonDerivedType(typeof(PhysicalDisk), "physical")]
[JsonDerivedType(typeof(ConsoleGame), "console")]
public abstract class Product
{
	// Статичне поле
	private static int _totalProductsCreated;

	// Статичний конструктор
	static Product()
	{
		_totalProductsCreated = 0;
	}

	// Властивості
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Genre { get; set; }
	public decimal Price { get; set; }

	// Подія
	public event PriceChangedHandler? OnPriceChanged;

	// Звичайний конструктор
	protected Product(string name, string genre, decimal price)
	{
		Id = GenerateId();
		Name = name;
		Genre = genre;
		Price = price;

		_totalProductsCreated++;
	}

	// Метод
	public virtual string GetProductSummary()
	{
		return $"{Name} | Жанр: {Genre} | Ціна: ${Price:F2}";
	}

	// Статичний метод
	public static Guid GenerateId()
	{
		return Guid.NewGuid();
	}

	// Статичний метод
	public static int GetCreatedProductsCount()
	{
		return _totalProductsCreated;
	}

	// Метод зміни ціни з викликом події
	public void ChangePrice(decimal newPrice)
	{
		decimal oldPrice = Price;
		Price = newPrice;

		OnPriceChanged?.Invoke(Name, oldPrice, newPrice);
	}
}