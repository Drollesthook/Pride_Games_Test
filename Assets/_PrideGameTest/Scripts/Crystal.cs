using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, ICollected
{
    
    public void Collect() {
        SoftCurrencyController.Instance.AddCrystal();
        UIController.Instance.UpdateMenuTexts();
        gameObject.SetActive(false);
    }
}
