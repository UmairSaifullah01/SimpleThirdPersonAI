using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabView : MonoBehaviour
{
    public TabEntity[] tabEntities;

    void OnEnable()
    {
        ActivateTab(0);
    }

    // Used In Buttons Of Tabs(TabEntity) as Reference 
    // ReSharper disable once MemberCanBePrivate.Global
    public void ActivateTab(int index)
    {
        for (var i = 0; i < tabEntities.Length; i++)
        {
            var entity = tabEntities[i];
            entity.tabPanel.SetActive(i == index);
            entity.triggerButton.interactable = i != index;
        }
    }


    [Serializable]
    public class TabEntity
    {
        public GameObject tabPanel;
        public Button triggerButton;
    }
}