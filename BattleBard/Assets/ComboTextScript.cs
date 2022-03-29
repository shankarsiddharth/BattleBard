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

    void Test(ComboBase eff, int level, Vector3 vec)
    {
        if (eff != null)
        {
            GetComponent<Text>().text = eff.gameObject.name;
            GetComponent<Text>().SetAllDirty();
        }
    }
}
