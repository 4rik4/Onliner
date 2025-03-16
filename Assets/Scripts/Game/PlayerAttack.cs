using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _trailTransform;
    [SerializeField] private GameObject _bulletAirPrefab;
    [SerializeField] private Transform _bulletOrigin;

    private Camera _camera;
    private PlayerControlls _playerControlls;
    private PhotonView _photonView;

    private bool _isAiming = false;

    private TargetMark _target;

    private void Awake()
    {
       
        _photonView = GetComponent<PhotonView>();

            
        
        _playerControlls = new PlayerControlls();

        if (_photonView.IsMine)
        {
        _playerControlls.KeyBoardControlls.Aim.started += OnAimChanged;
        _playerControlls.KeyBoardControlls.Aim.canceled += OnAimChanged;
        _playerControlls.KeyBoardControlls.Fire.started += OnShoot;
        }
    }

    private void OnEnable()
    {
        _playerControlls.Enable();
    }
    private void OnDisable()
    {
        _playerControlls.Disable();
    }

    private void Start()
    {
        _camera = Camera.main;

    }

    private void LateUpdate()
    {
        Aim();
    }

    private void OnAimChanged(InputAction.CallbackContext context)
    {
        _isAiming = context.ReadValueAsButton();
        _trailTransform.gameObject.SetActive(_isAiming);

        if (_target != null && _isAiming == false)
        {
            _target.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Shoot();
    }

    private void Aim()
    {
        if (_isAiming)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Vector3 mousePosition = hitInfo.point;
                Vector3 direction = mousePosition - transform.position;
                direction = new Vector3(direction.x, 0, direction.z);
                _trailTransform.forward = direction;

            }


            if(Physics.Raycast(_trailTransform.position, _trailTransform.forward, out RaycastHit hit, 8.0f))
            {
                if(hit.collider.TryGetComponent(out TargetMark targetMark))
                {
                    if(_target != targetMark && _target != null)
                    {
                        _target.GetComponent<MeshRenderer>().enabled = false;
                    }
                   _target = targetMark;
                    _target.GetComponent<MeshRenderer>().enabled = true;
                }
                else if(_target != null)
                {
                    _target.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            else if (_target != null)
            {
                _target.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else if (_target != null)
        {
            _target.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void Shoot()
    {
        if (_isAiming)
        {
            GameObject bullet = PhotonNetwork.Instantiate("Bullet", _bulletOrigin.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDirection(_trailTransform.forward, gameObject);
        }
    }
}
