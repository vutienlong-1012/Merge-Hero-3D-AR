using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.Editor
{
    public class BuyCharacterButtonEditor : MonoBehaviour
    {
        [SerializeField] CharacterID characterID;

        Button button;

        [ShowInInspector, ReadOnly]
        Button Button
        {
            get
            {
                if (button != null)
                {
                    return button;
                }
                else
                {
                    button = this.GetComponent<Button>();
                    return button;
                }
            }
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClickBuyCharacter);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClickBuyCharacter);
        }

        private void OnClickBuyCharacter()
        {
            CharacterDataManager.Instance.SpawnCharacterInGrid(ControlManagerEditor.Instance.CurrentEnemyGrid, characterID);
            //CharacterDataManager.Instance.SaveFriendlyGrids();
            //EventDispatcher.Instance.Dispatch(EventName.OnBuyCharacter, characterID);
        }
    }
}