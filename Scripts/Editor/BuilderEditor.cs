#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (BuilderSetup))]

public class BuilderEditor : Editor
{

    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector ();
        BuilderSetup t = (BuilderSetup)target;
        GUILayout.Label ("Обновить массив префабов:", EditorStyles.boldLabel);
        if (GUILayout.Button ("Load Resources")) t.LoadResources ();
    }

    LayerMask LayerMaskField (LayerMask layerMask) // вывод маски
    {
        List<string> layers = new List<string>();
        List<int> layerNumbers = new List<int>();

        for (int i = 0; i < 32; i++)
        {
            string layerName = LayerMask.LayerToName(i);

            if (layerName != "")
            {
                layers.Add (layerName);
                layerNumbers.Add (i);
            }
        }

        int maskWithoutEmpty = 0;

        for (int i = 0; i < layerNumbers.Count; i++)
        {
            if (((1 << layerNumbers [i]) & layerMask.value) > 0) maskWithoutEmpty |= (1 << i);
        }

        maskWithoutEmpty = EditorGUILayout.MaskField (maskWithoutEmpty, layers.ToArray ());

        int mask = 0;

        for (int i = 0; i < layerNumbers.Count; i++)
        {
            if ((maskWithoutEmpty & (1 << i)) > 0) mask |= (1 << layerNumbers [i]);
        }

        layerMask.value = mask;

        return layerMask;
    }

    void OnSceneGUI ()
    {
        BuilderSetup t = (BuilderSetup)target;

        HandleUtility.AddDefaultControl (GUIUtility.GetControlID (FocusType.Passive)); // отмена выбора объекта ЛКМ в окне редактора

        string showButtonText = (t.showButton) ? "Скрыть меню" : "Развернуть меню";
        string toggleText = (t.project2D) ? "Raycast в двухмерном режиме" : "Трехмерный режим Raycast-а";

        if (Event.current.button == 0 && Event.current.type == EventType.mouseDown)
        {
            if (t.project2D)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.current.ScreenToWorldPoint(new Vector2(Event.current.mousePosition.x,
                    SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y)), Vector2.zero, Mathf.Infinity, t.layerMask);

                if (hit.collider != null && hit.collider.tag.CompareTo (t.tagField) == 0)
                {
                    t.InstantiatePrefab (hit.point);
                }
            }
            else
            {
                RaycastHit hit;
                Ray ray = Camera.current.ScreenPointToRay(new Vector2(Event.current.mousePosition.x,
                    SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y));

                if (Physics.Raycast (ray, out hit, Mathf.Infinity, t.layerMask) && hit.collider.tag.CompareTo (t.tagField) == 0)
                {
                    t.InstantiatePrefab (hit.point);
                }
            }
        }

        Handles.BeginGUI ();
        GUILayout.BeginArea (new Rect (t.position.x, t.position.y, t.width, t.height)); // вырезаем область в окне сцены

        if (GUILayout.Button (showButtonText)) t.showButton = !t.showButton;

        if (t.showButton)
        {
            GUILayout.TextArea ("Справка:\n" +
                "Для установки выбранного префаба, ставим курсор в нужное место, затем ЛКМ");

            GUILayout.BeginHorizontal ();
            GUILayout.TextField ("Выбор префаба: ");
            t.index = EditorGUILayout.Popup (t.index, t.prefabsNames);
            GUILayout.EndHorizontal ();

            GUILayout.Box ("Опции рейкаста:", GUILayout.ExpandWidth (true));

            GUILayout.BeginHorizontal ();
            GUILayout.TextField ("Фильтр по тегу: ");
            if (t.tagField.Trim () == string.Empty) t.tagField = "Untagged";
            t.tagField = EditorGUILayout.TagField (t.tagField);
            GUILayout.EndHorizontal ();

            GUILayout.BeginHorizontal ();
            GUILayout.TextField ("Фильтр по маске: ");
            t.layerMask = LayerMaskField (t.layerMask);
            GUILayout.EndHorizontal ();


            t.project2D = EditorGUILayout.ToggleLeft (toggleText, t.project2D, EditorStyles.textField);

            GUILayout.Label ("© 2017 NULLcode Studio", EditorStyles.centeredGreyMiniLabel);
        }

        GUILayout.EndArea ();
        Handles.EndGUI ();
    }
}
#endif
