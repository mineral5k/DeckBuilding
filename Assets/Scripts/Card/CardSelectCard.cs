using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardSelectCard : Card
{
    private CardSelect cardSelect;
    public void Init(CardSelect cs)
    {
        cardSelect = cs;
    }

    void OnMouseEnter()
    {
        // 마우스를 올리면 오브젝트를 크게 만듦
        transform.DOScale(2.5f, 0.1f);
        pannels.ShowPannels(KeywardDescs);
    }

    private void OnMouseDown()
    {
    }

    private void OnMouseUp()
    {
        cardSelect.ClickACard(cardData.cardId);
    }

    void OnMouseExit()
    {
        // 마우스가 나가면 원래 크기로 복구
        transform.DOScale(2.1f, 0.1f);
        pannels.HidePannels();
    }

    
}
