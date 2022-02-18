using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;

    public void TriggerDialog()
    {
        if (inkJSON != null)
        {
            DialogManager.instance.EnterDialogueMode(inkJSON);
        }
    }

}
