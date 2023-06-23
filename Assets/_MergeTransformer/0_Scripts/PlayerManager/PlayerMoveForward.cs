using UnityEngine;

namespace MergeAR
{
    public class PlayerMoveForward : MonoBehaviour
    {
        public bool isAllowToMoveForward;
        public float moveSpeed;
        float defaultMoveSpeed;

        private void Start()
        {
            defaultMoveSpeed = moveSpeed;
        }

        public void ResetSpeed()
        {
            moveSpeed = defaultMoveSpeed;
        }

        public void SpeedUp()
        {
            moveSpeed = defaultMoveSpeed * 2;
        }


        private void Update()
        {
            if (!isAllowToMoveForward)
                return;
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
        }

    }
}