using System;
using System.Collections.Generic;
using SOSXR.Extensions;
using UnityEngine;


namespace SOSXR
{
    public class Trials<T>
    {
        public Trials(List<T> conditions, WebViewConfigData configData)
        {
            if (conditions == null || conditions.Count == 0)
            {
                throw new ArgumentException("Conditions cannot be null or empty.");
            }

            if (configData == null)
            {
                throw new ArgumentNullException(nameof(configData));
            }

            Conditions = conditions;
            ConfigData = configData;

            if (ConfigData.Order == WebViewConfigData.Ordering.Permutation)
            {
                OrderedConditions = Permutations.GetPermutationAtIndex(Conditions, Modulus);
            }
            else if (ConfigData.Order == WebViewConfigData.Ordering.Counterbalanced)
            {
                Modulus = ConfigData.PPN % Conditions.Count;

                if (ConfigData.PlayWayEnum == WebViewConfigData.PlayWay.One)
                {
                    OrderedConditions = new List<T>();
                    var index = Mathf.Clamp(Modulus, 0, Conditions.Count - 1);

                    OrderedConditions.Add(Conditions[index]);
                }
                else
                {
                    OrderedConditions = new List<T>();

                    for (var i = 0; i < Conditions.Count; i++)
                    {
                        OrderedConditions.Add(Conditions[(i + Modulus) % Conditions.Count]);
                    }
                }
            }
            else if (ConfigData.Order == WebViewConfigData.Ordering.InOrder)
            {
                if (ConfigData.PlayWayEnum == WebViewConfigData.PlayWay.One)
                {
                    Modulus = ConfigData.PPN % Conditions.Count;
                    OrderedConditions = new List<T> {Conditions[Modulus]};
                }
                else
                {
                    OrderedConditions = Conditions;
                }
            }
            else if (ConfigData.Order == WebViewConfigData.Ordering.Random)
            {
                if (ConfigData.PlayWayEnum == WebViewConfigData.PlayWay.One)
                {
                    Modulus = ConfigData.PPN % Conditions.Count;
                    OrderedConditions = new List<T> {Conditions[Modulus]};
                }
                else
                {
                    OrderedConditions = Conditions;
                    OrderedConditions.Shuffle();
                }
            }
        }


        public int Modulus { get; }
        public List<T> Conditions { get; }
        public List<T> OrderedConditions { get; }
        private WebViewConfigData ConfigData { get; }
    }
}