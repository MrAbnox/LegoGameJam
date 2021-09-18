using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public Button playbtn;
    public Button surprisebtn;
    public Button quitbtn;
    // Start is called before the first frame update
    public RobotController rc;
    public RawImage isOnline;
    public TextMeshProUGUI statusText;

    void Start()
    {
        playbtn.onClick.AddListener(StartGame);
        surprisebtn.onClick.AddListener(Surprise);
        quitbtn.onClick.AddListener(QuitGame);
        isOnline.color = Color.black;
        statusText.text = "No Technic Hub found!";
        ConnectHub();
    }

    void StartGame()
    {
        Debug.Log("Loading GameScene");
        SceneManager.LoadScene(1); // Define Scene Build, make sure menu is 0 and that game scene is 1.
    }

    void Surprise()
    {
        //to be implemented
        print("to be implemented!");
    }

    void QuitGame()
    {
        Application.Quit();
    }

    public void ConnectHub()
    {
        statusText.text = "Technic Hub Connected!";
        isOnline.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		Debug.Log ("You have clicked the button!");
	}
    */
}
