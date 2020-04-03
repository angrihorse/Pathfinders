using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PriorityQueue<T>
{
    // The items and values.
    List<T> items = new List<T>();
    List<int> values = new List<int>();

    // Return the number of items in the queue.
    public int Count
    {
        get
        {
            return items.Count;
        }
    }

    // Add an item to the queue.
    public void Enqueue(T item, int value)
    {
        items.Add(item);
        values.Add(value);
    }

    // Dequeue the item with the smallest value from the queue.
    public T Dequeue()
    {
        // Find the index corresponding to the smallest value.
        int bestIndex = 0;
        int bestValue = values[0];
        for (int i = 1; i < values.Count; i++)
        {
            if (values[i] < bestValue)
            {
                bestValue = values[i];
                bestIndex = i;
            }
        }

		// Get the item.
        T bestItem = items[bestIndex];

        // Remove the item from the lists.
        items.RemoveAt(bestIndex);
        values.RemoveAt(bestIndex);

		return bestItem;
    }
}
