using System;
using System.Collections.Generic;

namespace ProceduralGeneration;

public class InMemoryDatabase<V> where V : class
{
  public readonly Dictionary<object, V> entries = new();
  private readonly object lockObject = new();

  public V GetEntry(object key) => entries.TryGetValue(key, out V value) ? value : null;

  public V AddEntry(object key, V entry)
  {
    lock (lockObject)
      entries.Add(key, entry);

    return entry;
  }

  public bool RemoveEntry(object key)
  {
    bool isSuccess;
    lock (lockObject)
      isSuccess = entries.Remove(key);

    return isSuccess;
  }

  public V[] FilterBy(Func<V, bool> predicate)
  {
    List<V> filteredList = new();
    foreach (var item in entries.Values)
      if (predicate(item))
        filteredList.Add(item);

    return filteredList.ToArray();
  }
}