using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionInWheel : MonoBehaviour , IPointerEnterHandler
{
    public GameObject gunToInstantiate;
    public OptionsWheel optionsWheel;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        optionsWheel.optionInWheel = this;
    }
}
