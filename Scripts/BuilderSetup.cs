using UnityEngine;

[ExecuteInEditMode]

public class BuilderSetup : MonoBehaviour
{

    public Vector2 position = new Vector2(10, 10); // позиция меню
    public float width = 250; // ширина
    public float height = 400; // высота
    public string prefabsPath = "Prefabs"; // путь в папке Resources

    // данные переменные используются менюшкой
    [HideInInspector] public Transform[] prefabs;
    [HideInInspector] public string[] prefabsNames;
    [HideInInspector] public int index;
    [HideInInspector] public bool showButton, project2D;
    [HideInInspector] public string tagField;
    [HideInInspector] public LayerMask layerMask;

    public void LoadResources ()
    {
        prefabs = Resources.LoadAll<Transform> (prefabsPath);

        prefabsNames = new string [prefabs.Length];
        for (int i = 0; i < prefabs.Length; i++)
        {
            prefabsNames [i] = prefabs [i].name;
        }

        index = 0;
    }

    public void InstantiatePrefab (Vector3 position)
    {
        Instantiate (prefabs [index], position, Quaternion.identity);
    }
}

