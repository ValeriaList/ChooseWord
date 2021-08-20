using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChooseSymbolGame : MonoBehaviour
{
    [SerializeField]
    private SO_Elements Letters;

    [SerializeField]
    private SO_Elements Numbers;

    [SerializeField]
    private int NmbrLevel = 0;

    [SerializeField]
    private string TrueAnswer;

    [SerializeField]
    private GameObject[] Icons_pref;

    [SerializeField]
    private List<Sprite> AllElements;

    public GameObject ParentPanel;

    public void Start()
    {
        NmbrLevel += 3;
        FillingFields(Letters, Numbers);
        CreateIcons();
        RandTrueAnswer(NmbrLevel);

        GameObject B_Restart = GameObject.Find("B_Restart");
        B_Restart.transform.localPosition = new Vector2(0, 700);
    }

    private void FillingFields(SO_Elements letters, SO_Elements numbers)
    {
        AllElements = new List<Sprite>();

        for (int i = 0; i < letters.ElementsSprite.Length; i++)
        {
            AllElements.Add(letters.ElementsSprite[i]);
        }

        for (int i = 0; i < numbers.ElementsSprite.Length; i++)
        {
            AllElements.Add(numbers.ElementsSprite[i]);
        }
    }

    private void CreateIcons() // создание иконок
    {
        List<Sprite> CopyAllElements = new List<Sprite>();

        foreach (Sprite i in AllElements)
            CopyAllElements.Add(i);

        Icons_pref = new GameObject[NmbrLevel];
        if (NmbrLevel <= 9)
        {
            for (int i = 0; i < Icons_pref.Length; i++)
            {
                Icons_pref[i] = Instantiate(Resources.Load("Button", typeof(GameObject)), transform, false) as GameObject;
                Icons_pref[i].transform.SetParent(ParentPanel.transform);

                var buttonTransform = Icons_pref[i].transform;
                var image = buttonTransform.GetChild(0);

                Sprite Item = RandIcons(CopyAllElements);

                if (CopyAllElements.Contains(Item))
                    CopyAllElements.Remove(Item);

                image.GetComponent<Image>().sprite = Item;
                Icons_pref[i].name = Item.name;
                int j = i;
                Icons_pref[i].GetComponent<Button>().onClick.AddListener(() => Examination(j));
            }
        }
    }

    private Sprite RandIcons(List<Sprite> copyAllElements)
    {
        System.Random Random = new System.Random();

        Sprite Item = copyAllElements.OrderBy(s => Random.NextDouble()).First();

        return Item;
    }

    private void Update()
    {
        if (NmbrLevel > 9)
        {
            GameObject B_Restart = GameObject.Find("B_Restart");
            B_Restart.transform.localPosition = new Vector2(0,0);
            NmbrLevel = 0;
        }
    }

    private void Examination(int Nmbr_ActiveButton)
    {
        if (Icons_pref[Nmbr_ActiveButton].name == TrueAnswer)
        {
            if (AllElements.Contains(Icons_pref[Nmbr_ActiveButton].GetComponent<Image>().sprite))
            {
                AllElements.Remove(Icons_pref[Nmbr_ActiveButton].GetComponent<Image>().sprite);
            }
            //анимация
            DestroyIcons();
            NmbrLevel += 3;
            CreateIcons();
            RandTrueAnswer(Nmbr_ActiveButton);
        }
        else
        {
            if(NmbrLevel <= 9)
            {
                // анимация
                DestroyIcons();
                CreateIcons();
                RandTrueAnswer(Nmbr_ActiveButton);
            }
           
        }
    }

    private void DestroyIcons()
    {
        for (int i = 0; i < Icons_pref.Length; i++)
            Destroy(Icons_pref[i]);
    }

    private void RandTrueAnswer(int NmbrLvl) // выбираем символ для поиска
    {
        System.Random Random = new System.Random();
        

        if(NmbrLevel <= 9)
        {
            int Symbol = Random.Next(NmbrLvl);

            GameObject T_FindLetter = GameObject.Find("T_FindLetter");
            T_FindLetter.GetComponent<Text>().text = "Find " + Icons_pref[Symbol].name;

            TrueAnswer = Icons_pref[Symbol].name;
        }
    }
}