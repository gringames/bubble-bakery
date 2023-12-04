using UnityEngine;

namespace Util
{
    public class Logic : MonoBehaviour
    {
        public static Logic Instance;

        public SceneChanger SceneChanger { get; private set; }

        public Quit Quit { get; private set; }
        // TODO: add AudioClip

        private void Awake()
        {
            CheckInstance();
            InitComponents();
        }

        private void CheckInstance()
        {
            if (ExistsOtherInstanceAlready())
            {
                Debug.LogError($"There are more than 1 Logics in the scene! Deleting {name}");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private bool ExistsOtherInstanceAlready()
        {
            return Instance != null && Instance != this;
        }

        private void InitComponents()
        {
            SceneChanger = GetComponent<SceneChanger>();
            Quit = GetComponent<Quit>();
        }
    }
}
