using UnityEngine;

public class Coin : MonoBehaviour, ICollected
{
   void Update() {
      transform.Rotate(Vector3.right * 100 * Time.deltaTime, Space.Self);  //Jazz for my soul
   }
   
   public void Collect() {
      SoftCurrencyController.Instance.AddCoin();
      UIController.Instance.UpdateMenuTexts();
      gameObject.SetActive(false);
   }
}
