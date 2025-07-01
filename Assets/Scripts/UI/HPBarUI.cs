using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class HPBarUI : MonoBehaviour
{
    private Damagable status;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI healthText;


    public void Init(Damagable damagable)
    {
        status = damagable;
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (status == null) return;

        float ratio = status.PercentHP;
        image.fillAmount = ratio;
        healthText.text = $"{status.CurrentHP} / {status.MaxHP}";
    }
}
