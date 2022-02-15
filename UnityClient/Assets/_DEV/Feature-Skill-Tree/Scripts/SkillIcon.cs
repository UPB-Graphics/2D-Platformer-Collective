using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace SkillTree
{
    public class SkillIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string Guid;
        public bool pointerOver = false;
        public SkillTreeUI skillTreeUI;
        public SkillData data;

        public Image iconSprite;
        public TextMeshProUGUI currentLevel;
        public TextMeshProUGUI maxLevel;

        public LineRenderer line;
        public Gradient lockedColor;
        public Gradient unlockedColor;


        // Start is called before the first frame update
        void Start()
        {
            skillTreeUI = GetComponentInParent<SkillTreeUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (pointerOver)
            {
                skillTreeUI.ShowDetails(pointerOver, (Vector2)Input.mousePosition, data);
            }
        }

        public void BuySkill()
        {
            Debug.Log(data.skillName + " pressed");
            //send to backend
            PressedSkillData.currentIcon = this;
        }

        public void UpdateLevel(int newLevel)
        {
            if (newLevel == -1)
            {
                Debug.Log("Out of learning points");
                return;
            }
            currentLevel.text = newLevel.ToString();
        }

        public void SetLock(bool stat)
        {
            GetComponentInChildren<Button>().interactable = !stat;
            if (line != null)
            {
                line.colorGradient = stat ? lockedColor : unlockedColor;
            }
        }

        public void SetUI(string guid)
        {
            SetLock(true);
            Guid = guid;
            if (data == null)
            {
                foreach (var t in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    t.gameObject.SetActive(false);
                }
                return;
            }
            iconSprite.sprite = data.icon;
            currentLevel.text = "0";
            maxLevel.text = data.maxLevels.ToString();

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            pointerOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            pointerOver = false;
            skillTreeUI.ShowDetails(false, Vector2.zero, data);
        }
    }
}