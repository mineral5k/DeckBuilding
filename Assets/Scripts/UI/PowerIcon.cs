using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerIcon : MonoBehaviour
{
    private Power allocatedPower;
    [SerializeField] private TextMeshProUGUI powerAmount;


    public void Init(Power power)
    {
        allocatedPower = power;
        UpdateUI();
    }

    public void UpdateUI()
    {

    }
}
