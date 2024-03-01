using TMPro;
using UnityEngine;
using VContainer;
using World;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _diedPreysAmountText;
        [SerializeField] private TextMeshProUGUI _diedPredatorsAmountText;
        
        private IWorldController _worldController;

        [Inject]
        public void Construct(IWorldController worldController)
        {
            _worldController = worldController;
        }

        private void Start()
        {
            OnPreyDie(0);
            OnPredatorDie(0);
            
            _worldController.OnPreyDie += OnPreyDie;
            _worldController.OnPredatorDie += OnPredatorDie;
        }

        private void OnDestroy()
        {
            _worldController.OnPreyDie -= OnPreyDie;
            _worldController.OnPredatorDie -= OnPredatorDie;
        }

        private void OnPreyDie(int amount)
        {
            _diedPreysAmountText.text = amount.ToString();
        }
        
        private void OnPredatorDie(int amount)
        {
            _diedPredatorsAmountText.text = amount.ToString();
        }
    }
}