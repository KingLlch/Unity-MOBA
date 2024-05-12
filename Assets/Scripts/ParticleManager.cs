using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _moveParticle;
    [SerializeField] private ParticleSystem _attackParticle;

    public void MoveParticle(Vector3 position)
    {
        _moveParticle.transform.position = position;
        _moveParticle.Play();
    }

    public void AttackParticle(Vector3 position)
    {
        _attackParticle.transform.position = position;
        _attackParticle.Play();
    }
}
