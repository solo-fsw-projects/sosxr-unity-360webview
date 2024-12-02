using System;
using System.Collections.Generic;
using SOSXR.Extensions;
using UnityEngine;


namespace SOSXR
{
    public class Trials<T>
    {
        public Trials(List<T> conditions, ConfigData configData)
        {
            Conditions = conditions;
            ConfigData = configData;

            LastDigitModulus = ConfigData.PPN % Conditions.Count;
            Debug.Log(LastDigitModulus);

            if (ConfigData.Order == Order.Permutation)
            {
                OrderedConditions = GetPermutationIndex(Conditions, LastDigitModulus);
            }
            else if (ConfigData.Order == Order.Counterbalanced)
            {
                if (ConfigData.PlayWay == PlayWay.One)
                {
                    OrderedConditions = new List<T>();
                    var index = LastDigitModulus;

                    Debug.LogWarning(index);

                    if (index < 0)
                    {
                        index = 0;
                    }
                    else if (index > Conditions.Count)
                    {
                        index = Conditions.Count - 1;
                    }

                    OrderedConditions.Add(Conditions[index]); // This adds thus only 1 video / condition to the list of all possible conditions
                }
                else
                {
                    OrderedConditions = new List<T>();

                    for (var i = 0; i < Conditions.Count; i++)
                    {
                        OrderedConditions.Add(Conditions[(i + LastDigitModulus) % Conditions.Count]);
                    }
                }
            }
            else if (ConfigData.Order == Order.InOrder)
            {
                OrderedConditions = Conditions;
            }
            else if (ConfigData.Order == Order.Random)
            {
                OrderedConditions = Conditions;
                OrderedConditions.Shuffle();
            }
        }


        public int LastDigitModulus { get; }
        private List<T> Conditions { get; }
        public List<T> OrderedConditions { get; }
        private ConfigData ConfigData { get; }


        public List<T> GetPermutationIndex(List<T> items, int index)
        {
            return Permute(items)[index];
        }


        public List<T> GetPermutationIndex(T[] items, int index)
        {
            return Permute(items)[index];
        }


        public List<List<T>> Permute(List<T> items)
        {
            var list = new List<List<T>>();

            return DoPermute(items.ToArray(), 0, items.Count - 1, list);
        }


        public List<List<T>> Permute(T[] items)
        {
            var list = new List<List<T>>();

            return DoPermute(items, 0, items.Length - 1, list);
        }


        /// <summary>
        ///     From: https://chadgolden.com/blog/finding-all-the-permutations-of-an-array-in-c-sharp
        /// </summary>
        private List<List<T>> DoPermute(T[] items, int start, int end, List<List<T>> list)
        {
            if (start == end)
            {
                // We hebben een van de mogelijke n! oplossingen,
                // voeg deze toe aan de lijst.
                list.Add(new List<T>(items));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref items[start], ref items[i]);
                    DoPermute(items, start + 1, end, list);
                    Swap(ref items[start], ref items[i]);
                }
            }

            return list;
        }


        private void Swap(ref T a, ref T b)
        {
            (a, b) = (b, a);
        }
    }
}


[Serializable]
public class PermutationContainer<T>
{
    public List<T> Permutations; // This holds the actual permutation
}