using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controls.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonEvent : MonoBehaviour
    {
        protected FifteenGame _gameManager;
        protected Button _button;

        [Inject]
        private void ScriptInit(FifteenGame game)
        {
            _gameManager = game;
            _button = GetComponent<Button>();
        }
    }
}
