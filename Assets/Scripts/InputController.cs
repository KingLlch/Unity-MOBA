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
    private bool _isHeroSelect = true;

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

        if ((Input.GetMouseButtonDown(1)) &&(_isHeroSelect))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            _player.SetDestination(hit.point);
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);

            if (hit.collider.GetComponent<Unit>())
            {
                _UIManager.ShowUI();
                _UIManager.ChangeUI(hit.collider.GetComponent<Unit>().Health, hit.collider.GetComponent<Unit>().MaxHealth, hit.collider.GetComponent<Unit>().Mana, hit.collider.GetComponent<Unit>().MaxMana);
                _isHeroSelect = true;
            }

            else
            {
                _UIManager.HideUI();
                _isHeroSelect = false;
            }
        }
    }
}
