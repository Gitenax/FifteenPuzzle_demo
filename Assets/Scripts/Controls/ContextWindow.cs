using Controls;
using UnityEngine;
using UnityEngine.UI;

public class ContextWindow : MonoBehaviour
{
    private RectTransform _rectTransform;
    
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;

    [SerializeField] private Text _header;
    [SerializeField] private Text _description;
    [SerializeField] private Text _okButtonLabel;
    [SerializeField] private Text _cancelButtonLabel;
    
    [SerializeField] private PopupContext _context;
    
    public void Initialize(PopupContext context)
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.sizeDelta = new Vector2(context.Rect.width, context.Rect.height);

        _header.text = context.Header;
        _description.text = context.Description;
        _okButtonLabel.text = context.OkButtonText;
        _cancelButtonLabel.text = context.CancelButtonText;
        
        _okButton.onClick.AddListener(() =>
        {
            context.ClickAction.Invoke();
            Destroy(transform.parent.gameObject);
        });
        
        _cancelButton.onClick.AddListener(() =>
        {
            Destroy(transform.parent.gameObject);
        });
    }
}
