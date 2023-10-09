using System;
using UnityEngine;
using Views;

namespace Models.Units
{
    public class EnemyUnitModel
    {
        private readonly EnemyUnitView _enemyUnitView;

        private int _currentHealth = 0;
        private int _maxHealth = 0;

        public event Action EnemyIsDead;

        public EnemyUnitModel(EnemyUnitView enemyUnitView)
        {
            _enemyUnitView = enemyUnitView;
        }

        public void SetHealth(int health)
        {
            _currentHealth = _maxHealth = health;
            _enemyUnitView.SetHealthBar(1);
        }

        public void Hit(int damage)
        {
            _enemyUnitView.SetHit();
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                EnemyIsDead?.Invoke();
            }

            _enemyUnitView.SetHealthBar((float) _currentHealth / (float) _maxHealth);
        }
    }
}