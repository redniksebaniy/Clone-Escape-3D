using UnityEngine;

namespace CloneEscape.PlayerBehavours
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : PlayerBehaviour
    {
        [SerializeField]
        private uint speed;

        private Rigidbody _rigidbody;
        private Vector3 velocity;

        private void Start() => _rigidbody = GetComponent<Rigidbody>();

        private void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (Input.GetAxisRaw("Horizontal") == 0 &&
                Input.GetAxisRaw("Vertical") == 0)
            {
                x = 0;
                z = 0;
            }

            velocity = new Vector3(x, 0, z).normalized;
        }

        private void FixedUpdate()
        {
            transform.LookAt(_rigidbody.position + velocity);
            _rigidbody.velocity = velocity * speed;
        }
    }
}
