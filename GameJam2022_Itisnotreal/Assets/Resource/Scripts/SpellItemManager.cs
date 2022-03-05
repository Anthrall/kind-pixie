using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellItemManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    public Spells spell;
    private GameObject child;
    private Color rgb;
    public Sprite _none, _enter, _click, _choose, _spelling;
    public bool isChoosing, isSpellRigth;
    private int how;
    public SpellsManager spellManager;
   

    private void Start()
    {
        child = this.gameObject.transform.GetChild(0).gameObject;
       
    }

    public void PointEnter()
    {
        if(!isSpellRigth)
        child.GetComponent<Image>().sprite = _enter ;  
    }

    public void PointExit()
    {
        if(isChoosing)
            child.GetComponent<Image>().sprite = _choose;
        else if (isSpellRigth)
            child.GetComponent<Image>().sprite = _spelling;
        else
            child.GetComponent<Image>().sprite = _none;
    }

    public void PointClick()
    {
        if (!isSpellRigth)
        {
            child.GetComponent<Image>().sprite = _click;
            
        }
    }

    public void EndSpell()
    {
        child.GetComponent<Image>().sprite = _none;
    }

    public void SpellReady()
    {
        child.GetComponent<Image>().sprite = _spelling;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!spellManager.readyToSpell)
        {
            if (!isChoosing)
            {
                how = spellManager.how;
                isChoosing = true;
                spellManager.ChooseSpells(transform.parent.gameObject, spellManager.how);
                if (spellManager.how == 1)
                {
                    FindObjectOfType<AudioManager>().Play("ButtonClick");
                    spellManager.how = 2;
                }
                else
                {
                    spellManager.how = 1;

                }

            }
            else
            {
                isChoosing = false;
                spellManager.ChooseSpells(null, how);
                spellManager.how = how;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
        if(!spellManager.readyToSpell)
            PointEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!spellManager.readyToSpell)
            PointExit();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!spellManager.readyToSpell)
            PointEnter();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!spellManager.readyToSpell)
            PointClick();
    }
}
