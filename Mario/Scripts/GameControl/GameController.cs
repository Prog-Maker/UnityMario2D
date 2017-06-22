using System.Collections;
using System.Linq;
using UnityEngine;
using MarioWorldForAll;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject reStarter; //рестартер префаб

    public static GameController instance;

    public CharacterBase Character { get; private set; }

    [Header("Array of Characters")]
    [SerializeField] private CharacterBase[] characters;

    [Header("Array of NonCharacter Sounds")]
    [SerializeField] private AudioClip[] clips;

    private AudioSource[] SoundPlayers;

    public bool IsGameOver { get; set; }


    [Header("Scenes")]
    [SerializeField] private string[] sceneNames;

    private Transform startPoint;


   // private string str = "GameController DURAK";

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //if (Restarter.instance == null) Instantiate(reStarter);
    }

    void Start ()
    {
        // StartCoroutine(FadeAndSwitchScenes(sceneNames[0]));

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);

        //CurrentCharacter.character = characters[0];
      //  startPoint = newlyLoadedScene.GetRootGameObjects().Where(x => x.name == "StartPoint").FirstOrDefault().transform;

        //var character = Instantiate(characters[1], startPoint.position, startPoint.rotation);

        //Character = character;
        SoundPlayers = GetComponents<AudioSource>();
        SoundPlayers[1].Play();

        //str = "OK";
}

    private void Update()
    {
       // Debug.Log(str);
        if (!IsGameOver) return;
        if (IsGameOver) SoundPlayers[0].clip = null;
    }

    public void PlaySound(string name)
    {
        SoundPlayers[0].clip = Clip(name);
        SoundPlayers[0].Play();
    }

    private AudioClip Clip(string name)
    {
        return clips.Where(x => x.name == name).FirstOrDefault();
    }


    /*public void FadeAndLoadScene(SceneReaction sceneReaction) // Это публичный метод в который и передаем название сцены, которую надо загрузить
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneReaction.sceneName));
        }
    }*/

    private IEnumerator FadeAndSwitchScenes(string sceneName) // Этот метод выгружает сцену и вызывает метод загрузки сцены, 
    {
        //yield return StartCoroutine(Fade(1f));
        /* if (BeforeSceneUnload != null)
             BeforeSceneUnload ();*/
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        /* if (AfterSceneLoad != null)
             AfterSceneLoad ();*/

        //yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName) // Метод который загружает одну сцену в другую LoadSceneMode.Additive 
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
    }
}

enum EnvSounds
{
    smb_stomp = 0 
}
