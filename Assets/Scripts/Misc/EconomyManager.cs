using SceneManagement;
using UnityEngine;
using TMPro;

namespace Misc
{
    public class EconomyManager : Singleton<EconomyManager>
    {
        private TextMeshProUGUI _coinText;
        private int _currentGold = 0;

        private const string COIN_AMOUNT_TEXT = "GoldCoinAmountText";

        public void UpdateCurrentGold()
        {
            _currentGold++;
            
            if (_coinText == null)
            {
                _coinText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TextMeshProUGUI>();  
            }
            
            _coinText.text = _currentGold.ToString("D3");
        }
    }
}
