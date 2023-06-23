using UnityEngine;

namespace MergeAR
{
    public class Cursor : MonoBehaviour
    {
        public void SetScale(Vector3 _scale)
        {
            this.transform.localScale = _scale;
        }
    }
}