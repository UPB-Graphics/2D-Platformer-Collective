using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace SkillTree
{

    [System.Serializable]
    public class UILink
    {
        public RectTransform object1;
        public RectTransform object2;
        public LineRenderer line;
    }


    public class SkillTreeUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public GameObject fullUI;
        public RectTransform draggableZone;
        public RectTransform dragZone;
        private Vector2 lastMousePosition;

        public RectTransform detailObject;
        public TextMeshProUGUI skillName;
        public TextMeshProUGUI skillDesc;

        public List<UILink> skillLinks;
        public GameObject linePrefab;
        public new Camera camera;

        public SkillTreeContainer container;
        public GameObject treeSupportPrefab;
        public GameObject treeLevelPrefab;
        public GameObject skillIconPrefab;
        public GameObject treeRoot;

        public TextMeshProUGUI skillPointCounter;

        public Dictionary<string, GameObject> treeDict = new Dictionary<string, GameObject>();

        public bool redoTreeOnStart = false;

        void Start()
        {
            detailObject.gameObject.SetActive(false);
            fullUI.SetActive(false);
            if (redoTreeOnStart)
                CreateSkillTree();
            DrawEdges();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ShowTree();
            }
            DrawEdges();
        }

        public void ShowTree()
        {
            fullUI.SetActive(!fullUI.activeInHierarchy);
        }

        /// <summary>
        /// This method will be called on the start of the mouse drag
        /// </summary>
        /// <param name="eventData">mouse pointer event data</param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin Drag");
            lastMousePosition = eventData.position;
        }

        /// <summary>
        /// This method will be called during the mouse drag
        /// </summary>
        /// <param name="eventData">mouse pointer event data</param>
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 currentMousePosition = eventData.position;
            Vector2 diff = currentMousePosition - lastMousePosition;
            RectTransform rect = draggableZone;

            Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, 0);
            Vector3 oldPos = rect.position;
            rect.position = newPosition;
            if (!IsRectTransformInsideSreen(rect))
            {
                rect.position = oldPos;
            }
            lastMousePosition = currentMousePosition;
        }

        /// <summary>
        /// This method will be called at the end of mouse drag
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End Drag");
            //Implement your funtionlity here
        }

        /// <summary>
        /// This methods will check is the rect transform is inside the screen or not
        /// </summary>
        /// <param name="rectTransform">Rect Trasform</param>
        /// <returns></returns>
        private bool IsRectTransformInsideSreen(RectTransform rectTransform)
        {
            bool isInside = true;
            Vector3[] corners = new Vector3[4];
            Vector3[] otherCorners = new Vector3[4];
            Vector3[] cameraCorners = new Vector3[4];
            dragZone.GetWorldCorners(corners);
            rectTransform.GetWorldCorners(otherCorners);
            int visibleCorners = 0;
            int maxCorners = 0;

            cameraCorners[0] = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
            cameraCorners[1] = camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane));
            cameraCorners[2] = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
            cameraCorners[3] = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));
            Rect rect = new Rect(0, 0, Screen.width, Screen.height);
            foreach (Vector3 corner in corners)
            {
                if (rect.Contains(corner))
                {
                    visibleCorners++;
                }
            }
            foreach (Vector3 corner in otherCorners)
            {
                if (rect.Contains(corner))
                {
                    maxCorners++;
                }
            }
            //if (visibleCorners > 0 || maxCorners == 0)
            //{
            //    isInside = true;
            //}
            Debug.Log(corners[0].x + " " + corners[0].y + "\n" +
                corners[1].x + " " + corners[1].y + "\n" +
                corners[2].x + " " + corners[2].y + "\n" +
                corners[3].x + " " + corners[3].y + "\n");
            if (corners[0].x > cameraCorners[2].x || corners[0].y > cameraCorners[2].y ||
                corners[1].x > cameraCorners[3].x || corners[1].y < cameraCorners[3].y ||
                corners[2].x < cameraCorners[0].x || corners[2].y < cameraCorners[0].y ||
                corners[3].x < cameraCorners[1].x || corners[3].y > cameraCorners[1].y)
            {
                isInside = false;
            }
            Debug.Log(isInside);
            return isInside;
        }

        public void ShowDetails(bool status, Vector2 position, SkillData data)
        {
            if (data != null)
            {
                detailObject.gameObject.SetActive(status);
                skillName.text = data.skillName;
                skillDesc.text = data.description;
            }
            if (status)
                detailObject.anchoredPosition = position;
        }

        public void UpdateSkillPointCounter(int skillPoints)
        {
            skillPointCounter.text = $"{skillPoints} skill points available";
        }

        //private void OnValidate()
        //{
        //    CreateSkillTree(treeRoot);
        //    foreach (UILink l in skillLinks)
        //    {
        //        if (l.object1 != null && l.object2 != null && l.line == null)
        //        {
        //            l.line = Instantiate(linePrefab, dragZone, false).GetComponent<LineRenderer>();
        //        }
        //    }
        //}

        public void CreateSkillTree()
        {
            GameObject parent = treeRoot;
#if UNITY_EDITOR
            DestroySkillTree();
#else
        treeDict = new Dictionary<string, GameObject>();
        foreach (Transform t in parent.transform)
        {
            Destroy(t);
        }
#endif

            parent = Instantiate(treeSupportPrefab, parent.transform);
            string startNodeGuid = container.nodeLinks[0].baseNodeGuid;
            GameObject level = Instantiate(treeLevelPrefab, parent.transform);
            GameObject si = Instantiate(skillIconPrefab, level.transform);
            si.GetComponent<SkillIcon>().SetUI(startNodeGuid);
            treeDict.Add(startNodeGuid, si);
            List<NodeLinkData> nodes = container.nodeLinks.Where(x => x.baseNodeGuid == startNodeGuid).ToList();
            skillLinks = new List<UILink>();
            while (nodes.Count > 0)
            {
                level = Instantiate(treeLevelPrefab, parent.transform);
                List<NodeLinkData> children = new List<NodeLinkData>();
                foreach (NodeLinkData link in nodes)
                {
                    //Debug.Log(link.targetNodeGuid);
                    si = Instantiate(skillIconPrefab, level.transform);
                    si.GetComponent<SkillIcon>().data = container.skillTreeNodeData.First(x => x.GUID == link.targetNodeGuid).data;
                    si.GetComponent<SkillIcon>().SetUI(link.targetNodeGuid);
                    treeDict.Add(link.targetNodeGuid, si);
                    skillLinks.Add(new UILink { object1 = treeDict[link.baseNodeGuid].GetComponent<RectTransform>(), object2 = si.GetComponent<RectTransform>() });

                    //Debug.Log(container.nodeLinks.Where(x => x.baseNodeGuid == link.targetNodeGuid).Count());
                    children = children.Concat(container.nodeLinks.Where(x => x.baseNodeGuid == link.targetNodeGuid)).ToList();
                }
                nodes = children;
                //Debug.Log(nodes.Count);
            }
            DrawEdges();
        }

        public void DrawEdges()
        {
            foreach (UILink l in skillLinks)
            {
                if (l.object1 != null && l.object2 != null && l.line == null)
                {
                    l.line = Instantiate(linePrefab, l.object2.transform, false).GetComponent<LineRenderer>();
                    l.object2.GetComponent<SkillIcon>().line = l.line;
                    l.object2.GetComponent<SkillIcon>().line.colorGradient = l.object2.GetComponent<SkillIcon>().lockedColor;

                }
                if (l.line != null)
                {
                    l.line.SetPosition(0, (Vector3)(l.object1.transform.position));
                    l.line.SetPosition(1, (Vector3)(l.object2.transform.position));
                }
            }
        }

        public void DestroySkillTree()
        {
            GameObject parent = treeRoot;
            treeDict = new Dictionary<string, GameObject>();
            for (int i = parent.transform.childCount; i > 0; --i)
                DestroyImmediate(parent.transform.GetChild(0).gameObject);
        }

        public void SetConstraints(bool enabled)
        {
            treeRoot.GetComponentInChildren<VerticalLayoutGroup>().enabled = enabled;
            foreach (HorizontalLayoutGroup hlg in treeRoot.GetComponentsInChildren<HorizontalLayoutGroup>())
            {
                hlg.enabled = enabled;
            }
        }


        void OnDrawGizmos()
        {
            foreach (UILink l in skillLinks)
            {
                if (l.object1 != null && l.object2 != null)
                {
                    Gizmos.DrawLine(l.object1.transform.position, l.object2.transform.position);
                }
            }
        }

    }
}