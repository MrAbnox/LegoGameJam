using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private RawImage hubIcon;
    [SerializeField] private TextMeshProUGUI hubText;


    private float darkHubIconDarkness;

    private string connectedText = "Technic Hub connected";
    // Start is called before the first frame update
    private void Start()
    {
        darkHubIconDarkness = hubIcon.color.r;
        SetHubIconDark();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SpawnBlocks()
    {

    }

    public void HubConnected()
    {
        SetHubIconLight();
        hubText.text = connectedText;
    }

    public void HubDisconnected()
    {
        SetHubIconDark();
        hubText.text = "No " + connectedText;
    }

    //Dark meaning HUB isn't connected
    public void SetHubIconDark()
    {
        Color tempColor = new Color(darkHubIconDarkness, darkHubIconDarkness, darkHubIconDarkness);
        hubIcon.color = tempColor;
    }

    //Light meaning Hub is connected
    public void SetHubIconLight()
    {
        hubIcon.color = Color.white;
    }
}
