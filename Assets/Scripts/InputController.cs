using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private ParticleManager _particleManager;

    private Camera _mainCamera;
    private GameObject _player;
    private bool _isHeroSelect = false;
    private Unit selectedUnit;

    [HideInInspector] public UnityEvent<Unit> SelectUnit;
    [HideInInspector] public UnityEvent<Unit> ChangeSelectUnit;
    [HideInInspector] public UnityEvent DeSelectUnit;

    [HideInInspector] public UnityEvent<Unit, RaycastHit, int> CastSpell;

    private static InputController _instance;

    public static InputController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InputController>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _mainCamera = Camera.main;
        _player = GameObject.Find("Player");
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

        if (Input.GetMouseButtonDown(1) && _isHeroSelect)
        {
            RaycastHit hit = RayCast();

            if (hit.collider.gameObject.GetComponent<Unit>() && (hit.collider.gameObject != _player.gameObject))
            {
                _player.GetComponent<Unit>().MoveToAttack(hit.collider.gameObject);
                _particleManager.AttackParticle(hit.point);
            }
            else
            {
                _player.GetComponent<Unit>().Move(hit.point);
                _particleManager.MoveParticle(hit.point);
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && _isHeroSelect)
        {
            RaycastHit hit = RayCast();

            Collider[] nearObjects = Physics.OverlapSphere(hit.point, _player.GetComponent<Unit>().AttackRange);
            Collider nearestUnit = null;

            foreach (Collider collider in nearObjects)
            {
                if (collider.GetComponent<Unit>() && collider.gameObject != _player)
                {              
                    if (nearestUnit == null)
                    nearestUnit = collider;

                    else if (Vector3.Distance(collider.transform.position, hit.point) < Vector3.Distance(nearestUnit.transform.position, hit.point))
                    {
                        nearestUnit = collider;
                    }
                }
            }

            if (nearestUnit != null)
            {
                _player.GetComponent<Unit>().MoveToAttack(nearestUnit.gameObject);
            }
            else
            {
                _player.GetComponent<Unit>().Move(hit.point);
            }

            _particleManager.AttackParticle(hit.point);

        }

        if (Input.GetKeyDown(KeyCode.M) && _isHeroSelect)
        {
            RaycastHit hit = RayCast();

            _player.GetComponent<Unit>().Move(hit.point);
            _particleManager.MoveParticle(hit.point);
        }

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit = RayCast();
            Unit unit = null;

            if (hit.collider.GetComponent<Unit>())
            {
                unit = hit.collider.GetComponent<Unit>();
            }

            if (unit != null)
            {
                if (selectedUnit == null)
                {
                    SelectUnit.Invoke(unit);
                    selectedUnit = unit;
                }
                else if (unit != selectedUnit)
                {
                    ChangeSelectUnit.Invoke(selectedUnit);
                    SelectUnit.Invoke(unit);
                    selectedUnit = unit;
                }

                if (hit.collider.gameObject.name == "Player") _isHeroSelect = true;
                else _isHeroSelect = false;
            }

            else
            {
                ChangeSelectUnit.Invoke(selectedUnit);
                DeSelectUnit.Invoke();
                selectedUnit = null;
                _isHeroSelect = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && _isHeroSelect && _player.GetComponent<Unit>().Cooldown[0] == 0 && _player.GetComponent<Unit>().Mana >= _player.GetComponent<Unit>().Spells[0].ManaCost)
        {
            RaycastHit hit = RayCast();

            if (Vector3.Distance(_player.transform.position,hit.point) < _player.GetComponent<Unit>().Spells[0].CastRange)
                CastSpell.Invoke(_player.GetComponent<Unit>(), hit, 0);

            else
            {
                _particleManager.CastParticle(hit.point);
                _player.GetComponent<Unit>().MoveToCast(hit, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && _isHeroSelect && _player.GetComponent<Unit>().Cooldown[1] == 0 && _player.GetComponent<Unit>().Mana >= _player.GetComponent<Unit>().Spells[1].ManaCost)
        {
            RaycastHit hit = RayCast();

            if (Vector3.Distance(_player.transform.position, hit.point) < _player.GetComponent<Unit>().Spells[1].CastRange)
                CastSpell.Invoke(_player.GetComponent<Unit>(), hit, 1);

            else
            {
                _particleManager.CastParticle(hit.point);
                _player.GetComponent<Unit>().MoveToCast(hit, 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && _isHeroSelect && _player.GetComponent<Unit>().Cooldown[2] == 0 && _player.GetComponent<Unit>().Mana >= _player.GetComponent<Unit>().Spells[2].ManaCost)
        {
            RaycastHit hit = RayCast();

            if (Vector3.Distance(_player.transform.position, hit.point) < _player.GetComponent<Unit>().Spells[2].CastRange)
                CastSpell.Invoke(_player.GetComponent<Unit>(), hit, 2);

            else
            {
                _particleManager.CastParticle(hit.point);
                _player.GetComponent<Unit>().MoveToCast(hit, 2);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && _isHeroSelect && _player.GetComponent<Unit>().Cooldown[3] == 0 && _player.GetComponent<Unit>().Mana >= _player.GetComponent<Unit>().Spells[3].ManaCost)
        {
            RaycastHit hit = RayCast();

            if (Vector3.Distance(_player.transform.position, hit.point) < _player.GetComponent<Unit>().Spells[3].CastRange)
                CastSpell.Invoke(_player.GetComponent<Unit>(), hit, 3);

            else
            {
                _particleManager.CastParticle(hit.point);
                _player.GetComponent<Unit>().MoveToCast(hit, 3);
            }
        }
    }

    private RaycastHit RayCast()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);
        return hit;
    }
}
