using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitSelect : MonoBehaviour
{

    [SerializeField] private string iconsFolder = "UnitIcons"; // папка в Resources, где лежат иконки юнитов
    [SerializeField] private int maxUnits = 100; // сколько всего может быть юнитов, под контролем игрока
    [SerializeField] private RectTransform unitIcon; // шаблон иконки
    [SerializeField] private RectTransform window; // трансформ, для построения сетки иконок
    [SerializeField] private Image mainRect; // чем будем рисовать рамку

#if UNITY_EDITOR
    [SerializeField] private int width = 5, height = 5; // сколько создать иконок по ширине и высоте
    [SerializeField] private float offset = 10; // расстояние между ними
#endif

    private Rect rect;
    private bool canDraw;
    private Vector2 startPos, endPos;
    private Color original, clear, curColor;
    private Sprite[] unitImage;
    private static UnitComponent[] _unit;
    private static List<UnitComponent> _unitSelected;
    private static int _unitCount;
    private static UnitSelect _internal;

    public static UnitSelect Internal
    {
        get { return _internal; }
    }

    public static void DoAction() // запрос на выполнение какого-либо действия, если есть выбранные юниты
    {
        foreach (UnitComponent target in _unitSelected)
        {
            if (target) target.DoAction();
        }
    }

    public static void AddUnit(UnitComponent comp) // добавить нового юнита
    {
        for (int i = 0; i < _unit.Length; i++)
        {
            if (_unit[i] == null)
            {
                _unit[i] = comp;
                _unitCount++;
                break;
            }
        }
    }

    public static int currentUnitCount // текущее количество юнитов
    {
        get { return _unitCount; }
    }

    //public void GetCurrentUnit(int id) // выбор типа юнитов из окна иконок
    //{
    //    foreach (UnitComponent target in _unitSelected)
    //    {
    //        if (target && target.id != id) target.Deselect();
    //    }

    //    foreach (UnitIcon target in icons)
    //    {
    //        if (target.id != id)
    //        {
    //            target.icon.sprite = null;
    //            target.gameObject.SetActive(false);
    //        }
    //    }
    //}

    public void UnitDestroyed(int id, bool isSelected)
    {
        //for (int i = 0; i < icons.Length; i++)
        //{
        //    if (icons[i] && icons[i].id == id && isSelected)
        //    {
        //        switch (icons[i].counter) // настройка иконок
        //        {
        //            case 1:
        //                icons[i].id = 0;
        //                icons[i].icon.sprite = null;
        //                icons[i].gameObject.SetActive(false);
        //                break;
        //            case 2:
        //                icons[i].counter--;
        //                icons[i].iconCount.SetActive(false);
        //                break;
        //            default:
        //                icons[i].counter--;
        //                icons[i].iconCountText.text = icons[i].counter.ToString();
        //                break;
        //        }

        //        break;
        //    }
        //}

        _unitCount--;
    }

    void Awake()
    {
        _internal = this;
       // unitImage = Resources.LoadAll<Sprite>(iconsFolder);
        _unitCount = 0;
        _unit = new UnitComponent[maxUnits];
        _unitSelected = new List<UnitComponent>();
        original = mainRect.color;
        clear = original;
        clear.a = 0;
        curColor = clear;
        mainRect.color = clear;
    }

    void Draw() // рисуем рамку
    {
        endPos = Input.mousePosition;
        if (startPos == endPos || !canDraw) return;

        curColor = original;

        rect = new Rect(Mathf.Min(endPos.x, startPos.x),
            Screen.height - Mathf.Max(endPos.y, startPos.y),
            Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
            Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
        );

        mainRect.rectTransform.sizeDelta = new Vector2(rect.width, rect.height);

        mainRect.rectTransform.anchoredPosition = new Vector2(rect.x + mainRect.rectTransform.sizeDelta.x / 2,
            Mathf.Max(endPos.y, startPos.y) - mainRect.rectTransform.sizeDelta.y / 2);
    }

    void Deselect() // отмена текущего выбора
    {
        foreach (UnitComponent target in _unitSelected)
        {
            if (target) target.Deselect();
        }

        //for (int i = 0; i < icons.Length; i++)
        //{
        //    icons[i].icon.sprite = null;
        //    icons[i].gameObject.SetActive(false);
        //}
    }

    void SetSelected() // поиск юнитов в рамке и если они там есть, добавляем их
    {
        foreach (UnitComponent target in _unit)
        {
            if (target)
            {
                Vector2 pos = Camera.main.WorldToScreenPoint(target.transform.position);
                pos = new Vector2(pos.x, Screen.height - pos.y);

                if (rect.Contains(pos))
                {
                    target.Select();
                   // SetIcon(target.id, target.iconName);
                    _unitSelected.Add(target);
                }
            }
        }
    }

    Sprite GetSprite(string iconName)
    {
        Sprite result = null;
        foreach (Sprite sp in unitImage)
        {
            if (sp.name == iconName)
            {
                result = sp;
                break;
            }
        }
        return result;
    }

    //void SetIcon(int id, string iconName) // добавление иконок в панель
    //{
    //    for (int i = 0; i < icons.Length; i++)
    //    {
    //        if (icons[i].icon.sprite == null)
    //        {
    //            icons[i].icon.sprite = GetSprite(iconName);
    //            icons[i].iconCount.SetActive(false);
    //            icons[i].id = id;
    //            icons[i].counter = 1;
    //            icons[i].gameObject.SetActive(true);
    //            break;
    //        }
    //        else if (icons[i].id == id)
    //        {
    //            icons[i].counter++;
    //            icons[i].iconCount.SetActive(true);
    //            icons[i].iconCountText.text = icons[i].counter.ToString();
    //            break;
    //        }
    //    }
    //}

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !InWindow() && !HitUnit())
        {
            Deselect();
            rect = new Rect();
            _unitSelected = new List<UnitComponent>();
            startPos = Input.mousePosition;
            canDraw = true;
        }

        if (Input.GetMouseButtonUp(0) && canDraw)
        {
            curColor = clear;
            canDraw = false;
            SetSelected();
        }

        Draw();

        mainRect.color = Color.Lerp(mainRect.color, curColor, 10 * Time.deltaTime);
    }

    bool InWindow() // находится ли курсор в окне иконок или нет
    {
        Vector2 position = Input.mousePosition;
        Vector3[] worldCorners = new Vector3[4];
        window.GetWorldCorners(worldCorners);

        if (position.x >= worldCorners[0].x && position.x < worldCorners[2].x
            && position.y >= worldCorners[0].y && position.y < worldCorners[2].y)
        {
            return true;
        }

        return false;
    }

    bool HitUnit() // клик по юниту или нет
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            UnitComponent unit = hit.collider.GetComponent<UnitComponent>();
            if (unit)
            {
                Deselect();
                rect = new Rect();
                _unitSelected = new List<UnitComponent>();
                unit.Select();
               // SetIcon(unit.id, unit.iconName);
                _unitSelected.Add(unit);
                return true;
            }
        }

        return false;
    }

