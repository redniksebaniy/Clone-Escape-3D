using UnityEngine;
using CloneEscape.PlayerBehavours;
using CloneEscape.LevelBase;

namespace CloneEscape.Objects
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemySystem : MonoBehaviour
    {
        [SerializeField]
        private float viewRange;

        [SerializeField]
        private float speed;

        private Transform _playerTransform;

        private Rigidbody _rigidbody;
        private Vector3 velocity;

        private Animator _animator;
        private const string VELOCITY = "velocity";
        private const string IS_PUSHING = "isPushing";

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();

            PlayerSelector.Instance.OnNewPlayerSelected.AddListener(SetNewPlayer);
        }

        private void Update()
        {
            if (_playerTransform == null || !IsPlayerInView())
            {
                velocity = Vector3.zero;
            }
            else
            {
                velocity = _playerTransform.position - transform.position;
                velocity = velocity.normalized * speed;

                transform.LookAt(_playerTransform.position);
            }

            _animator.SetInteger(VELOCITY, Mathf.FloorToInt(velocity.magnitude));
        }

        private bool IsPlayerInView()
        {
            if (Physics.Raycast(transform.position + Vector3.up, _playerTransform.position - transform.position, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<SelectablePlayer>(out _))
                {
                    return true;
                }
            }

            return false;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + velocity * Time.fixedDeltaTime);
        }

        private void SetNewPlayer(SelectablePlayer newPlayer)
        {
            _playerTransform = newPlayer.transform;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<SelectablePlayer>(out _))
            {
                if (collision.transform == _playerTransform)
                {
                    LevelController.Instance.OnPlayerLose?.Invoke();
                }

                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.TryGetComponent<Rigidbody>(out _))
            {
                _animator.SetBool(IS_PUSHING, true);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Rigidbody>(out _))
            {
                _animator.SetBool(IS_PUSHING, false);
            }
        }
    }
}