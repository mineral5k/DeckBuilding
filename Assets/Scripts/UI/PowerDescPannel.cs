using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerDescPannel : MonoBehaviour
{
    private Power allocatedPower;
    [SerializeField] private Image powerIcon;
    [SerializeField] private TextMeshProUGUI powerName;
    [SerializeField] private TextMeshProUGUI powerDesc;

    public void SetLocation(Damagable damagable)
    {
        if (damagable is Monster)
        {
            transform.DOLocalMoveX(-3f, 0);
        }
    }

    public void AllocatePower(Power power)
    {
        allocatedPower = power;
        UIUpdate();
    }

    public void UIUpdate()
    {
        powerIcon.sprite = Resources.Load<Sprite>("Icons/Power/" + allocatedPower.IconName);
        powerName.text = allocatedPower.PowerName;
        powerDesc.text = allocatedPower.Description;
    }
}
