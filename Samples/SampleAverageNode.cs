/*
 * Graph editor for Unity3D.
 * 
 * Copyright (c) Marshalkin Aleksey <megalanthus@gmail.com>
 * Asset store: https://assetstore.unity.com/publishers/50181
 * Github: https://github.com/megalanthus
*/

using System;
using System.Linq;
using GraphEditor.Core;
using GraphEditor.Core.Attributes;
using UnityEngine.UIElements;

namespace GraphEditor.Samples
{
    [Serializable]
    [Node("Sample average node", typeof(SampleGraph))]
    [Design(BasePath + "SampleAverageNode.uxml", BasePath + "SampleNodeStyle.uss")]
    [MinimizedDesign(BasePath + "SampleAverageNodeMinimized.uxml", BasePath + "SampleNodeStyle.uss")]
    [NodeTooltip("Calculate average value")]
    [NodeHelp(BasePath + "SampleAverageNodeHelp.uxml", BasePath + "SampleAverageNodeHelpStyles.uss")]
    [Port(typeof(SampleFloatPort), OutputPort, PortDirection.Output)]
    [Port(typeof(SampleFloatPort), "Input 1", PortDirection.Input, PortTypeConnection.Single)]
    [Port(typeof(SampleFloatPort), "Input 2", PortDirection.Input, PortTypeConnection.Single)]
    public class SampleAverageNode : Node
    {
        private const string BasePath = "Samples/";
        private const string OutputPort = "Output";
        private const string NodeItemTemplateUxml = BasePath + "SampleAverageNodeItem.uxml";
        private const string TemplatePortClassUss = "template-port";

        public override void OnCreateContent(VisualElement nodeVisual)
        {
            Button addInputButton = nodeVisual.contentContainer.Q<Button>("add-input");
            if (addInputButton != null) addInputButton.clicked += AddInput;
            Button removeInputButton = nodeVisual.contentContainer.Q<Button>("remove-input");
            if (removeInputButton != null) removeInputButton.clicked += RemoveInput;
        }

        public override void OnSynchronize(VisualElement nodeVisual)
        {
            VisualElement portContainer = nodeVisual.contentContainer.Q("port-container");
            portContainer.Query(classes: TemplatePortClassUss).ForEach(portVisual =>
            {
                if (FindPortByName(portVisual.name) == null) portVisual.parent.RemoveFromHierarchy();
            });
            foreach (Port port in InputPorts)
            {
                if (portContainer.Q(port.name) != null) continue;
                VisualElement template = Utils.LoadTemplate(NodeItemTemplateUxml);
                portContainer.Add(template);
                template.AddToClassList("node-row");
                template.Q(classes: TemplatePortClassUss).name = port.name;
                template.Q<Label>().text = port.name;
            }
        }

        public override void OnCalculate()
        {
            FindPortByName<SampleFloatPort>(OutputPort).Value = GetAverage();
        }

        public override void OnCreateContextMenu(DropdownMenu dropdownMenu)
        {
            dropdownMenu.AppendSeparator();
            dropdownMenu.AppendAction("Add input", action => AddInput());
            dropdownMenu.AppendAction("Remove input", action => RemoveInput());
        }

        private void AddInput()
        {
            string portName = $"Input {CountInputPorts + 1}";
            CreatePort(typeof(SampleFloatPort), portName, PortDirection.Input, PortTypeConnection.Single);
            Synchronize();
            Calculate();
        }

        private void RemoveInput()
        {
            if (CountInputPorts <= 2) return;
            RemovePort(InputPortsByType<SampleFloatPort>().Last());
            Synchronize();
            Calculate();
        }

        private float GetAverage()
        {
            double average = 0;
            foreach (SampleFloatPort port in InputPortsByType<SampleFloatPort>()) average += port.Value;
            return (float) (average / CountInputPorts);
        }
    }
}