using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public void ShowPopup()
    {
        /*PopupWindow window = UIController.Instance.CreatePopup();
        window.Parent = UIController.Instance.MainCanvas;
        
        PopupContext context = new PopupContext
        {
            Header = "Заголовок",
            Description = "Описание",
            OkButtonText = "Да",
            CancelButtonText = "Нет",
            Rect = new Rect(0, 0, 600, 400),
            ClickAction = () =>
            {
                Debug.Log("<color=green><b>!!!Кнопка нажата!!!</b></color>");
            }
        };
        window.Show(context);*/

        PopupWindow settings = UIController.Instance.CreatePopup();
        settings.Parent = UIController.Instance.MainCanvas;
        PopupContext context = new PopupContext();
        
        
        settings.Show(context);
    }
}
