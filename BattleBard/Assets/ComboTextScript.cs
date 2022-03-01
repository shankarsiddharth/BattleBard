using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Instance.onDrumComboCompleted.AddListener(Test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Test(ComboEffect eff, Vector3 vec, bool affectsA, bool affectsE)
    {
        if (eff != null)
        {
            GetComponent<Text>().text = eff.gameObject.name;
            GetComponent<Text>().SetAllDirty();
        }
    }
}
