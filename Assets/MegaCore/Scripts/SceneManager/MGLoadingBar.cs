using UnityEngine;
using UnityEngine.UI;

namespace MegaCore.SceneManager
{
    public class MGLoadingBar : MonoBehaviour
    {
        public GameObject _loadingPanel;

        public RectTransform _loadingBarTrs;
        public Text _percentTxt;

        public void ShowBar(bool active)
        {
            _loadingPanel.SetActive(active);
        }

        public void ShowPercent(bool active)
        {
            _percentTxt.gameObject.SetActive(active);
        }

        public void UpdateBar(float value)
        {
            if (_loadingBarTrs != null)
                _loadingBarTrs.localScale = new Vector2(value, 1f);
        }

        public void UpdatePercent(float value)
        {
            if (_percentTxt != null)
                _percentTxt.text = ((int)value).ToString() + "%";
        }

    }
}
