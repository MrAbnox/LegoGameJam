using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLasers : MonoBehaviour
{
    // Start is called before the first frame update

    BoxCollider collider;
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
        if(GetComponent<BoxCollider>())
        {
            collider = GetComponent<BoxCollider>();
        }
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
            if(collider)
                collider.enabled = false;
        }
        else
        {
            particleSystem.Play();
            if(collider)
                collider.enabled = true;
        }
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     gameObject.SetActive(false);
        // }
    }
}
