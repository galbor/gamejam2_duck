using System.Collections;
using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DocMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 70f;
        [SerializeField] private float _rotateTime = 0.15f;
        [SerializeField] private Rigidbody2D _rb;
        private Vector2 _direction;
        private bool _isMoving;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _isMoving = true;
            _direction = Vector2.down;
        }

        private void Update()
        {
            _rb.velocity = _isMoving ? _direction * (_speed * Time.deltaTime) : Vector2.zero;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("border"))
            {
                // TODO: Possible multiple direction-changes.
                StartCoroutine(ChangeDirection());
            }
        }

        private IEnumerator ChangeDirection()
        {
            _isMoving = false;
            for (int i = 0; i < 4; i++)
            {
                transform.Rotate(0, 0, 45);
                yield return new WaitForSeconds(_rotateTime);
            }

            _direction = -_direction;
            _isMoving = true;
        }
    }
}