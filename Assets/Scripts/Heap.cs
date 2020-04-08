using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T>
{
	public int itemCount;
	T[] items;
	int[] values;

	public Heap(int maxSize) {
		items = new T[maxSize];
		values = new int[maxSize];
	}

	int GetLeftChildIndex(int i) => 2 * i + 1;
	int GetRightChildIndex(int i) => 2 * i + 2;
	int GetParentIndex(int i) => (i - 1) / 2;

	void Swap(int i, int j) {
		T tempItem = items[i];
		items[i] = items[j];
		items[j] = tempItem;

		int tempValue = values[i];
		values[i] = values[j];
		values[j] = tempValue;
	}

	public void Insert(T item, int value) {
		items[itemCount] = item;
		values[itemCount] = value;
		itemCount++;
		SiftUp();
	}

	void SiftUp() {
		int index = itemCount - 1;
		while (true) {
			int parentIndex = GetParentIndex(index);
			if (values[index] <= values[parentIndex] && index != 0) {
				Swap(parentIndex, index);
				index = parentIndex;
			} else {
				break;
			}
		}
	}

	public T Extract() {
		T rootItem = items[0];
		items[0] = items[itemCount - 1];
		values[0] = values[itemCount - 1];
		itemCount--;
		SiftDown();
		return rootItem;
	}

	void SiftDown() {
		int index = 0;
		while (true) {
			int leftChildIndex = GetLeftChildIndex(index);
			int rightChildIndex = GetRightChildIndex(index);

			if (leftChildIndex >= itemCount) {
				break;
			}

			int smallerValueIndex = leftChildIndex;
			if (rightChildIndex < itemCount && values[rightChildIndex] < values[leftChildIndex]) {
				smallerValueIndex = rightChildIndex;
			}

			if (values[smallerValueIndex] >= values[index]) {
				break;
			}

			Swap(index, smallerValueIndex);
			index = smallerValueIndex;
		}
	}


}
