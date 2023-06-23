using Abu;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class TutorialManager : Singleton<TutorialManager>
    {
        [SerializeField] RectTransform guideHand;
        [SerializeField] TutorialFadeImage tutorialFadeImage;
        [SerializeField, ReadOnly] bool isShowingTutorial;

        [SerializeField] Canvas UICanvas;

        public void CheckTutorialFirstBuy()
        {
            if (isShowingTutorial)
                return;

            if (StaticVariables.CurrentTutorialPhase != TutorialPhase.FirstBuyCharacter)
                return;

            isShowingTutorial = true;
            tutorialFadeImage.gameObject.SetActive(true);
            tutorialFadeImage.StartFadeInToHightLight();
        }

        public void HideTutorialFirstBuy()
        {
            isShowingTutorial = false;
            tutorialFadeImage.StartFadeOutToHightLight(() =>
            {
                tutorialFadeImage.gameObject.SetActive(false);
            });
        }

        public void CheckTutorialFirstMerge(Transform _startGrid, Transform _endGrid)
        {
            if (isShowingTutorial)
                return;

            if (StaticVariables.CurrentTutorialPhase != TutorialPhase.FirstMergeCharacter)
                return;

            isShowingTutorial = true;
            guideHand.gameObject.SetActive(true);

            Vector3 _startPos = Helpers.WorldToLocalPointInRectangle(_startGrid.position, UICanvas);
            Vector3 _endPos = Helpers.WorldToLocalPointInRectangle(_endGrid.position, UICanvas);

            guideHand.localPosition = _startPos;
            guideHand.DOLocalMove(_endPos, 2f).SetLoops(-1, LoopType.Restart);
        }

        public void HideGuildHandTutorial()
        {
            if (!isShowingTutorial)
                return;

            guideHand.transform.DOKill();
            guideHand.gameObject.SetActive(false);
            isShowingTutorial = false;
        }

        public void CheckTutorialFirstMove(Transform _startGrid)
        {
            if (isShowingTutorial)
                return;

            if (StaticVariables.CurrentTutorialPhase != TutorialPhase.FirstMoveCharacter)
                return;

            Grid _endGrid = GridManager.instance.GetEmptyFriendlyGrid();
            if (_endGrid == null)
            {
                HideGuildHandTutorial();
                return;
            }

            isShowingTutorial = true;
            guideHand.gameObject.SetActive(true);

            Vector3 _startPos = Helpers.WorldToLocalPointInRectangle(_startGrid.position, UICanvas);
            Vector3 _endPos = Helpers.WorldToLocalPointInRectangle(_endGrid.transform.position, UICanvas);

            guideHand.localPosition = _startPos;
            guideHand.DOLocalMove(_endPos, 2f).SetLoops(-1, LoopType.Restart);
        }

    }
}