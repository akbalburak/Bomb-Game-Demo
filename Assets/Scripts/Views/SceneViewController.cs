using UnityEngine;

public class SceneViewController : MonoBehaviour
{
    public static SceneViewController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnClickMenu()
    {

    }

    public void OnClickPlay()
    {

    }
}