#if UNITY_EDITOR
    //public void BuildGrid() // инструмент для создания окна иконок на основе шаблона
    //{
    //    foreach (UnitIcon icon in icons)
    //    {
    //        if (icon) DestroyImmediate(icon.gameObject);
    //    }
    //    float sizeX = unitIcon.sizeDelta.x + offset;
    //    float sizeY = unitIcon.sizeDelta.y + offset;
    //    window.sizeDelta = new Vector2(sizeX * width, sizeY * height);
    //    float posX = -sizeX * width / 2 - sizeX / 2;
    //    float posY = sizeY * height / 2 + sizeY / 2;
    //    float Xreset = posX;
    //    int i = 0;
    //    icons = new UnitIcon[width * height];
    //    for (int y = 0; y < height; y++)
    //    {
    //        posY -= sizeY;
    //        for (int x = 0; x < width; x++)
    //        {
    //            posX += sizeX;
    //            RectTransform tr = Instantiate(unitIcon) as RectTransform;
    //            tr.SetParent(window);
    //            tr.localScale = Vector3.one;
    //            tr.anchoredPosition = new Vector2(posX, posY);
    //            tr.name = "Icon-" + i;
    //            icons[i] = tr.GetComponent<UnitIcon>();
    //            i++;
    //        }
    //        posX = Xreset;
    //    }
    //}
#endif
}

