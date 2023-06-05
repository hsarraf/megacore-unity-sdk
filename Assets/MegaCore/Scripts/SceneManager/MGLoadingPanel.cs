using UnityEngine;


namespace MegaCore.SceneManager
{

    public class MGLoadingPanel : MonoBehaviour
    {

        public GameObject _loadingPanel;


        public Transform _loadingBackgroundGrp;

        public Transform _loadingBannerGrp;

        public GameObject _loadingScroll;

        public Transform _loadingHintGrp;

        public MGLoadingBar _loadingBar;
        public GameObject _loadingPercentTxt;

        public Transform _loadingTextGrp;


        private void Awake()
        {
            //_loadingBackgroundGrp = GameObject.Find("MG: LoadingBackgroundGrp").transform;
            //_loadingBannerGrp = GameObject.Find("MG: LoadingBannerGrp").transform;
            //_loadingScroll = GameObject.Find("MG: LoadingScroll");
            //_loadingHintGrp = GameObject.Find("MG: LoadingHintGrp").transform;
            //_loadingBar = GameObject.Find("MG: LoadingBar").GetComponent<MGLoadingBar>();
            //_loadingTextGrp = GameObject.Find("MG: LoadingTextGrp").transform;

            Hide();
        }

        public void Show(LoadingBackgroungPanel panelBackground = 0, LoadingSplash splash = 0, LoadingHint hint = 0, LoadingText loadingText = 0,
            bool showLoadingBar = true, bool showLoadingScroll = true, bool showLoadingPercent = true)
        {
            Transform panelBackgroundTrs =_loadingBackgroundGrp.Find(panelBackground.ToString());
            if (panelBackgroundTrs != null)
                panelBackgroundTrs.gameObject.SetActive(true);

            Transform splashTrs = _loadingBannerGrp.Find(splash.ToString());
            if (splashTrs != null)
                splashTrs.gameObject.SetActive(true);

            Transform loadingHintTrs = _loadingHintGrp.Find(hint.ToString());
            if (loadingHintTrs != null)
                loadingHintTrs.gameObject.SetActive(true);

            Transform loadingTextTrs = _loadingTextGrp.Find(loadingText.ToString());
            if (loadingTextTrs != null)
                loadingTextTrs.gameObject.SetActive(true);


            _loadingScroll.SetActive(showLoadingScroll);

            _loadingBar.ShowBar(showLoadingBar);
            _loadingBar.ShowPercent(showLoadingPercent);

            _loadingPanel.SetActive(true);
        }

        public void Hide()
        {
            _loadingPanel.SetActive(false);

            foreach (Transform child in _loadingBackgroundGrp)
                child.gameObject.SetActive(false);

            foreach (Transform child in _loadingBannerGrp)
                child.gameObject.SetActive(false);

            foreach (Transform child in _loadingHintGrp)
                child.gameObject.SetActive(false);

            foreach (Transform child in _loadingTextGrp)
                child.gameObject.SetActive(false);

            _loadingBar.ShowBar(false);
            _loadingBar.ShowPercent(false);
            _loadingBar.UpdateBar(0f);
        }

        public void UpdateLoadingBar(float value)
        {
            _loadingBar.UpdateBar(value);
            _loadingBar.UpdatePercent(value);
        }


    }

}
