using Sirenix.OdinInspector;
using System.Collections.Generic;
using VTLTools;

namespace MergeAR
{
    public class CharacterDataManager : Singleton<CharacterDataManager>
    {
        [ShowInInspector] public List<CharacterData> characterDatas = new();

        public CharacterData GetCharacterDataByID(CharacterID _id)
        {
            CharacterData _char = null;
            foreach (var _item in characterDatas)
            {
                if (_id == _item.iD)
                {
                    _char = _item;
                    break;
                }
            }
            return _char;
        }
    }
}