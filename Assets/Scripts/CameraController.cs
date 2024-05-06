using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _mainCameraObj;

    private Camera _mainCamera;

    [SerializeField] private float _cameraSpeed = 5f;
    [SerializeField] private float _cameraSmooth = 0.5f;

    [SerializeField] private float _scrollSpeed = 5f;
    [SerializeField] private float _scrollSmooth = 0.5f;

    [SerializeField] private int _screenPart = 15;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if ((Input.mousePosition.x >= Screen.currentResolution.width - Screen.currentResolution.width / _screenPart) ||
                (Input.mousePosition.x <= Screen.currentResolution.width / _screenPart) ||
                (Input.mousePosition.y >= Screen.currentResolution.height - Screen.currentResolution.height / _screenPart) ||
                Input.mousePosition.y <= Screen.currentResolution.height / _screenPart)
        {
            _mainCameraObj.transform.position = Vector3.Lerp(_mainCameraObj.transform.position, _mainCameraObj.transform.position + (new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y) - new Vector3(Screen.width / 2, 0, Screen.height / 2)) * _cameraSpeed/10000, _cameraSmooth * Time.deltaTime);
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            _mainCameraObj.transform.position = Vector3.Lerp(_mainCameraObj.transform.position, _mainCameraObj.transform.position +  Vector3.up * -Input.mouseScrollDelta.y * _scrollSpeed, _scrollSmooth * Time.deltaTime);
        }
    }
}
