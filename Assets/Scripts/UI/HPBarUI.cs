using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
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

    [SerializeField] private List<PowerIcon> powerIcons;
    [SerializeField] private List<PowerDescPannel> powerDescPannels;



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

        UpdatePowerIcons();
        
    }

    public void UpdatePowerIcons()
    {
        foreach (PowerIcon icon in powerIcons)
        {
            icon.gameObject.SetActive(false);
        }

        for (int i = 0; i < status.powers.Count; i++)
        {
            if (i>=12) break;
            powerIcons[i].gameObject.SetActive(true);
            powerIcons[i].AllocatePower(status.powers[i]);       // TODO : 추후 파워가 13개 이상일때 아이콘 추가하는 코드 필요 
        }
    }

    public void ShowAllPowerDescPannels()
    {
        for (int i = 0; i < status.powers.Count; i++)
        {
            if (i >= 5) break;
            powerDescPannels[i].gameObject.SetActive(true);
            powerDescPannels[i].AllocatePower(status.powers[i]);       
        }
    }

    public void HideAllPowerDescPannels()
    {
        foreach(PowerDescPannel pannel in powerDescPannels)
        {
            pannel.gameObject.SetActive(false);
        }
    }
}
