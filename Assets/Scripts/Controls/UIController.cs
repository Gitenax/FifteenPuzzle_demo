using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField] private Transform _mainCanvas;

    
    public Transform MainCanvas => _mainCanvas;

    
    public PopupWindow CreatePopup()
    {
        var popup = Instantiate(Resources.Load("UI/PopupSettings") as GameObject);
        return popup.GetComponent<PopupWindow>();
    }
    
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
