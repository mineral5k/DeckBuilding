using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeywardDescPannel : MonoBehaviour
{

    [SerializeField] private List<GameObject> pannels = new List<GameObject>();
    [SerializeField] private List<TextMeshProUGUI> names = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> descs = new List<TextMeshProUGUI>();

    public void ShowPannels(List<string[]> keywards)
    {
        for (int i = 0; i < keywards.Count; i++)
        {
            if(i>2) break;
            pannels[i].SetActive(true);
            names[i].text = keywards[i][0];
            descs[i].text= keywards[i][1];
        }
    }

    public void HidePannels()
    {
        foreach (GameObject pannel in pannels)
        {
            pannel.SetActive(false);
        }
    }
}
