using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.Editor
{
    public class DropdownChooseLevelEnemy : MonoBehaviour
    {
        [SerializeField] Dropdown dropdown;
        List<string> enemyData = new List<string>();

        private void OnEnable()
        {
            dropdown.onValueChanged.AddListener(ChangeLevel);
        }
        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(ChangeLevel);
        }

        private void Start()
        {
            for (int i = 0; i < CharacterDataManager.Instance.AllEnemyGridIds.Count; i++)
            {
                enemyData.Add("level " + (i + 1));
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(enemyData);
        }

        private void ChangeLevel(int arg0)
        {
            CharacterDataManager.Instance.ClearCharacter();
            CharacterDataManager.Instance.LoadEnemyGrids(arg0 + 1);
            EditorEnemyManager.Instance.currentEditingLevel = arg0 + 1;
            Debug.Log("Loaded Level!!!");
        }
    }
}