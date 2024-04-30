using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _mainCameraObj;

    private Camera _mainCamera;
    private float _cameraSpeed = 0.0005f;
    private int _screenPart = 15;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if ((Input.mousePosition.x >= Screen.currentResolution.width - Screen.currentResolution.width/ _screenPart) ||
                (Input.mousePosition.x <= Screen.currentResolution.width / _screenPart) ||
                (Input.mousePosition.y >= Screen.currentResolution.height - Screen.currentResolution.height / _screenPart) ||
                Input.mousePosition.y <= Screen.currentResolution.height / _screenPart)
        {
            _mainCameraObj.transform.position += (new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y) - new Vector3(Screen.width/2, 0,Screen.height/2)) * _cameraSpeed;
        }
    }
}
