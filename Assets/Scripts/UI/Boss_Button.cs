using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Button : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene not found: " + sceneName);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("GGWP");
    }
}