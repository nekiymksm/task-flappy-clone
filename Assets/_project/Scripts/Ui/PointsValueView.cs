using _project.Scripts.Ui.Base;
using TMPro;
using UnityEngine;

namespace _project.Scripts.Ui
{
    public class PointsValueView : UiElement
    {
        [SerializeField] private TextMeshProUGUI _pointsValueText;

        private int _pointsCounter;

        protected override void OnShow()
        {
            Clear();
        }

        public void Add()
        {
            _pointsCounter++;
            Set();
        }

        public void Clear()
        {
            _pointsCounter = 0;
            Set();
        }

        private void Set()
        {
            _pointsValueText.SetText(_pointsCounter.ToString());
        }
    }
}