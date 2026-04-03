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
    [SerializeField] private GameObject shieldImage;
    [SerializeField] private TextMeshProUGUI shieldText;
    private Color shieldColor = new Color32(55, 171, 225,255);


    public void Init(Damagable damagable)
    {
        status = damagable;
        status.OnHPChanged += UpdateUI;
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (status == null) return;

        float ratio = status.PercentHP;
        image.fillAmount = ratio;
        healthText.text = $"{status.CurrentHP} / {status.MaxHP}";

        if (status.shield<=0)                      //방어도 존재 시 아이콘 띄우고 색상 변경.
        {
            shieldImage.SetActive(false);
            image.color = Color.red;
            shieldText.text = "";
        }
        else
        {
            shieldImage.SetActive(true);
            image.color = shieldColor;
            shieldText.text = $"{status.shield}";
        }
    }
}
