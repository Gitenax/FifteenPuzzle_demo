using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controls.Buttons
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleEvent : MonoBehaviour
    {
        [SerializeField] private int _value;

        [Inject]
        private void ScriptInit(FifteenGame game)
        {
            var toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener((flag) =>
            {
                if(flag) game.SetGameMode(_value);
            });
        }
    }
}
