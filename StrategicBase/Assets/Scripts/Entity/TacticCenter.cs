using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticCenter : Entity
{
    private bool _menuIsOpen;
    private void OnMouseDown()
    {
        _menuIsOpen = !_menuIsOpen;
        if (_menuIsOpen) GameSceneUIManager.Instance.OpenMenu(this);
        else GameSceneUIManager.Instance.CloseMenu(this);
    }
}
