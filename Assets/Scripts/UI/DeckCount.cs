using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckCount : MonoBehaviour
{
    private enum deck
    {
        preDraw,
        used
    }
    private List<GameObject> selectedDeck;
    [SerializeField] private deck deckcount;
    [SerializeField] TextMeshProUGUI countText;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (deckcount == deck.preDraw)
        {
            if(DeckManager.Instance.preDrawCards == null) { return; }
            selectedDeck = DeckManager.Instance.preDrawCards;
            countText.text = selectedDeck.Count.ToString();
        }
        else
        {
            if (DeckManager.Instance.usedCards == null) { return; }
            selectedDeck = DeckManager.Instance.usedCards;
            countText.text = selectedDeck.Count.ToString();



        }
    }
}
