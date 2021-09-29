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

    void Awake()
    {
        // GameManager.instance.IsGamePlaying = true;
        if (GetComponent<BoxCollider>())
        {
            collider = GetComponent<BoxCollider>();
        }

        // print(color.blue);
        if (gameObject.GetComponent<ParticleSystem>() != null)
        {
            particleSystem = gameObject.GetComponent<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log((int) enumColor);
        if (GameManager.CurrentColorPuzzle == (int) enumColor)
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
    }
}
