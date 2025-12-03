using System;
using _SecondGameJam.Scripts.Core.GameStates.Concrete;
using _SecondGameJam.Scripts.Core.Managers;
using _SecondGameJam.Scripts.Gameplay.Both.Combat;
using UnityEngine;

//IMPORTANT: destroys self after health reaches 0 (1 second later)
//DO NOT SAVE THE ENEMY FOR LONG, IT WILL BE DESTROYED


// This class is the base class for all enemies in the game
// It contains the basic functionality for all enemies
// It can be extended to add more functionality
namespace _SecondGameJam.Scripts.Gameplay.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f; //max speed
        
        [SerializeField] private float _frozenSpeedMultiplier = 0.3f;
        [SerializeField] private Color _frozonColor = Color.cyan;
        [SerializeField] private float _thawingRate = 0.5f; //amount per second. 1 is 100% in 1 second.
    
        // private PathPoint _nextPoint = null;
        private Rigidbody2D _body;
        private SpriteRenderer _spriteRenderer;
        private Color _originalColor;
        private CharacterStats _stats;

        private float _frozenPercent = 0f;
        private float _curSpeed;
        private const float _originalRotation = 0;
    
    
        // Start is called before the first frame update
        void Start()
        {
            _body = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.color;
            _stats = GetComponent<CharacterStats>();
            _curSpeed = _speed;
            transform.rotation = Quaternion.Euler(0,0,_originalRotation); //face right
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Move();
            Freeze(-_thawingRate * Time.fixedDeltaTime);
        }

        //in FixedUpdate
        //also reorients
        private void Move()
        {
            // if (_nextPoint != null) Reorient(_nextPoint.transform);
            _body.AddForce(transform.right * _curSpeed);
        }
        
        /**
         * Freezes the enemy by a certain percent
         * can't go below 0 or above 1
         * @param percent: the percent to freeze the enemy by
         */
        public void Freeze(float percent)
        {
            _frozenPercent = Math.Min(1, Math.Max(0, _frozenPercent + percent));
            _curSpeed = _speed * Mathf.Lerp(1, _frozenSpeedMultiplier, _frozenPercent);
            _spriteRenderer.color = Color.Lerp(_originalColor, _frozonColor, _frozenPercent);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("FinishBorder"))
            {
                GameManager.Instance.ChangeState(new GameOverState(GameManager.Instance));
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("forwarder"))
            {
                transform.rotation = Quaternion.Euler(0,0,_originalRotation); //face right
            }
        }

        private void Reorient(Transform target)
        {
            transform.up = target.position - transform.position;
        }
    }
}
