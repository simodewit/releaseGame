using UnityEngine;
using UnityEngine.UI;

public class OptionInWheel : MonoBehaviour
{
    public GameObject gunToInstantiate;

    Button button;
    
    public void Start()
    {
        button = GetComponent<Button>();
    }

    public void CheckIfSelected()
    {
        if (gunToInstantiate == null)
        {
            return;
        }
    }
}
