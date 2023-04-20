using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TowerDefence.UI
{
    public class PriceImprovement : MonoBehaviour
    {
        [SerializeField] private TMP_Text priceSpeedText;
        [SerializeField] private TMP_Text priceDamageText;
        [SerializeField] private TMP_Text priceDamageDistanceText;

        public void AssignPrice(int _improvementSpeedPrice, int _improvementDamagePrice, int _improvementDamageDistancePrice)
        {
            priceSpeedText.text = _improvementSpeedPrice.ToString();
            priceDamageText.text = _improvementDamagePrice.ToString();
            priceDamageDistanceText.text = _improvementDamageDistancePrice.ToString();
        }
    }
}
