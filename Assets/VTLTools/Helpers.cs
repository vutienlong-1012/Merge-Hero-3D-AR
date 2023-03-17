using System.Collections.Generic;
using UnityEngine;

namespace VTLTools
{
    /// <summary>
    /// A static class for general helpful methods
    /// </summary> 

    public class Helpers : MonoBehaviour
    {
        /// <summary>
        /// Destroy all child objects of this transform (Unintentionally evil sounding).
        /// Use it like so:
        /// <code>
        /// transform.DestroyChildren();
        /// </code>
        /// </summary>

        public static List<int> CalcultePyramid(int number)
        {
            List<int> list = new List<int>();
            int index = 0;
            for (int i = 0; i < Mathf.Infinity; i++)
            {
                if (i % 2 == 0)
                {
                    index++;
                    if (number >= index)
                    {
                        number -= index;
                        list.Add(index);
                    }
                    else
                    {
                        if (number == 0)
                            break;
                        else
                        {
                            list.Insert(number * 2, number);
                            number = 0;
                        }
                        break;
                    }
                }
                else
                {
                    if (number >= index)
                    {
                        number -= index;
                        list.Add(index);
                    }
                    else
                    {
                        if (number == 0)
                            break;
                        else
                        {
                            list.Insert(number * 2, number);
                            number = 0;
                        }
                        break;
                    }
                }
            }
            return list;
        }

        public static int Fibonacci(int n)
        {
            if (n <= 0)
                return 1;
            if (n == 1)
                return 1;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        public static int RandomByWeight(float[] probs)
        {

            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Length - 1;
        }

        public static void DestroyAllChilds(GameObject go)
        {
            for (int i = go.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(go.transform.GetChild(i).gameObject);
            }
        }

        public static void RecycleAllChilds(GameObject go)
        {
            for (int i = go.transform.childCount - 1; i >= 0; i--)
            {
                ObjectPool.Recycle(go.transform.GetChild(i).gameObject);
            }
        }

        public static List<T> GetAllChildsComponent<T>(Transform _parent)
        {
            List<T> _l = new List<T>();
            foreach (Transform _child in _parent.GetComponentsInChildren<Transform>())
            {
                if (_child.GetComponent<T>() != null)
                    _l.Add(_child.GetComponent<T>());
            }
            return _l;
        }
    }
}