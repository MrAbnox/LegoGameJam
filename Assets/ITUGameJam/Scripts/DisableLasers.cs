using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLasers : MonoBehaviour
{
    // Start is called before the first frame update


    public enum color
    {
        red,
        blue,
        green,
        none
    };

    public color enumColor;


    void Start()
    {
        // print(color.blue);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     gameObject.SetActive(false);
        // }
    }
}
