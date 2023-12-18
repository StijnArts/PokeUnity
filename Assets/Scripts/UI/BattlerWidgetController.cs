using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI
{
    public class BattlerWidgetController
    {
        private VisualElement _battleContainer;
        private ProgressBar _healthBar;
        private Label _nameLabel;
        private bool _showHpValues;
        private string _hpCounter => _showHpValues ? _currentHp+"/"+ _maxHp : "";
        private int _maxHp;
        private int _currentHp;
        public BattlerWidgetController(VisualElement battleContainer, PokemonIndividualData battler, bool showHpValues = false)
        {
            _battleContainer = battleContainer;
            _healthBar = _battleContainer.Q("HealthBar") as ProgressBar;
            _nameLabel = _battleContainer.Q("SelectedOposingPokemonName") as Label;
            _showHpValues = showHpValues;
            _healthBar.title = _hpCounter;

            SetName(battler.GetName());
            SetMaxHealth(battler.Stats.Hp);
            SetCurrentHealth(battler.CurrentHp);
        }

        public void SetName(string battlerName)
        {
            _nameLabel.text = battlerName;
        }

        public void SetMaxHealth(int maxHp)
        {
            _healthBar.highValue = maxHp;
        }

        public void SetCurrentHealth(int currentHp)
        {
            _healthBar.value = currentHp;
        }

        public void SetHealth(int maxHp, int currentHp) 
        { 
            SetMaxHealth(maxHp);
            SetCurrentHealth(currentHp);
        }
    }
}
