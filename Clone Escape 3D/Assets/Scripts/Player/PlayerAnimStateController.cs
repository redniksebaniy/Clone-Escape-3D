using UnityEngine;

namespace CloneEscape.PlayerBehavours
{
    public class PlayerAnimStateController : PlayerBehaviour
    {
        private Animator _animator;

        const string VELOCITY = "velocity";
        const string IS_PUSHING = "isPushing";

        private void Start() => _animator = GetComponent<Animator>();

        private void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            int magnitude = Mathf.RoundToInt(new Vector2(x, z).magnitude);

            _animator.SetInteger(VELOCITY, magnitude);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!enabled) return;

            if (collision.collider.TryGetComponent(typeof(Rigidbody), out _))
            {
                _animator.SetBool(IS_PUSHING, true);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!enabled) return;

            if (collision.collider.TryGetComponent(typeof(Rigidbody), out _))
            {
                _animator.SetBool(IS_PUSHING, false);
            }
        }

        private void OnDisable() => _animator.SetInteger(VELOCITY, 0);
    }
}