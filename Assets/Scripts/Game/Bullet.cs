using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private float _speed = 8.0f;
    [SerializeField] private int _damage = 10;

    private Rigidbody _rigidbody;

    private GameObject _objectToIgnore;

    private IEnumerator DestroyAfter(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(gameObject);
    }

    private void Awake()
    {
        _rigidbody = gameObject.AddComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        StartCoroutine(DestroyAfter(gameObject, 1.0f));
    }

    private void Update()
    {
        
    }

    public void SetDirection(Vector3 direction, GameObject objectToIgnore = null)
    {
        _rigidbody.AddForce(direction.normalized * _speed, ForceMode.Impulse);
        transform.forward = direction;

        _objectToIgnore = objectToIgnore;
        _collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != _objectToIgnore && other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
