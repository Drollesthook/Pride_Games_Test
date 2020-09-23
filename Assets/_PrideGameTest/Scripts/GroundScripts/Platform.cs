using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Coin _coin = default;
    [SerializeField] Crystal _crystal = default;

    public void SpawnCoin() {
        _coin.gameObject.SetActive(true);
    }

    public void SpawnCrystal() {
        _crystal.gameObject.SetActive(true);
    }

    public void reset() {
        _coin.gameObject.SetActive(false);
        _crystal.gameObject.SetActive(false);
    }
}
