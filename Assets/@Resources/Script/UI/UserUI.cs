using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UserUI : MonoBehaviour
{
    public UI_ColorPicker UI_ColorPicker;
    public List<UI_ButtonController> Buttons;
    public Composite composite;
    Type[] types = 
    {
        typeof(UI_ColorPicker),
        typeof(UI_ButtonController),
        typeof(UI_ImageController),
    };
    void Start()
    {
        composite = findChild(transform, null);

        composite.Operation<UI_ButtonController>(new Action<UI_ButtonController>(AddButtonEvent));
    }
    void AddButtonEvent(UI_ButtonController _button)
    {
        ICommand command = null;
        if (_button == null)
        {
            Debug.Log("버튼이 null");
        }

        switch (_button.ClickEvent)
        {
            case UI_ButtonController.OnClickEvent.ChangeMode:
                command = new ChangeMode(_button.num);
                break;
            case UI_ButtonController.OnClickEvent.ChangeMaterial:
                command = new ChangeMaterial(_button.num);
                break;
            case UI_ButtonController.OnClickEvent.Save:
                command = new Save();
                break;
            case UI_ButtonController.OnClickEvent.DelateAll:
                command = new DeleteAll();
                break;
            case UI_ButtonController.OnClickEvent.ChangeColor:
                Color color =  _button.gameObject.GetComponent<Image>().color;
                command = new ChangeColor(ref color);
                break;
            default:
                break;
        }
        _button.AddOnClickEvent(new Action(command.Execute));
    }

    public void ChagneColorHistory(List<Color> colors)
    {
        composite.Operation<UI_ImageController>(new Action<UI_ImageController>((ui_imageController) =>{

        }));
    }
    Composite findChild(Transform transform, Composite _composite)
    {
        if (transform == null)
            return null;

        Composite compos = _composite;
        if (compos == null)
            compos = new Composite();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.childCount > 1)
            {
                Composite childComopsite = new Composite();
                compos.Elements.Add(childComopsite);
                findChild(child, childComopsite);
            }
            else
            {
                for (int j = 0; j < types.Length; j++)
                {
                    Component component = child.GetComponent(types[j]);

                    if (component == null)
                        continue;

                    Type type = typeof(Leaf<>);
                    Debug.Log($"{component.GetType()}, {component}");
                    object leaf = Activator.CreateInstance(type.MakeGenericType(component.GetType()), new object[] { component });
                    compos.Add(leaf as Element);
                }
            }
        }
        return compos;
    }
    
}

public interface Element
{
    public void Operation<T>(Action<T> action) where T : Component;
}

public class Leaf<T> : Element where T : Component
{
    public T Compo;
    public void Operation<J>(Action<J> action) where J : Component
    {
        if (Compo is Component)
        {
            action.Invoke(Compo as J);
        }
        else
        {
            Debug.Log("타입이 부적절합니다.");
        }
    }
    public Leaf(T component)
    {
        this.Compo = component;
    }
}
public class Composite : Element
{
    public List<Element> Elements = new List<Element>();
    public void Operation<T>(Action<T> action) where T : Component
    {
        foreach (Element element in Elements)
        {
            element.Operation(action);
        }
    }
    public void Add(Element element)
    {
        if (element == null)
        {
            Debug.Log("element is null");
            return;
        }
        Elements.Add(element);
    }
    public void Remove(Element element)
    {
        if (element == null)
        {
            Debug.Log("element is null");
            return;
        }
        Elements.Remove(element);
    }
}