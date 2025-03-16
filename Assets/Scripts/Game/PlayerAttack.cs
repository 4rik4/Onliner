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

    private void LateUpdate()//думаем еще
    {
        Aim();
    }

    private void OnAimChanged(InputAction.CallbackContext context)
    {
        _isAiming = context.ReadValueAsButton();
        _trailTransform.gameObject.SetActive(_isAiming);
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

            RaycastHit[] raycastHits = Physics.RaycastAll(_trailTransform.position + _trailTransform.forward, _trailTransform.forward, 8.0f);

            foreach (RaycastHit hit in raycastHits)
            {
                if(hit.collider.TryGetComponent(out TargetMark targetMark))
                {
                    targetMark.IsTarget = true;
                }
            }
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
