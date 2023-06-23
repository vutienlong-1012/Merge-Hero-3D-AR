using UnityEngine;
using Sirenix.OdinInspector;

namespace MergeAR
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] RunnerCharacterGroup runnersGroup;
        [SerializeField] GameObject hitPlane;

        [SerializeField, ReadOnly] Vector3 startTouchPosition;
        [SerializeField, ReadOnly] Vector3 endTouchPosition;

        [SerializeField] float boundValueLeft;
        [SerializeField] float boundValueRight;
        [SerializeField] float boundSize;

        [SerializeField] float borderValue;

        [SerializeField, ReadOnly] float currentDirect;

        public bool availableToMove;

        int RunnerGroupChildCount => runnersGroup.runnerList.Count;

        private void Start()
        {

        }

        void Update()
        {
            if (!availableToMove)
                return;
            MoveLeftRight();
        }

        RaycastHit _raycastHit;
        Ray _ray;
        float _distanceToMove;
        Vector3 _newPos;
        void MoveLeftRight()
        {
            _ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(_ray, out _raycastHit, Mathf.Infinity, 1 << hitPlane.layer))
                {
                    startTouchPosition = new Vector3(_raycastHit.collider.transform.InverseTransformPoint(_raycastHit.point).x, 0, 0);
                    endTouchPosition = new Vector3(_raycastHit.collider.transform.InverseTransformPoint(_raycastHit.point).x, 0, 0);
                }

            }
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(_ray, out _raycastHit, Mathf.Infinity, 1 << hitPlane.layer))
                {
                    startTouchPosition = endTouchPosition;
                    currentDirect = Mathf.Sign((_raycastHit.collider.transform.InverseTransformPoint(_raycastHit.point).x - endTouchPosition.x));  //Hướng di chuyển
                    endTouchPosition = new Vector3(_raycastHit.collider.transform.InverseTransformPoint(_raycastHit.point).x, 0, 0);  //vị trí cuối frame mới = vị trí hit panel
                    _distanceToMove = Vector3.Distance(startTouchPosition, endTouchPosition) * currentDirect; //tính khoảng cách và hướng di chuyển


                    if (runnersGroup.transform.localPosition.x >= -borderValue + boundSize && runnersGroup.transform.localPosition.x <= borderValue - boundSize)  //nếu player trong border
                    {
                        _newPos = new Vector3(Mathf.Clamp(runnersGroup.transform.localPosition.x + _distanceToMove, -borderValue + boundSize, borderValue - boundSize), 0, 0);
                    }
                    else
                    {
                        if (runnersGroup.transform.localPosition.x > 0)  //nếu player ngoài border bên phải
                            _newPos = new Vector3(Mathf.Clamp(runnersGroup.transform.localPosition.x + _distanceToMove, -runnersGroup.transform.localPosition.x, runnersGroup.transform.localPosition.x), 0, 0);
                        else                                               //nếu player ngoài border bên trái
                            _newPos = new Vector3(Mathf.Clamp(runnersGroup.transform.localPosition.x + _distanceToMove, runnersGroup.transform.localPosition.x, -runnersGroup.transform.localPosition.x), 0, 0);
                    }

                    runnersGroup.transform.localPosition = _newPos;

                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                startTouchPosition = Vector3.zero;
                endTouchPosition = Vector3.zero;
            }

        }

        [Button]
        public void CalculateBound(/*float _timeDelay*/)
        {
            //float[] _listPos = new float[RunnerGroupChildCount];
            //for (int i = 0; i < RunnerGroupChildCount; i++)
            //{
            //    _listPos[i] = runnersGroup.transform.GetChild(i).localPosition.x;
            //}
            //boundValueLeft = Mathf.Min(_listPos);
            //boundValueRight = Mathf.Max(_listPos);
            //boundSize = (boundValueRight - boundValueLeft) / 2;

            float _children = runnersGroup.runnerList.Count;

            if (_children <= 1)
            {
                boundValueLeft = 0;
                boundValueRight = 0;
                boundSize = 0;
            }


            if (_children > 1 && _children <= 10)
            {
                if (_children % 2 == 0)
                {
                    boundValueLeft = -_children / 2;
                    boundValueRight = _children / 2 - 1;
                    boundSize = (_children - 1) * (runnersGroup.destinyPopulation / 2);
                }
                else
                {
                    boundValueLeft = -_children / 2 - 1;
                    boundValueRight = _children / 2;
                    boundSize = (_children - 1) * (runnersGroup.destinyPopulation / 2);
                }
            }

            if (_children > 10)
            {
                boundValueLeft = -borderValue - (runnersGroup.destinyPopulation / 2);
                boundValueRight = borderValue - (runnersGroup.destinyPopulation / 2);
                boundSize = borderValue - (runnersGroup.destinyPopulation / 2);
            }


            runnersGroup.CenterParentPosition();
        }

        public void SetActiveHitPlane(bool _isActive)
        {
            hitPlane.SetActive(_isActive);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(borderValue, 0, this.transform.position.z), new Vector3(borderValue, 0, this.transform.position.z + 9));
            Gizmos.DrawLine(new Vector3(-borderValue, 0, this.transform.position.z), new Vector3(-borderValue, 0, this.transform.position.z + 9));
            // Gizmos.color = Color.green;
            //  Gizmos.DrawLine(new Vector3(boundValueLeft, 0, this.transform.position.z), new Vector3(boundValueLeft, 0, this.transform.position.z + 9));
            //Gizmos.DrawLine(new Vector3(boundValueRight, 0, this.transform.position.z), new Vector3(boundValueRight, 0, this.transform.position.z + 9));
        }
    }
}