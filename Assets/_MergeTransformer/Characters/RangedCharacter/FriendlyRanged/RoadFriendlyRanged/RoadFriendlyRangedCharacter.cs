using Sirenix.OdinInspector;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class RoadFriendlyRangedCharacter : FriendlyRangedCharacter, IInteractable
    {
        [SerializeField, ReadOnly] bool isInFormation = false;
        [SerializeField, TabGroup("FX")] AudioClip interactAudioClip;
        public void Interact(Character _char)
        {
            if (isInFormation)
                return;
            if (_char.gameObject.CompareTag(StringsSafeAccess.TAG_FRIENDLY_CHARACTER))
            {
                SoundSystem.Instance.PlaySoundOneShot(AudioSource, interactAudioClip);
                isInFormation = true;
                PlayerManager.Instance.runnerCharactersGroup.AddCharacterOnRunWay(this);
                this.RunToBattlefield();
            }
        }    
    }
}