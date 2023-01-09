using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;
using Abstractions;
using System.Text;

namespace UserControllSystem.UI.Presenter
{
    public class GameOverScreenPresenter : MonoBehaviour
    {
        [Inject] private IGameStatus _gameStatus;
        [SerializeField] private Text _text;
        [SerializeField] private GameObject _view;

        [Inject]
        private void Init()
        {
            _gameStatus.Status.ObserveOnMainThread().Subscribe(result =>
            {
                StringBuilder sb = new StringBuilder($"Game Over!");
                sb.AppendLine("\n");
                if (result == 0)
                {
                    sb.AppendLine("Ничья!");
                }
                else
                {
                    sb.AppendLine($"Победила партия №{result}");
                }
                _view.SetActive(true);
                _text.text = sb.ToString();
                Time.timeScale = 0;
            });
        }
    }
}