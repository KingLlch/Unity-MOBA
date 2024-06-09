using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _moveParticle;

    public void MoveParticle(Vector3 position)
    {
        var main = _moveParticle.main;
        main.startColor = Color.green;

        _moveParticle.transform.position = position;
        _moveParticle.Play();
    }

    public void AttackParticle(Vector3 position)
    {
        var main = _moveParticle.main;
        main.startColor = Color.red;

        _moveParticle.transform.position = position;
        _moveParticle.Play();
    }

    public void CastParticle(Vector3 position)
    {
        var main = _moveParticle.main;
        main.startColor = Color.blue;

        _moveParticle.transform.position = position;
        _moveParticle.Play();
    }
}
