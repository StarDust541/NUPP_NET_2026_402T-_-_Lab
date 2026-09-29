using GameStore.Common.Entities;

namespace GameStore.Common.Extensions;

public static class ProductExtensions
{
    // Метод розширення
    public static void ApplyDiscount(this Product product, decimal discountPercent)
    {
        if (discountPercent < 0 || discountPercent > 100)
        {
            throw new ArgumentException(
                "Знижка повинна бути від 0 до 100%.");
        }

        decimal newPrice =
            product.Price * (1 - discountPercent / 100);

        product.ChangePrice(newPrice);
    }
}