using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelMenuViewItemController : MonoBehaviour
{
    /// <summary>
    /// Index of the level.
    /// </summary>
    public int LevelIndex { get; private set; }

    [Header("Play button to start game.")]
    public Button BTNPlay;

    [Header("Lock button to prevent user enter the level.")]
    public Button BTNLocked;

    [Header("Name of the level.")]
    public TMP_Text TXTLevelName;

    [Header("Star progress for to activate the level..")]
    public Slider SLDLevelProgress;

    [Header("Transform all the stars contained.")]
    public Transform StarsParent;

    public void LoadLevel(LevelDataItem levelData)
    {
        // We bind the level index.
        this.LevelIndex = LevelDataService.Instance.GetLevelIndex(levelData);

        // We check is level ready to play.
        bool isLevelReadyToPlay = LevelDataService.Instance.IsLevelReadyToPlay(this.LevelIndex);

        // We will activate if the level playable.
        BTNPlay.gameObject.SetActive(isLevelReadyToPlay);

        // We activate lock button if the level is not ready to play.
        BTNLocked.gameObject.SetActive(!isLevelReadyToPlay);

        // We print the level name.
        TXTLevelName.text = $"LEVEL-{this.LevelIndex}";

        // if not ready to play we are going to show the slider and progress.
        if (!isLevelReadyToPlay)
        {
            // if this is the last playable level.
            // But user cant play this then we are going to show the slider.
            bool isPreviousLevelPlayed = LevelDataService.Instance.IsPreviousLevelPlayed(this.LevelIndex);

            // if previous level played but still cant play.
            // Means we are going to check star condition.
            if (isPreviousLevelPlayed)
            {
                // We make sure stars are closed.
                StarsParent.gameObject.SetActive(false);

                // First we activate the slider.
                SLDLevelProgress.gameObject.SetActive(true);

                // We bind the star cost of level.
                SLDLevelProgress.maxValue = LevelDataService.Instance.GetStarCountForPlayingTheLevel(this.LevelIndex);

                // We bind the current progress.
                SLDLevelProgress.value = LevelDataService.Instance.GetStarCountTillTheLevel(this.LevelIndex);

                // We update the progress.
                SLDLevelProgress.transform.Find("TXTProgress").GetComponent<TMP_Text>().text = $"{SLDLevelProgress.value}/{SLDLevelProgress.maxValue}";

                // We dont need to go more.
                return;
            }
        }

        // if not return means we will use stars to show user progress.
        // Here is the star management place.

        // We make sure slider is closed.
        SLDLevelProgress.gameObject.SetActive(false);

        // We activate the star.
        StarsParent.gameObject.SetActive(true);

        // We get the level star.
        int levelStar = LevelDataService.Instance.GetLevelStar(this.LevelIndex);

        // We make brighter the completeds.
        for (int i = 0; i < levelStar; i++)
            StarsParent.GetChild(i).GetComponent<Image>().color = Color.white;
    }

    public void OnClickLevel()
    {
        // We show the menu view.
        MenuViewController.Instance.ShowLevelView(this.LevelIndex);
    }

}
