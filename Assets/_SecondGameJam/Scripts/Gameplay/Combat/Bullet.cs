using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    public class Bullet : Weapon
    {
        // Keep private field for serialization:
        [SerializeField] private string _objectPoolManagerName;
        public string ObjectPoolManagerName => _objectPoolManagerName;
        public void SetMass(float mass) => _body.mass = mass;
        public void SetSize(float size) => transform.localScale = new Vector3(size, size, 1) * _size;

        private Rigidbody2D _body;
        private SpriteRenderer _spriteRenderer;
        private Sprite _defaultSprite;

        private float _speed = 5.0f;

        private ObjectPoolManager _objectPoolManager;
        private float _size;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultSprite = _spriteRenderer.sprite;
            _size = transform.localScale.x;
        }

        private void FixedUpdate()
        {
            _body.velocity = transform.up * _speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(TargetTag))
            {
                DamageEnemy(other.gameObject);
                if (_objectPoolManager) _objectPoolManager.ReturnObjectToPool(gameObject);
            }

            if (_objectPoolManager) _objectPoolManager.ReturnObjectToPool(gameObject);
        }

        public void SetAttributes(float speed, int damage, string targetTag, ObjectPoolManager objectPoolManager)
        {
            _speed = speed;
            Damage = damage;
            TargetTag = targetTag;
            _objectPoolManager = objectPoolManager;
        }
        
        public void SetSprite(Sprite sprite)
        {
            if (sprite)
            {
                _spriteRenderer.sprite = sprite;
                _spriteRenderer.color = Color.white;
            }
            else
            {
                _spriteRenderer.sprite = _defaultSprite;
                _spriteRenderer.color = Color.black;
            }
        }
    }
}