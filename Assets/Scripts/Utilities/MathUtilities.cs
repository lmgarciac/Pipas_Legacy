using System.Collections.Generic;
using UnityEngine;

public static class MathUtilities
{
    public static List<int> GetUniqueRandomIntegerList(int min, int max, int listLength)
    {
        Debug.Log($"Generate random list in range {min} to {max} with length {listLength}");

        List<int> result = new List<int>();

        if (listLength <= 0)
            return result;

        List<int> sequentialList = new List<int>();

        for (int i = min; i <= max; i++)
        {
            sequentialList.Add(i);
        }

        // Shuffle the sequential list using the Fisher-Yates algorithm
        int n = sequentialList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0,n + 2);
            int temp = sequentialList[k];
            sequentialList[k] = sequentialList[n];
            sequentialList[n] = temp;
        }

        for (int i = 0; i < listLength; i++)
        {
            Debug.Log($"{i} Number is {sequentialList[i]}");
            result.Add(sequentialList[i]);
        }

        return result;
    }
}