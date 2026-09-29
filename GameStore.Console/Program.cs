using System.Text;
using GameStore.Common.Entities;
using GameStore.Common.Extensions;
using GameStore.Common.Services;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("==============================================");
Console.WriteLine("       GAME STORE - LABORATORY WORK 1");
Console.WriteLine("==============================================\n");

// ======================================================
// 1. Демонстрація делегата, події та extension method
// ======================================================

Console.WriteLine("--- 1. ПОДІЇ ТА МЕТОД РОЗШИРЕННЯ ---");

var witcher = new DigitalGame(
    "The Witcher 3",
    "RPG",
    39.99m,
    50.5,
    "GOG");

// Підписка на подію
witcher.OnPriceChanged += (name, oldPrice, newPrice) =>
{
    Console.WriteLine(
        $"[ПОДІЯ]: Ціна '{name}' змінилася " +
        $"з ${oldPrice:F2} на ${newPrice:F2}");
};

Console.WriteLine($"Початкова ціна: ${witcher.Price:F2}");

// Використання методу розширення
witcher.ApplyDiscount(25);

Console.WriteLine($"Ціна після знижки: ${witcher.Price:F2}");


// ======================================================
// 2. Статичне поле та статичний метод
// ======================================================

Console.WriteLine("\n--- 2. СТАТИЧНІ КОНСТРУКЦІЇ ---");

Console.WriteLine(
    $"Створено об'єктів Product: " +
    $"{Product.GetCreatedProductsCount()}");


// ======================================================
// 3. Ініціалізація CRUD
// ======================================================

Console.WriteLine("\n--- 3. CRUD ОПЕРАЦІЇ ---");

var gameService = new InMemoryCrudService<Product>();


// Створення об'єктів

var cyberpunk = new PhysicalDisk(
    "Cyberpunk 2077",
    "Action-RPG",
    49.99m,
    "Steelbook Edition",
    350,
    true);

var starfield = new DigitalGame(
    "Starfield",
    "Sci-Fi RPG",
    69.99m,
    125.0,
    "Steam");

var gta = new ConsoleGame(
    "GTA V",
    "Action",
    29.99m,
    "PlayStation 5",
    true,
    18);


// CREATE

Console.WriteLine("\nCREATE:");

gameService.Create(witcher);
gameService.Create(cyberpunk);
gameService.Create(starfield);
gameService.Create(gta);

Console.WriteLine(
    $"Створено продуктів. " +
    $"Всього в базі: {gameService.ReadAll().Count()}");


// READ ALL

Console.WriteLine("\nREAD ALL:");

foreach (var product in gameService.ReadAll())
{
    Console.WriteLine(
        $"- [{product.Id}] " +
        product.GetProductSummary());
}


// READ

Console.WriteLine("\nREAD:");

Console.WriteLine(
    $"Пошук товару за Id: {witcher.Id}");

var foundGame = gameService.Read(witcher.Id);

Console.WriteLine(
    $"Знайдено: {foundGame.GetProductSummary()}");


// UPDATE

Console.WriteLine("\nUPDATE:");

Console.WriteLine(
    $"Стара ціна Cyberpunk: ${cyberpunk.Price:F2}");

cyberpunk.Price = 44.99m;

gameService.Update(cyberpunk);

Console.WriteLine(
    $"Нова ціна Cyberpunk: " +
    $"${gameService.Read(cyberpunk.Id).Price:F2}");


// DELETE

Console.WriteLine("\nDELETE:");

Console.WriteLine(
    "Видалення гри Starfield...");

gameService.Remove(starfield);

Console.WriteLine(
    $"Залишилося елементів: " +
    $"{gameService.ReadAll().Count()}");


// ======================================================
// 4. SAVE / LOAD
// ======================================================

Console.WriteLine(
    "\n--- 4. СЕРІАЛІЗАЦІЯ / ДЕСЕРІАЛІЗАЦІЯ ---");

string savePath = "games_backup.json";


// SAVE

gameService.Save(savePath);

Console.WriteLine(
    $"Дані успішно збережено у файл: " +
    $"'{savePath}'");


// Створюємо новий сервіс

var newService =
    new InMemoryCrudService<Product>();


// LOAD

newService.Load(savePath);

Console.WriteLine(
    $"Дані успішно завантажено у новий сервіс.");

Console.WriteLine(
    $"Кількість елементів: " +
    $"{newService.ReadAll().Count()}");

foreach (var product in newService.ReadAll())
{
    Console.WriteLine(
        $"- {product.GetProductSummary()}");
}


// ======================================================
// Завершення
// ======================================================

Console.WriteLine(
    "\n==============================================");

Console.WriteLine(
    "       ТЕСТУВАННЯ УСПІШНО ЗАВЕРШЕНО!");

Console.WriteLine(
    "==============================================");