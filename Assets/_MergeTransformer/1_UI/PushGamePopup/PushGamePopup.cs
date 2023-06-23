using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class PushGamePopup : PopupBase
    {
        [SerializeField] Image iconGameImage;
        [SerializeField] Text nameGameText;

        [SerializeField] Button downloadGameButton;

        public override void Show(object _data = null, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
            //CC_PushMoreGames.current.CreateIconPush(iconGameImage);
            //nameGameText.text = CC_PushMoreGames.current.nameGame;
        }

        private void OnEnable()
        {
            downloadGameButton.onClick.AddListener(OnClickDownLoadGame);
        }

        private void OnDisable()
        {
            downloadGameButton.onClick.RemoveListener(OnClickDownLoadGame);
        }

        private void OnClickDownLoadGame()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            //CC_PushMoreGames.current.OnDownloadedGame();

            List<string> _list = StaticVariables.ListGamesDownloadedByPush;
            //_list.Add(CC_PushMoreGames.current.nameGame);
            StaticVariables.ListGamesDownloadedByPush = _list;

            UIManager.Instance.HidePopup(this);
        }
    }
}