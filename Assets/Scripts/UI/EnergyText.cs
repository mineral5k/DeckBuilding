using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI energyText;


    public void UpdateEnergyUI()
    {
        energyText.text = GameManager.Instance.Player.Status.MaxEnergy.ToString() + "/" + GameManager.Instance.Player.Status.Energy.ToString();
    }
}
