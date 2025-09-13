using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public TextMeshProUGUI waveText;
    public void UpdateWaveText(int currentWaveNumber)
    {
        if (waveText != null)
        {
            waveText.text = "Oleada: " + currentWaveNumber;
        }
    }
}