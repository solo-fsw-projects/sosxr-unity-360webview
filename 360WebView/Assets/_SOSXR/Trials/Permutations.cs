using System.Collections.Generic;


public static class Permutations
{
    public static List<T> GetPermutationAtIndex<T>(List<T> items, int index)
    {
        return GeneratePermutations(items)[index];
    }


    public static List<T> GetPermutationAtIndex<T>(T[] items, int index)
    {
        return GeneratePermutations(items)[index];
    }


    public static List<List<T>> GeneratePermutations<T>(List<T> items)
    {
        var list = new List<List<T>>();

        return GeneratePermutationsRecursively(items.ToArray(), 0, items.Count - 1, list);
    }


    public static List<List<T>> GeneratePermutations<T>(T[] items)
    {
        var list = new List<List<T>>();

        return GeneratePermutationsRecursively(items, 0, items.Length - 1, list);
    }


    private static List<List<T>> GeneratePermutationsRecursively<T>(T[] items, int start, int end, List<List<T>> list)
    {
        if (start == end)
        {
            list.Add(new List<T>(items)); // Add one of the n! solutions to the list.
        }
        else
        {
            for (var i = start; i <= end; i++)
            {
                SwapElements(ref items[start], ref items[i]);
                GeneratePermutationsRecursively(items, start + 1, end, list);
                SwapElements(ref items[start], ref items[i]);
            }
        }

        return list;
    }


    private static void SwapElements<T>(ref T a, ref T b)
    {
        (a, b) = (b, a);
    }
}