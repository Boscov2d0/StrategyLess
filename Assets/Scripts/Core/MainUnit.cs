using Abstractions;
using UnityEngine;

namespace Core
{
    public class MainUnit : MonoBehaviour, ISelectable
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _maxHealth = 100;
        private float _health = 100;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;
    }
}