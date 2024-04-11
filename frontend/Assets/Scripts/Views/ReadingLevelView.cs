using System;
using UnityEngine;
using UnityEngine.UI;

public class ReadingLevelView : MonoBehaviour
{
    public Button kindergartenButton;
    public Button firstGradeButton;
    public Button secondGradeButton;
    public Button thirdGradeButton;

    // Define an event for reading level selection
    public static event Action<string> ReadingLevelSelected;

    private void Start()
    {
        kindergartenButton.onClick.AddListener(() => OnReadingLevelButtonClick("Kindergarten"));
        firstGradeButton.onClick.AddListener(() => OnReadingLevelButtonClick("First Grade"));
        secondGradeButton.onClick.AddListener(() => OnReadingLevelButtonClick("Second Grade"));
        thirdGradeButton.onClick.AddListener(() => OnReadingLevelButtonClick("Third Grade"));
    }

    private void OnReadingLevelButtonClick(string readingLevel)
    {
        Debug.Log("Selected reading level: " + readingLevel);
        // Emit the reading level selection event
        ReadingLevelSelected?.Invoke(readingLevel);
    }
}
