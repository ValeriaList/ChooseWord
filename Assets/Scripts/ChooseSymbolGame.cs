using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChooseSymbolGame : MonoBehaviour
{
    [SerializeField] private SO_Elements Letters;
    [SerializeField] private SO_Elements Numbers;
    private GameObject ParentPanel;
    private int NmbrLevel = 0;
    private string TrueAnswer;
    private GameObject[] Icons_pref;
    private List<Sprite> AllElements;
    private GameObject P_FadePanel;
    private void Awake()
    {
        P_FadePanel = GameObject.Find("P_FadePanel");
        ParentPanel = GameObject.Find("P_Game");
    }
    public void StartGameLogic(GameObject T_FindLetter)
    {
        P_FadePanel.GetComponent<FadePanel>().FadeOut(1f);
        P_FadePanel.GetComponent<FadePanel>().FadeIn(1f);
        NmbrLevel += 3;
        FillingFields(Letters, Numbers);
        CreateIcons();
        RandTrueAnswer(NmbrLevel);
        StartCoroutine(StartGame(P_FadePanel, T_FindLetter));
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
                Icons_pref[i] = Instantiate(Resources.Load("Prefabs/Icon", typeof(GameObject)), transform, false) as GameObject;
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
            NmbrLevel = 0;
            GameObject B_Restart = GameObject.Find("B_Restart");
            B_Restart.transform.localPosition = new Vector2(0,0);

            P_FadePanel.GetComponent<CanvasGroup>().alpha = 1;
            P_FadePanel.GetComponent<FadePanel>().FadeIn(1f);
            P_FadePanel.GetComponent<FadePanel>().FadeOut(1f);
        }
    }
    private void Examination(int Nmbr_ActiveButton)
    {
        if (Icons_pref[Nmbr_ActiveButton].name == TrueAnswer)
        {
            StartCoroutine(BounceTrue(Icons_pref[Nmbr_ActiveButton]));
        }
        else
        {
            if(NmbrLevel <= 9)
            {
                StartCoroutine(EaselnBounse(Icons_pref[Nmbr_ActiveButton]));
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
    private IEnumerator StartGame(GameObject P_FadePanel, GameObject T_FindLetter) // начало игры
    {
        yield return new WaitForSeconds(0.3f);
        P_FadePanel.GetComponent<BounceEffect>().Bounce(Icons_pref);
        T_FindLetter.GetComponent<FadePanel>().FadeOut(1f);
        T_FindLetter.GetComponent<FadePanel>().FadeIn(1f);
    }
    private IEnumerator EaselnBounse(GameObject icon) // неправильный ответ
    {
        Vector3 PosImageBounce = new Vector3(2,0,2);
        yield return null;
        P_FadePanel.GetComponent<BounceEffect>().ImageBounce(icon, PosImageBounce);
    }
    private IEnumerator BounceTrue(GameObject icon)  //правильный ответ
    {
        Vector3 PosEffect = icon.transform.position;
        Vector3 PosImageBounce = new Vector3(0, 2, 0);
        icon.GetComponent<AudioSource>().Play();
        P_FadePanel.GetComponent<BounceEffect>().ImageBounce(icon, PosImageBounce);
        GameObject SO_Stars = P_FadePanel.GetComponent<SpecialEffectsHelper>().StarsEffects(PosEffect);
        yield return new WaitForSeconds(1f);
        Destroy(SO_Stars);
        if (AllElements.Contains(icon.GetComponent<Image>().sprite))
        {
            AllElements.Remove(icon.GetComponent<Image>().sprite);
        }        
        DestroyIcons();
        NmbrLevel += 3;
        CreateIcons();
        RandTrueAnswer(NmbrLevel);
    }
}