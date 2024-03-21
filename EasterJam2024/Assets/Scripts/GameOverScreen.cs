using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public GameObject pointsGainedTop;
    public void Setup(int score) {
        gameObject.SetActive(true);
        pointsGainedTop.SetActive(false);
        pointsText.text = score.ToString() + " POINTS";
    }

    public void RestartButton() {
        SceneManager.LoadSceneAsync(1);
    }

    public void MenuButton() {
        SceneManager.LoadSceneAsync(0);
    }
}
