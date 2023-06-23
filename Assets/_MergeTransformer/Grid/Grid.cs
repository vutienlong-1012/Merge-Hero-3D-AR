using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MergeAR
{
    [Serializable]
    public class Grid : MonoBehaviour
    {

        public bool IsHaveCharacter => CurrentCharacter != null;

        [ReadOnly] public int col;
        [ReadOnly] public int cow;

        [SerializeField, ReadOnly] protected Character CurrentCharacter;



        protected virtual void OnEnable()
        {
            EventDispatcher.Instance.AddListener(EventName.OnClearGrid, ClearGrid);
        }

        protected virtual void OnDisable()
        {
            EventDispatcher.Instance.RemoveListener(EventName.OnClearGrid, ClearGrid);
        }

        public void SetCurrentCharacter(Character _char)
        {
            CurrentCharacter = _char;
        }

        public Character GetCurrentCharacter()
        {
            return CurrentCharacter;
        }

        public void Init(Transform _parent, Vector3 _localPos, Quaternion _quaternion, int _col, int _row)
        {
            this.transform.parent = _parent;
            this.transform.localPosition = _localPos;
            this.transform.rotation = _quaternion;
            this.name = this.name + _col + _row;
            col = _col;
            cow = _row;
        }

        public void ClearGrid(EventName _key = EventName.NONE, object _data = null)
        {
            if (CurrentCharacter == null)
                return;

            Destroy(CurrentCharacter.gameObject);
            CurrentCharacter = null;
            //isHaveChar = false;
        }
    }
}
