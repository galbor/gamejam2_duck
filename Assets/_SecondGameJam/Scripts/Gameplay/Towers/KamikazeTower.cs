using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class KamikazeTower : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _body;

        private void Start()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }


        private void Move()
        {
            _body.AddForce(_speed * Vector2.left);
        }
    }
}
