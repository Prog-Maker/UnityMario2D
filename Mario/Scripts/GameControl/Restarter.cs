using Assets.Mario.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarioWorldForAll
{

    public class Restarter : MonoBehaviour
    {
       // [SerializeField] private int sceneId;
        private Scene scene;

        public static Restarter instance;

        private void Awake()
        {
            if (instance == null) instance = this;
            else if (instance != this)
            {
                Destroy(gameObject);
            }

           // DontDestroyOnLoad(gameObject);
        }


        private void Start()
        {
            scene = SceneManager.GetActiveScene();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Player))
            {
                SceneManager.LoadScene(scene.name);
            }

            if (other.CompareTag(Tags.Enemy))
            {
                Destroy(other.gameObject);
            }
        }

        public void ReStart(int sceneId)
        {
            SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
        }

    }
}
