using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Constantine
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField]
        private SceneReference[] sceneToLoad;

        public UnityEvent OnSceneLoaded;

        private void Start()
        {
            if (AreAllSceneValid())
            {
                Trigger();
            }
            else
            {
                LoadAllScenes();
            }


        }

        private void LoadAllScenes()
        {
            foreach (SceneReference s in sceneToLoad)
            {
                if (!s.GetScene().IsValid())
                {
                    s.LoadSceneAsync(true).completed += OnOperationCompleted;
                }
            }
        }

        private void OnOperationCompleted(AsyncOperation operation)
        {
            operation.completed -= OnOperationCompleted;
            if (AreAllSceneValid())
                Trigger();
        }

        private void Trigger()
        {
            OnSceneLoaded?.Invoke();
            Destroy(gameObject);
        }

        private bool AreAllSceneValid()
        {
            for (int i = 0; i < sceneToLoad.Length; i++)
            {
                Scene scene = sceneToLoad[i].GetScene();
                if (!scene.IsValid() || !scene.isLoaded) return false;
            }
            return true;
        }
    }
}