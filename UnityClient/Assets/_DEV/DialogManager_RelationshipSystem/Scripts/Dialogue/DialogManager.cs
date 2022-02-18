using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using UnityEngine.EventSystems;


public class DialogManager : MonoBehaviour
{
    #region Singleton DialogM
    public static DialogManager instance;
    private void Awake()
    {
        instance = this;
    }

    public static DialogManager GetInstance()
    {
        return instance;
    }
    #endregion

    private Text[] choices;
    public GameObject[] choiceButtons;

    //checks if dialogue panel is open
    public bool isOpen = false;
    public Animator animator;

    //controller for relationship slider
    public RelationshipSliderController relationshipManager;

    //dialogue panel + text
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;

    //current dialogue
    private Story currentStory;

    private bool choiceSelected = false;

    void Start()
    {
        choices = new Text[choiceButtons.Length];
        int index = 0;
        foreach (GameObject choice in choiceButtons)
        {
            choices[index] = choice.GetComponentInChildren<Text>();
            index++;
        }
    }

    private void Update()
    {
        //checks if story needs to continue or if a choice has been made
        if (Input.GetKeyDown(KeyCode.D) || choiceSelected)
        {
            ContinueStory();
            choiceSelected = false;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        isOpen = true;
        animator.SetBool("openDialogue", isOpen);
        currentStory = new Story(inkJSON.text);
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    private void ContinueStory()
    {

        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            EndDialog();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
            int index = 0;
            foreach (Choice choice in currentChoices)
            {
                choiceButtons[index].gameObject.SetActive(true);
                choices[index].text = choice.text;
                index++;
            }

            for (int i = index; i < choiceButtons.Length; i++)
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
    }

    void EndDialog()
    {
        isOpen = false;
        animator.SetBool("openDialogue", isOpen);
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        choiceSelected = true;
        if (choiceIndex == 0)
        {
            relationshipManager.updateRelationshipBar(50);
        } else
        {
            relationshipManager.updateRelationshipBar(-50);
        }
    }

    IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choiceButtons[0].gameObject);
    }

}
