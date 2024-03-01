using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Animals
{
    [RequireComponent(typeof(IAnimal))]
    public class AnimalEatController : MonoBehaviour
    {
        [SerializeField] private GameObject _tastyLabel;
        [SerializeField] private int _tastyLabelShowTimeMilliSec = 1000;

        [SerializeField] private float _scaleIncreaseByEat = 0.1f;
        [SerializeField] private float _scaleIncreaseTime = 0.7f;

        private IAnimal _animal;

        private void Awake()
        {
            _animal = GetComponent<IAnimal>();
            _animal.OnEat += OnEat;
        }

        private void OnDestroy()
        {
            _animal.OnEat -= OnEat;
        }

        private void OnEat(IAnimal animal)
        {
            _ = OnEatTask();
        }

        private async UniTask OnEatTask()
        {
            transform.DOScale(GetEatenScale(), _scaleIncreaseTime);
            _tastyLabel.SetActive(true);

            await UniTask.Delay(_tastyLabelShowTimeMilliSec);

            _tastyLabel.SetActive(false);
        }

        private Vector3 GetEatenScale()
        {
            return new Vector3(Calc(), Calc(), Calc());

            float Calc() => 1f + _scaleIncreaseByEat * _animal.EatenAnimalsAmount;
        }
    }
}