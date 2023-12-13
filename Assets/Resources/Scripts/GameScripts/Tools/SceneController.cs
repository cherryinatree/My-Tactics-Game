using UnityEngine;
using UnityEngine.SceneManagement;


namespace GamingTools
{
    public static class SceneController
    {
        public static void ChangeScene(string newScene)
        {
            Spawner.Clear();
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene(newScene);
        }
    }
}
