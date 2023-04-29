using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Paralax Object", menuName = "Paralax")]
public class ParalaxSprites : ScriptableObject
{
    [SerializeField] private Sprite[] sprite;
}
