using _project.Scripts.Observer;
using UnityEngine;

namespace _project.Scripts.Ui.Base
{
    public class UiRoot : MonoBehaviour, IObserver
    {
        [SerializeField] private UiElement[] _uiElements;

        private void Start()
        {
            foreach (var element in _uiElements)
            {
                element.Init(this);
            }
        }

        public T GetUiElement<T>() where T : UiElement
        {
            for (int i = 0; i < _uiElements.Length; i++)
            {
                if (_uiElements[i].GetType() == typeof(T))
                {
                    return _uiElements[i] as T;
                }
            }

            Debug.LogError($"UI_ELEMENT_{typeof(T)}_NOT_FOUND");
            return null;
        }

        public void React(GameAction gameAction)
        {
            switch (gameAction)
            {
                case GameAction.CharacterCollide:
                    GetUiElement<DefeatMenu>().Show();
                    break;
                case GameAction.TakePoint:
                    GetUiElement<PointsValueView>().Add();
                    break;
                case GameAction.GameRestart:
                    GetUiElement<PointsValueView>().Clear();
                    break;
                case GameAction.GameEnd:
                    GetUiElement<PointsValueView>().Hide();
                    GetUiElement<ConversionDataView>().Show();
                    break;
                case GameAction.GameStart:
                    GetUiElement<ConversionDataView>().Hide();
                    break;
            }
        }
    }
}