using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum type
{
    car,
    shop,
    item
}


public class Interactable : MonoBehaviour
{
    [Tooltip("The type of interactable this is")]
    public type type;
}
