using _project.Scripts.Ui.Base;
using AppsFlyerSDK;
using TMPro;
using UnityEngine;

namespace _project.Scripts.Ui
{
    public class ConversionDataView : UiElement, IAppsFlyerConversionData
    {
        [SerializeField] private TextMeshProUGUI _conversionDataText;

        private void Start()
        {
            AppsFlyer.getConversionData(gameObject.name);
        }

        public void onConversionDataSuccess(string conversionData)
        {
            _conversionDataText.SetText(conversionData);
        }

        public void onConversionDataFail(string error)
        {
        }

        public void onAppOpenAttribution(string attributionData)
        {
        }

        public void onAppOpenAttributionFailure(string error)
        {
        }
    }
}