using TMPro;
using UnityEngine;

namespace MYgame.Scripts.Window
{
    /// <summary>
    /// The status of the single brick
    /// </summary>
    public class BrickStatusUI : MonoBehaviour
    {
        [SerializeField]
        protected StaticGlobalDel.EBrickColor _color;
        [SerializeField]
        protected TextMeshProUGUI _text;
        [SerializeField]
        protected TextMeshProUGUI _MaxShowText;
        [SerializeField]
        protected bool _showMaxNum;

        protected int _maxNumber = 1;
        protected int _currentNumber = 0;

        public StaticGlobalDel.EBrickColor color => _color;

        #region Activation

        public void Activate()
        {
            if (!_showMaxNum)
                _currentNumber = 0;
            gameObject.SetActive(true);
        }

        public void Inactivate()
        {
            gameObject.SetActive(false);
            _maxNumber = 1;
        }

        #endregion

        public void SetMaxNumber(int number)
        {
            _maxNumber = number;
            _currentNumber = 0;
        }

        public virtual void SetNumber(int number)
        {
            _text.text =
                _showMaxNum ?
                    $"{Mathf.Min(number, _maxNumber)}/{_maxNumber}" :
                    $"{number}";

            ShowMaxText(number == _maxNumber);
        }

        public void IncreaseNumber()
        {
            ++_currentNumber;
            _text.text = _currentNumber.ToString();
        }

        public void ShowMaxText(bool show)
        {
            _MaxShowText.gameObject.SetActive(show);
        }
    }
}
