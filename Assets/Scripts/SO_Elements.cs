using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Some elements", fileName = "Elements")]
public class SO_Elements : ScriptableObject
{
    [SerializeField]
    private string[] elementsName;

    public string[] ElementsName => elementsName;

    [SerializeField]
    private Sprite[] elementsSprite;

    public Sprite[] ElementsSprite => elementsSprite;
}