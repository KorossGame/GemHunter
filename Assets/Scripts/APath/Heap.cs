using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    public int Count { get; private set; }

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        // Add new item to the heap
        item.HeapIndex = Count;
        items[Count] = item;

        // Find items correct place
        SortUp(item);

        // Increment counter of object in heap
        Count++;
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        Count--;
        // Make last heap element as top
        items[0] = items[Count];
        items[0].HeapIndex = 0;

        // Find appropriate place of new heap element
        SortDown(items[0]);
        return firstItem;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true) {
            T parentItem = items[parentIndex];

            // Higher priority = 1, same = 0, lower = -1
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            // Check if current element has left child
            if (childIndexLeft < Count)
            {
                swapIndex = childIndexLeft;

                // Check if current element has right child
                if (childIndexRight < Count)
                {
                    // Lower priority of left child
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                // Check if parent got lower priority comparing to its child
                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                // Correct possition
                else {
                    return;
                }
            }
            // Doesn't have any children
            else {
                return;
            }
        }
    }

    void Swap(T itemA, T itemB)
    {
        // Swap in array
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;

        // Swap heap values
        int tmpIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = tmpIndex;
    }

}

public interface IHeapItem<T>:IComparable<T>
{
    int HeapIndex { get; set; }
}