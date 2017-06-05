using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitComponent : MonoBehaviour
{
    private int _id;
    private bool _isSelected;

    public bool isSelected { get { return _isSelected; } }
    public int id { get { return _id; } }

    void Start()
    {
        UnitSelect.AddUnit(this);
    }

    void OnDestroy()
    {
        // когда юнит уничтожен, сообщаем какой именно и если он выбран, то будет обновлена панель иконок
        UnitSelect.Internal.UnitDestroyed(_id, _isSelected);
    }

    public void Deselect() // вызов, если выделение юнита снято
    {
        _isSelected = false;
    }

    public void Select() // вызов, если юнит был выбран
    {
        _isSelected = true;
    }

    public void DoAction()
    {
        // дополнительная опция, вызывается специальной командой: UnitSelect.DoAction()
        // т.е. все юниты, которые в данный момент выбраны, выполнят данную функцию
        // можно использовать, например, для отправки юнитов в указанную точку
    }

}
