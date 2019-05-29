using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sort<T, V> where V : IComparable<V>
{
    public static bool MergeSort(T[] array)
    {
        if (typeof(V) == typeof(T))
        {
            Func<T[], int, V> accessor = (T[] arrayToAccess, int index) => (V)Convert.ChangeType(array[index], typeof(V));
            MergeSort(array, accessor);
            return true;
        }
        else
        {
            Debug.Log("Error: Array and value type mismatch. Cannot determine accessor lambda.");
            Debug.Log("Error: T type: " + typeof(T).ToString() + ". V Type: " + typeof(V).ToString() + ".");
            return false;
        }
    }

    public static void MergeSort(T[] array, Func<T[], int, V> valueFromArray)
    {
        MergeSort(array, 0, array.Length - 1, valueFromArray);
    }

    public static void MergeSort(T[] array, int left, int right, Func<T[], int, V> valueFromArray)
    {
        if (left < right)
        {
            int middle = left + (right - left) / 2;
            MergeSort(array, left, middle, valueFromArray);
            MergeSort(array, middle + 1, right, valueFromArray);
            Merge(array, left, middle, right, valueFromArray);
        }
    }

    private static void Merge(T[] array, int left, int middle, int right, Func<T[], int, V> valueFromArray)
    {
        T[] sortedArray = new T[right - left + 1];
        int currentIndex = 0;
        int indexLeft = left;
        int indexRight = middle + 1;

        while (indexLeft < middle + 1 && indexRight < right + 1)
        {
            if (IsLessThan(valueFromArray(array, indexLeft), valueFromArray(array, indexRight)))
            {
                sortedArray[currentIndex++] = array[indexLeft++];
            }
            else if (IsLessThan(valueFromArray(array, indexRight), valueFromArray(array, indexLeft)))
            {
                sortedArray[currentIndex++] = array[indexRight++];
            }
            else if (IsEqual(valueFromArray(array, indexRight), valueFromArray(array, indexLeft)))
            {
                sortedArray[currentIndex++] = array[indexLeft++];
            }
            else
            {
                //error
            }
        }

        for (int i = indexLeft; i < middle + 1; ++i)
        {
            sortedArray[currentIndex++] = array[i];
        }

        for (int i = indexRight; i < right + 1; ++i)
        {
            sortedArray[currentIndex++] = array[i];
        }

        for (int i = 0; i < right - left + 1; ++i)
        {
            array[left + i] = sortedArray[i];
        }
    }

    private static bool IsLessThan(V left, V right)
    {
        return left.CompareTo(right) < 0 ? true : false;
    }

    private static bool IsGreaterThan(V left, V right)
    {
        return left.CompareTo(right) > 0 ? true : false;
    }

    private static bool IsEqual(V left, V right)
    {
        return left.CompareTo(right) == 0 ? true : false;
    }

    private static bool IsNotEqual(V left, V right)
    {
        return left.CompareTo(right) != 0 ? true : false;
    }
}
