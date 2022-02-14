using UnityEngine;

public class MenuViewController : MonoBehaviour
{
    public static MenuViewController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Select level menu.")]
    public GameObject SelectMenu;

    [Header("Playground for the levels.")]
    public GameObject Playground;

    [Header("Where we will instantiate the ui views.")]
    public Transform TRMenuContent;

    private void Start()
    {
        // We load level menu by default.
        ShowSelectLevelMenu();
    }

    public void ShowSelectLevelMenu()
    {
        // We close all the views.
        this.CloseAll();

        // We show the select level menu.
        Instantiate(SelectMenu, TRMenuContent).GetComponent<SelectLevelMenuViewController>().RefreshLevels();
    }

    public void ShowLevelView(int levelIndex)
    {
        // We close older views.
        this.CloseAll();

        // We show the playground.
        GameObject playground = Instantiate(Playground, TRMenuContent);

        // We show the view.
        playground.GetComponent<PlaygroundViewController>().LoadPlayground(levelIndex);
    }

    public void CloseAll()
    {
        foreach(Transform oldMenu in TRMenuContent)
            Destroy(oldMenu.gameObject);
    }

}
