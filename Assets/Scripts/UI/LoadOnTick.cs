using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnTick : TickComponent
{
    public string sceneName;

    public override void OnTick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
