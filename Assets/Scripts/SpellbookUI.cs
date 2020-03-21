using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellbookUI : MonoBehaviour
{
    [Header("Spell Formatting")]
    public Image spellImage;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI pageCount;
    public GameObject[] symbols;
    [Header("Spell List")]
    public SpellList spellList;
    [Header("Page Buttons")]
    public Button nextPageButton;
    public Button prevPageButton;

    private int currentPage;
    private int totalPages;
    // Start is called before the first frame update
    void Start()
    {
        currentPage = 1;
        totalPages = spellList.getSpellCount();

        prevPageButton.interactable = false;

        formatForSpell(spellList.getSpell(currentPage-1));
    }

    private void formatForSpell(SpellDescription sD)
    {
        spellImage.sprite = sD.spellSymbol;
        title.text = sD.spellName;
        description.text = sD.spellDesc;
        pageCount.text = $"{currentPage} / {totalPages}";

        foreach(GameObject go in symbols)
        {
            go.SetActive(false);
        }

        foreach(int i in sD.sequence)
        {
            symbols[i].SetActive(true);
        }
    }

    public void nextPage()
    {
        if(currentPage == 1)
        {
            prevPageButton.interactable = true;
        }

        currentPage++;

        formatForSpell(spellList.getSpell(currentPage-1));

        if(currentPage == totalPages)
        {
            nextPageButton.interactable = false;
        }
    }

    public void prevPage()
    {
        if (currentPage == totalPages)
        {
            nextPageButton.interactable = true;
        }

        currentPage--;

        formatForSpell(spellList.getSpell(currentPage-1));

        if (currentPage == 1)
        {
            prevPageButton.interactable = false;
        }
    }


}
