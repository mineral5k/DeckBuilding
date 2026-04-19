using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerIcon : MonoBehaviour
{
    private Power allocatedPower;
    [SerializeField] private TextMeshProUGUI powerAmount;
    private Image IconImage;


    public void AllocatePower(Power power)
    {
        allocatedPower = power;
        UpdateUI();
    }

    public void UpdateUI()
    {
        IconImage = GetComponent<Image>();
        IconImage.sprite = Resources.Load<Sprite>("Icons/Power/" + allocatedPower.IconName);
        powerAmount.text = allocatedPower.amount.ToString();
    }
}
