using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneCall : MonoBehaviour
{
    public string sceneName;

    public void Activate()
    {
        SceneManager.LoadScene(sceneName);
    }
}
