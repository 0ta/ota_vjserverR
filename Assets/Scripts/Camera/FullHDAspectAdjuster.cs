using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHDAspectAdjuster : MonoBehaviour
{
    // Start is called before the first frame update
    Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
        var aspectratio = 1080.0f / 1440.0f;
        _camera.rect = new Rect((1.0f - aspectratio) / 2.0f, 0, aspectratio, 1);
    }
 
}
