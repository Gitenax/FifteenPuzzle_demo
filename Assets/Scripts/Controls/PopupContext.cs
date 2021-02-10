using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupContext
{
    private Rect _rect;
    
    private string _headerText;
    private string _descriptionText;
    private string _okButtonText;
    private string _cancelButtonText;
    
    private Action _clickAction;
    
    
    public Rect Rect
    {
        get => _rect;
        set => _rect = value;
    }
    
    public string Header
    {
        get => _headerText;
        set => _headerText = value;
    }

    public string Description
    {
        get => _descriptionText;
        set => _descriptionText = value;
    }

    public string OkButtonText
    {
        get => _okButtonText;
        set => _okButtonText = value;
    }
    
    public string CancelButtonText
    {
        get => _cancelButtonText;
        set => _cancelButtonText = value;
    }

    public Action ClickAction
    {
        get => _clickAction;
        set => _clickAction = value;
    }
}
