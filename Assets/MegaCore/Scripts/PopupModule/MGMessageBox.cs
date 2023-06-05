using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace MegaCore.Popup
{

    public class MGMessageBox : MGMessageBase
    {
        public GameObject _panel;
        internal RectTransform _panelTrs;
        internal Text _messageTxt;
        internal Button _tapBttn;

        public float _stayDuration = 3f;
        public float _topAnchor = 0f;
        public float _bottomAnchor = -100f;
        public float _speed = 4f;
        public AnimationCurve _showAnimCurve;
        public AnimationCurve _hideAnimCurve;

        Coroutine _showCoroutine = null;

        Action _tapAction;

        private void Awake()
        {
            _panelTrs = _panel.GetComponent<RectTransform>();
            _messageTxt = _panelTrs.GetChild(0).GetComponent<Text>();
            _tapBttn = _panel.GetComponent<Button>();

            _tapBttn.onClick.AddListener(OnTapped);
        }

        private void OnEnable()
        {
            _panelTrs.anchoredPosition = new Vector2(0f, _bottomAnchor);
        }

        public void Show(string message, Action tapAction)
        {
            if (_showCoroutine != null)
                StopCoroutine(_showCoroutine);
            _messageTxt.text = message;
            _tapAction = tapAction;
            _showCoroutine = StartCoroutine(ShowCo());
        }

        IEnumerator ShowCo()
        {
            _panelTrs.gameObject.SetActive(true);

            for (float p = 0f; p <= 1f ; p += Time.deltaTime * _speed)
            {
                _panelTrs.anchoredPosition = Vector2.Lerp(new Vector2(0f, _bottomAnchor), new Vector2(0f, _topAnchor), _showAnimCurve.Evaluate(p));
                yield return null;
            }

            yield return new WaitForSecondsRealtime(_stayDuration);

            for (float p = 0f; p <= 1; p += Time.deltaTime * _speed)
            {
                _panelTrs.anchoredPosition = Vector2.Lerp(new Vector2(0f, _topAnchor), new Vector2(0f, _bottomAnchor), _hideAnimCurve.Evaluate(p));
                yield return null;
            }

            yield return new WaitForSecondsRealtime(0.5f);

            _panelTrs.gameObject.SetActive(false);

            _showCoroutine = null;
        }

        public void OnTapped()
        {
            _tapAction?.Invoke();
            if (_showCoroutine != null)
            {
                StopCoroutine(_showCoroutine);
                _showCoroutine = null;
            }
            _panelTrs.gameObject.SetActive(false);
            _panelTrs.anchoredPosition = new Vector2(0f, _bottomAnchor);
        }

    }

}
