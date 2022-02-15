using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using System.Linq;
using UnityEditor.UIElements;

public class SkillTreeGraphView : GraphView
{
    public readonly Vector2 defaultNodeSize = new Vector2(150, 200);

    public SkillTreeGraphView()
    {
        styleSheets.Add(Resources.Load<StyleSheet>("SkillTreeGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        AddElement(GenerateEntryPoint());
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach((port) =>
        {
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });

        return compatiblePorts;
    }

    private Port GeneratePort(SkillTreeNode node, Direction portDirection, Port.Capacity capacity=Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float)); //Arbitrary Type
    }

    private SkillTreeNode GenerateEntryPoint()
    {
        var node = new SkillTreeNode
        {
            title = "START",
            GUID = Guid.NewGuid().ToString(),
            data = null,
            entryPoint = true
        };

        var generatePort =  GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        generatePort.portName = "Next";
        node.outputContainer.Add(generatePort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateSkillTreeNode(nodeName));
    }

    public SkillTreeNode CreateSkillTreeNode(string nodeName, SkillData skillData = null)
    {
        var skillTreeNode = new SkillTreeNode
        {
            title = nodeName,
            data = skillData,
            GUID = Guid.NewGuid().ToString(),
        };
        var inputPort = GeneratePort(skillTreeNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        skillTreeNode.inputContainer.Add(inputPort);

        var button = new Button(() =>
        {
            AddChildPort(skillTreeNode);
        });
        button.text = "Add Child";
        skillTreeNode.titleContainer.Add(button);

        //button = new Button(() =>
        //{
        //    RemoveChildPort(skillTreeNode);
        //});
        //button.text = "Remove Child";
        //skillTreeNode.titleContainer.Add(button);

        var objectField = new ObjectField(String.Empty)
        {
            allowSceneObjects = false,
            objectType = typeof(SkillData),
        };
        objectField.RegisterValueChangedCallback(evt =>
        {
            skillTreeNode.data = (SkillData)evt.newValue;
        });
        objectField.SetValueWithoutNotify(skillTreeNode.data);
        skillTreeNode.mainContainer.Add(objectField);

        skillTreeNode.RefreshExpandedState();
        skillTreeNode.RefreshPorts();
        skillTreeNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));

        return skillTreeNode;
    }

    public void AddChildPort(SkillTreeNode skillTreeNode, string overridenPortName = "")
    {
        var generatedPort = GeneratePort(skillTreeNode, Direction.Output);
        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(oldLabel);

        var outputPortCount = skillTreeNode.outputContainer.Query("connector").ToList().Count;

        var outputPortName = string.IsNullOrEmpty(overridenPortName) ? $"Child {outputPortCount}" : overridenPortName;

        var textField = new TextField
        {
            name = string.Empty,
            value = outputPortName,
        };

        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        generatedPort.contentContainer.Add(new Label("  "));
        generatedPort.contentContainer.Add(textField);
        var deleteButton = new Button(() => RemovePort(skillTreeNode, generatedPort))
        {
            text = "X",
        };
        generatedPort.contentContainer.Add(deleteButton);

        generatedPort.portName = outputPortName;
        skillTreeNode.outputContainer.Add(generatedPort);
        skillTreeNode.RefreshExpandedState();
        skillTreeNode.RefreshPorts();
    }

    private void RemovePort(Node node, Port socket)
    {
        var targetEdge = edges.ToList()
            .Where(x => x.output.portName == socket.portName && x.output.node == socket.node);
        if (targetEdge.Any())
        {
            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        }

        node.outputContainer.Remove(socket);
        node.RefreshPorts();
        node.RefreshExpandedState();
    }

    private void RemoveChildPort(SkillTreeNode skillTreeNode)
    {
        skillTreeNode.outputContainer.RemoveAt(skillTreeNode.outputContainer.Query("connector").ToList().Count-1);
        skillTreeNode.RefreshExpandedState();
        skillTreeNode.RefreshPorts();
    }
}
