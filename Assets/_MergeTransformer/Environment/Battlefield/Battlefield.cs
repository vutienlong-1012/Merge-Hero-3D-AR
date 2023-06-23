using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class Battlefield : Singleton<Battlefield>
    {
        [SerializeField] bool isTriggered = false;
        public Transform EngageCameraPoint;
        public Transform FightCameraPoint;
        public Transform VictoryCameraPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (isTriggered)
                return;
            if (other.CompareTag(StringsSafeAccess.TAG_FRIENDLY_CHARACTER))
            {
                isTriggered = true;
                //GameManager.Instance.State = GameState.EngageBattlefield;
            }
        }

        public void ResetBattlefield()
        {
            isTriggered = false;
        }

        public void SetBattlefieldPosition(Vector3 _pos)
        {
            this.transform.position = _pos;
        }
    }
}