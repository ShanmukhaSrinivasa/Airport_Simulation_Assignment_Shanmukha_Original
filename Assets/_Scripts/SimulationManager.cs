using TMPro;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    private bool isPaused = true;
    public TextMeshProUGUI buttonText;

    private void Start()
    {
        Time.timeScale = 0f;
        if(buttonText != null)
        {
            buttonText.text = "Start Simulation";
        }
    }

    public void ToggleSimulation()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;
        buttonText.text = isPaused ? "Start Simulation" : "Stop Simulation";
    }
}
