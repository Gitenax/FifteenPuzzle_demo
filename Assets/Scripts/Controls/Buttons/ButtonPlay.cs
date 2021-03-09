using System;
using UnityEngine;

namespace Controls.Buttons
{
    public class ButtonPlay : ButtonEvent
    {
        private void Awake()
        {
            _button.onClick.AddListener(_gameManager.StartGame);
        }
    }
}
