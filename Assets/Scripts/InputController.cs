using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InputController : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private UIManager _UIManager;

    private Camera _mainCamera;
    private NavMeshAgent _player;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _player = GameObject.Find("Player").GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            if (_timeManager.IsPaused == true)
            {
                _timeManager.IsPaused = false;
                Time.timeScale = 1;
            }

            else
            {
                _timeManager.IsPaused = true;
                Time.timeScale = 0;
            }

            _UIManager.Pause(_timeManager.IsPaused);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            _player.SetDestination(hit.point);
        }
    }
}
