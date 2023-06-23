using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MergeAR;
using TMPro;

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

        public static List<int> CalculatePyramid(int number)
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

        public static void DestroyAllChilds(Transform go)
        {
            for (int i = go.childCount - 1; i >= 0; i--)
            {
#if UNITY_EDITOR
                DestroyImmediate(go.GetChild(i).gameObject);
#else
                Destroy(go.GetChild(i).gameObject);
#endif
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
            foreach (Transform _child in _parent.GetComponentsInChildren<Transform>(true))
            {
                if (_child.GetComponent<T>() != null)
                    _l.Add(_child.GetComponent<T>());
            }
            return _l;
        }

        // method to solve minimum coin exchange problem
        public static List<int> SolveMinimumCoinExchange(List<int> coinValues, int targetValue)
        {
            coinValues.Sort();

            List<int> coinsUsed = new List<int>(); // list to store the coins used
            int remainingValue = targetValue; // remaining value to be reached

            // iterate through the coin values from highest to lowest
            for (int i = coinValues.Count - 1; i >= 0; i--)
            {
                // check if the coin value is less than or equal to the remaining value
                while (coinValues[i] <= remainingValue)
                {
                    coinsUsed.Add(coinValues[i]); // add the coin to the list of coins used
                    remainingValue -= coinValues[i]; // subtract the coin value from the remaining value
                }
            }

            return coinsUsed; // return the list of coins used
        }

        public static void SaveStringToFile(string _filePath, string _textToSave)
        {
            File.WriteAllText("Assets/Resources/" + _filePath + ".txt", _textToSave);
        }

        public static string LoadFileToString(string _filePath)
        {
            TextAsset _textAss = (TextAsset)Resources.Load(_filePath);
            return _textAss.text;
        }

        public static CharacterType DecideCharacterType(CharacterID _iD)
        {
            if (_iD == CharacterID.None)
                return CharacterType.None;
            else
                if (_iD >= CharacterID.EM1 && _iD <= CharacterID.EM10)
                return CharacterType.EnemyMelee;
            else
                   if (_iD >= CharacterID.FM0 && _iD <= CharacterID.HM)
                return CharacterType.FriendlyMelee;
            if (_iD >= CharacterID.ER1 && _iD <= CharacterID.ER10)
                return CharacterType.EnemyRanged;
            else
                return CharacterType.FriendlyRanged;
        }

        public static Vector3 WorldToLocalPointInRectangle(Vector3 _worldPosition, Canvas _canvasParent)
        {
            // Convert the world position to screen space
            Vector2 _screenPosition = Camera.main.WorldToScreenPoint(_worldPosition);

            // Convert the screen position to canvas local position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasParent.transform as RectTransform, _screenPosition, _canvasParent.worldCamera, out Vector2 _localPosition);
            return _localPosition;
        }

        public static string FormatTime(float _time)
        {
            int _minutes = Mathf.FloorToInt(_time / 60f);
            int _seconds = Mathf.FloorToInt(_time % 60f);
            return string.Format("{0:00}:{1:00}", _minutes, _seconds);
        }

        public static Quaternion GetQuaternionLookAt(Vector3 _startPosition, Vector3 _endPosition)
        {
            Vector3 _direction = _endPosition - _startPosition;
            return Quaternion.LookRotation(_direction);
        }
    }
}