using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelGameOverViewController : MonoBehaviour
{
    [Header("We will print gameover or win.")]
    public TMP_Text TXTHeader;

    [Header("We store the stars in here.")]
    public Transform Stars;

    public void ShowWinView(int leftBomb)
    {
        // We show the game over view.
        gameObject.SetActive(true);

        // We loop all the stars.
        for (int ii = 0; ii < 3; ii++)
            Stars.GetChild(ii).GetComponent<Image>().color = ii <= leftBomb ? Color.white : Color.black;

        // We get the star count.
        int starCount = Mathf.Clamp(leftBomb + 1, 0, 3);

        // We update the user progress.
        SaveLoadController.Instance.SaveData.UpdateLevelProgress(PlaygroundViewController.Instance.LevelIndex, starCount);

        // We print the text tells win.
        TXTHeader.text = $"CONGRATS!";
    }

    public void ShowGameOverView()
    {
        // We show the game over view.
        gameObject.SetActive(true);

        // We loop all the stars.
        for (int ii = 0; ii < 3; ii++)
            Stars.GetChild(ii).GetComponent<Image>().color = Color.black;

        // We print the text tells win.
        TXTHeader.text = $"GAME OVER!";
    }

    public void OnClickLevels()
    {
        PlaygroundViewController.Instance.LeaveFromPlayground();
    }

    public void OnClickRestart()
    {
        // We make sure the level is clear.
        PlaygroundViewController.Instance.ResetPlayground();

        // We hide the view.
        gameObject.SetActive(false);
    }

}
