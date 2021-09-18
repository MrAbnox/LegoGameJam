using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLasers : MonoBehaviour
{
    // Start is called before the first frame update


    public enum color
    {
        none,
        blue,
        green,
        red,
    };

    public color enumColor;
    ParticleSystem particleSystem;

    void Start()
    {
        // print(color.blue);
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log((int) enumColor);
        if (RobotController.CurrentColorPuzzle == (int) enumColor)
        {
            particleSystem.Stop();
        }
        else
        {
            particleSystem.Play();
        }
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     gameObject.SetActive(false);
        // }
    }
}
