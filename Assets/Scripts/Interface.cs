using UnityEngine;

public class Interface : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject T_FindLetter;
    private GameObject P_FadePanel;
    private GameObject B_Restart;
    private void Awake()
    {
        B_Restart = GameObject.Find("B_Restart");
        Canvas = GameObject.Find("Canvas");
        T_FindLetter = GameObject.Find("T_FindLetter");
        P_FadePanel = GameObject.Find("P_FadePanel");
        P_FadePanel.GetComponent<CanvasGroup>().alpha = 1;
        T_FindLetter.GetComponent<CanvasGroup>().alpha = 0;
    }
    public void Start()
    {
        B_Restart.transform.localPosition = new Vector2(0, 700);
        Canvas = GameObject.Find("Canvas");
        Canvas.GetComponent<ChooseSymbolGame>().StartGameLogic(T_FindLetter);        
    }
    public void QuitApp() // выход из приложения
    {
        Application.Quit();
    }
}
