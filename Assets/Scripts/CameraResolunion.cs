using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolunion : MonoBehaviour
{
    private float _defaultScreenWidth;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _defaultScreenWidth = 2.8125f;
        _camera.orthographicSize = _defaultScreenWidth / _camera.aspect;
    }
}
