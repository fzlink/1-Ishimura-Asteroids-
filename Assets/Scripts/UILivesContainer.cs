using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILivesContainer : MonoBehaviour
{
    [SerializeField] private Image LiveSprite;
    [SerializeField] private PlayerController PlayerController;

    private List<Image> LiveUIElements;
    private void Start()
    {
        LiveUIElements = new List<Image>();
        LiveUIElements.Add(LiveSprite);
        for (int i = 0; i < PlayerController.GetHealthPoint()-1; i++)
        {
            var liveUI = Instantiate(LiveSprite, transform);
            LiveUIElements.Add(liveUI);
        }
        
        PlayerController.OnDestroyed += OnPlayerDestroyed;
    }

    private void OnPlayerDestroyed(int hp)
    {
        for (int i = 0; i < LiveUIElements.Count; i++)
        {
            LiveUIElements[i].gameObject.SetActive(i < hp);
        }
    }
}
