using TMPro;
using UnityEngine;

public class PlaygroundViewController : MonoBehaviour
{
    public static PlaygroundViewController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Header("This is the index of the level.")]
    public int LevelIndex;

    [Header("Manages the level.")]
    public PlaygroundController Playground;
    
    [Header("We will use to show gameover or victory view.")]
    public LevelGameOverViewController LevelCompletedView;

    [Header("Name of the level.")]
    public TMP_Text TXTLevelName;

    [Header("This is the count of the bombs.")]
    public TMP_Text TXTBombCount;

    public void LoadPlayground(int levelIndex)
    {
        // We bind the level index.
        this.LevelIndex = levelIndex;

        // We activate the view.
        gameObject.SetActive(true);

        // We bind the level index.
        TXTLevelName.text = $"LEVEL-{levelIndex}";

        // We get the level data.
        LevelDataItem levelData = LevelDataService.Instance.GetLevelData(levelIndex);

        // We load the level.
        Playground.LoadGrid(levelData);
    }
    public void RefreshBombCount()
    {
        // We update the bomb count.
        this.TXTBombCount.text = $"{Playground.LeftBomb}";
    }
    public void ResetPlayground()
    {
        // We clear the level.
        this.Playground.ClearLevel();

        // We show the view.
        this.LoadPlayground(this.LevelIndex);
    }
    public void LeaveFromPlayground()
    {
        // We also close the view.
        MenuViewController.Instance.ShowSelectLevelMenu();
    }
}
