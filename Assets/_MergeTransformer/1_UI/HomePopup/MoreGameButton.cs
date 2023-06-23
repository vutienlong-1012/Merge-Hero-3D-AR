using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI.HomePopup
{
    public class MoreGameButton : MonoBehaviour
    {
        Button button;
        [ShowInInspector]
        Button ThisButton
        {
            get
            {
                if (button == null)
                    button = GetComponent<Button>();
                return button;
            }
        }

        Image image;
        [ShowInInspector]
        Image ThisImage
        {
            get
            {
                if (image == null)
                    image = GetComponent<Image>();
                return image;
            }
        }

        private void Start()
        {
            //CC_PushMoreGames.current.RandomIconMoregame(ThisImage);
        }

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(OnClickMoreGame);
        }

        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(OnClickMoreGame);
        }

        private void OnClickMoreGame()
        {
            //CC_PushMoreGames.current.OnClickMoreGame();
        }
    }
}