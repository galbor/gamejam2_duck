using System;
using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Towers
{
    /** Usage: Place on duck prefab, then set _sprites through the inspector. */
    [RequireComponent(typeof(SpriteRenderer))]
    public class DuckSpriteRotator : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private float _rotationTolerance = 2f;
        private SpriteRenderer _duckRenderer;
        private float _currentAngle;
        
        
        private void Start()
        {
            _duckRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void Update()
        {
            if (_sprites.Length < 2)
            {
                return;
            }
            Transform duckTransform = transform.parent;
            float duckAngle = duckTransform.rotation.eulerAngles.z;
            if (Math.Abs(duckAngle - _currentAngle) > _rotationTolerance)
            {
                _currentAngle = duckAngle;
                transform.rotation = Quaternion.identity;
                Sprite directionSprite = GetDirectionSprite();
                if (directionSprite != _duckRenderer.sprite)
                {
                    Color color = _duckRenderer.color;
                    _duckRenderer.sprite = directionSprite;
                    _duckRenderer.color = color;
                }
            }
        }

        private Sprite GetDirectionSprite()
        {
            int totalSprites = _sprites.Length;
            float stepSize = 360f / totalSprites;
            int spriteIndex = (int) Math.Floor(_currentAngle / stepSize);
            // Unity uses a right-handed coordinate system, so we need to flip the sprite index:
            spriteIndex = (totalSprites - spriteIndex) % totalSprites;
            Sprite newSprite = _sprites[spriteIndex];
            return newSprite;
        }
    }
}
