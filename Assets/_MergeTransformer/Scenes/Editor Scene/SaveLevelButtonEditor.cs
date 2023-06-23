using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.Editor
{
    public class SaveLevelButtonEditor : MonoBehaviour
    {
        [SerializeField] Button button;

        private void OnEnable()
        {
            button.onClick.AddListener(OnClickSaveLevelButtonEditor);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClickSaveLevelButtonEditor);
        }

        void OnClickSaveLevelButtonEditor()
        {
            CharacterDataManager.Instance.ModifyEnemyGridData(EditorEnemyManager.Instance.currentEditingLevel-1);
        }

    }
}