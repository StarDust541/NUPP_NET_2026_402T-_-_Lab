using System.Text.Json;
using GameStore.Common.Entities;

namespace GameStore.Common.Services;

public class InMemoryCrudService<T> : ICrudService<T>
    where T : Product
{
    // Вбудована колекція .NET
    private readonly List<T> _items = new();

    // CREATE
    public void Create(T element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        _items.Add(element);
    }

    // READ
    public T Read(Guid id)
    {
        T? element = _items.FirstOrDefault(x => x.Id == id);

        if (element == null)
        {
            throw new KeyNotFoundException(
                $"Елемент з Id {id} не знайдено.");
        }

        return element;
    }

    // READ ALL
    public IEnumerable<T> ReadAll()
    {
        return _items;
    }

    // UPDATE
    public void Update(T element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        int index = _items.FindIndex(x => x.Id == element.Id);

        if (index == -1)
        {
            throw new KeyNotFoundException(
                $"Елемент з Id {element.Id} не знайдено.");
        }

        _items[index] = element;
    }

    // DELETE
    public void Remove(T element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        _items.RemoveAll(x => x.Id == element.Id);
    }

    // Додаткове завдання: Save
    public void Save(string filePath)
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(_items, options);

        File.WriteAllText(filePath, json);
    }

    // Додаткове завдання: Load
    public void Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(
                "Файл збереження не знайдено.",
                filePath);
        }

        string json = File.ReadAllText(filePath);

        List<T>? loadedItems =
            JsonSerializer.Deserialize<List<T>>(json);

        _items.Clear();

        if (loadedItems != null)
        {
            _items.AddRange(loadedItems);
        }
    }
}