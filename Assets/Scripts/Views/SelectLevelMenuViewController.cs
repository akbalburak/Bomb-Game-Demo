using UnityEngine;
using UnityEngine.UI;

public class SelectLevelMenuViewController : MonoBehaviour
{
    public static SelectLevelMenuViewController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Prefab for the levels.")]
    public GameObject GOLevelItem;

    [Header("We are going to instantiate levels in scrollrect content.")]
    public ScrollRect SRLevelContent;

    public void RefreshLevels()
    {
        // We clear older levels.
        this.ClearOlderLevels();

        // We loop all the levels.
        foreach (LevelDataItem levelData in LevelDataService.Instance.Levels)
        {
            // We create a level item.
            GameObject levelItem = Instantiate(GOLevelItem, SRLevelContent.content);

            // We load all the level items. 
            levelItem.GetComponent<SelectLevelMenuViewItemController>().LoadLevel(levelData);
        }
    }

    private void ClearOlderLevels()
    {
        // We remove all the children.
        foreach (Transform child in SRLevelContent.content)
            Destroy(child.gameObject);
    }

}
