using Abstractions;
using Core.CommandExecutor;
using UnityEngine;
using UserControllSystem.CommandsRealization;

namespace Core
{
    public class MainUnit : MonoBehaviour, ISelectable, IAttackable, IDamageDealer
    {
        [SerializeField] private StopCommandExecutor _stopCommand;

        [SerializeField] private Sprite _icon;
        [SerializeField] private float _maxHealth = 100;
        private float _health = 100;

        public int Damage => _damage;
        [SerializeField] private int _damage = 25;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        public Transform PivotPoint { get; }
        public void RecieveDamage(int amount)
        {
            if (_health <= 0)
            {
                return;
            }
            _health -= amount;
            if (_health <= 0)
            {
                Invoke(nameof(Destroy), 1f);
            }
        }
        private async void Destroy()
        {
            await _stopCommand.ExecuteSpecificCommand(new StopCommand());
            Destroy(gameObject);
        }

    }
}