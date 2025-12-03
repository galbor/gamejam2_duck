using System;
using System.Collections.Generic;
using _SecondGameJam.Scripts.Gameplay.Both.Combat;
using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Enemies
{
    [RequireComponent(typeof(CharacterStats))]
    public class PainAnimation : MonoBehaviour
    {
        // Order sprites from least damaged to most damaged:
        [SerializeField] private List<Sprite> _painSprites;

        private SpriteRenderer _spriteRenderer;
        private CharacterStats _stats;
        private int _lastHealth;
        private int _threshold;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _stats = GetComponent<CharacterStats>();
            _threshold = _stats.MaxHealth / _painSprites.Count;
        }


        private void Update()
        {
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            if (_stats.CurrentHealth != _lastHealth)
            {
                _lastHealth = _stats.CurrentHealth;
                int index = _lastHealth / _threshold;
                index = Math.Max(0, _painSprites.Count - index - 1);
                // TODO: Set the sprite's color to previous color.
                _spriteRenderer.sprite = _painSprites[index];
            }
        }
    }
}