using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace SkillTree
{
    public class GraphSaveUtility
    {
        private SkillTreeGraphView _targetGraphView;
        private SkillTreeContainer _containerCache;

        private List<Edge> edges => _targetGraphView.edges.ToList();
        private List<SkillTreeNode> nodes => _targetGraphView.nodes.ToList().Cast<SkillTreeNode>().ToList();

        public static GraphSaveUtility GetInstance(SkillTreeGraphView targetGraphView)
        {
            return new GraphSaveUtility
            {
                _targetGraphView = targetGraphView
            };
        }

        public void SaveGraph(string filename)
        {
            if (!edges.Any()) return;
            var skillTreeContainer = ScriptableObject.CreateInstance<SkillTreeContainer>();

            var connectedPorts = edges.Where(x => x.input.node != null).ToArray();
            for (int i = 0; i < connectedPorts.Length; i++)
            {
                var outputNode = connectedPorts[i].output.node as SkillTreeNode;
                var inputNode = connectedPorts[i].input.node as SkillTreeNode;

                skillTreeContainer.nodeLinks.Add(new NodeLinkData
                {
                    baseNodeGuid = outputNode.GUID,
                    portName = connectedPorts[i].output.portName,
                    targetNodeGuid = inputNode.GUID,
                });
            }

            foreach (var skillTreeNode in nodes.Where(node => !node.entryPoint))
            {
                skillTreeContainer.skillTreeNodeData.Add(new SkillTreeNodeData
                {
                    GUID = skillTreeNode.GUID,
                    data = skillTreeNode.data,
                    position = skillTreeNode.GetPosition().position,
                });
            }

            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");

            AssetDatabase.CreateAsset(skillTreeContainer, $"Assets/Resources/{filename}.asset");
            AssetDatabase.SaveAssets();
        }

        public void LoadGraph(string filename)
        {
            _containerCache = Resources.Load<SkillTreeContainer>(filename);

            if (_containerCache == null)
            {
                EditorUtility.DisplayDialog("File Not Found", "Target skill tree graph does not exist!", "OK");
                return;
            }

            ClearGraph();
            GenerateNodes();
            ConnectNodes();
        }

        private void ClearGraph()
        {
            nodes.Find(x => x.entryPoint).GUID = _containerCache.nodeLinks[0].baseNodeGuid;
            foreach (var node in nodes)
            {
                if (node.entryPoint) continue;
                edges.Where(x => x.input.node == node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));

                _targetGraphView.RemoveElement(node);
            }
        }

        private void GenerateNodes()
        {
            foreach (var nodeData in _containerCache.skillTreeNodeData)
            {
                var tempNode = _targetGraphView.CreateSkillTreeNode("Skill Tree Node", nodeData.data);
                tempNode.GUID = nodeData.GUID;
                //tempNode.data = nodeData.data;
                _targetGraphView.AddElement(tempNode);

                var nodePorts = _containerCache.nodeLinks.Where(x => x.baseNodeGuid == nodeData.GUID).ToList();
                nodePorts.ForEach(x => _targetGraphView.AddChildPort(tempNode, x.portName));
            }
        }

        private void ConnectNodes()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                var connections = _containerCache.nodeLinks.Where(x => x.baseNodeGuid == nodes[i].GUID).ToList();
                for (int j = 0; j < connections.Count; j++)
                {
                    var targetNodeGuid = connections[j].targetNodeGuid;
                    var targetNode = nodes.First(x => x.GUID == targetNodeGuid);
                    if (nodes[i].entryPoint)
                        LinkNodes(nodes[i].outputContainer[0].Q<Port>(), (Port)targetNode.inputContainer[0]);
                    else
                        LinkNodes(nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

                    targetNode.SetPosition(new Rect(_containerCache.skillTreeNodeData.First(x => x.GUID == targetNodeGuid).position, _targetGraphView.defaultNodeSize));
                }
            }
        }

        private void LinkNodes(Port output, Port input)
        {
            var tempEdge = new Edge
            {
                output = output,
                input = input,
            };

            tempEdge.input.Connect(tempEdge);
            tempEdge.output.Connect(tempEdge);
            _targetGraphView.Add(tempEdge);
        }
    }
}