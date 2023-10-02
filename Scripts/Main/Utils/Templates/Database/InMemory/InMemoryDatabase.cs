using System;
using System.Collections.Generic;
using System.Linq;

public class InMemoryDatabase<K, V> where V : InMemoryEntity<K>
{
  private readonly Dictionary<K, V> entries = new();
  private readonly object lockObject = new();

  public V[] GetAll() => entries.Values.ToArray();
  public V GetEntry(K key) => entries.TryGetValue(key, out V value) ? value : null;

  public V AddEntry(K key, V entry)
  {
    lock (lockObject)
    {
      entries.Add(key, entry);
      entry.key = key;
    }
    return entry;
  }

  public bool RemoveEntry(K key)
  {
    bool isSuccess;
    lock (lockObject)
    {
      isSuccess = entries.Remove(key);
    }

    return isSuccess;
  }

  public V[] Filter(Func<V, bool> predicate)
  {
    List<V> filteredList = new();
    foreach (var item in GetAll())
      if (predicate(item))
        filteredList.Add(item);

    return filteredList.ToArray();
  }
}