using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Controls.Buttons
{
    [RequireComponent(typeof(EventTrigger))]
    public class ButtonAudio : MonoBehaviour
    {
        private EventTrigger _buttonTrigger;
        private AudioSource _buttonAudio;


        [Inject]
        private void ScriptInit(Audio gameAudio)
        {
            _buttonAudio = gameAudio.SfxSoure;
            _buttonAudio.enabled = true;
        }
        
        private void Awake()
        {
            var buttonEntry = new EventTrigger.Entry();
            buttonEntry.eventID = EventTriggerType.PointerDown;
            buttonEntry.callback.AddListener( data => OnPointerDown((PointerEventData)data));
            
            _buttonTrigger = GetComponent<EventTrigger>();
            _buttonTrigger.triggers.Add(buttonEntry);
        }

        private void OnPointerDown(PointerEventData data)
        {
            _buttonAudio.Play();
            Debug.Log($"Button <b><color=orange>{gameObject.name}</color></b> Clicked!");
        }
    }
}
