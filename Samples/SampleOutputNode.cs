/*
 * Graph editor for Unity3D.
 * 
 * Copyright (c) Marshalkin Aleksey <megalanthus@gmail.com>
 * Asset store: https://assetstore.unity.com/publishers/50181
 * Github: https://github.com/megalanthus
*/

using System;
using GraphEditor.Core;
using GraphEditor.Core.Attributes;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GraphEditor.Samples
{
    [Serializable]
    [Node("Sample output node", typeof(SampleGraph))]
    [Design("Samples/SampleOutputNode.uxml", "Samples/SampleNodeStyle.uss")]
    [Port(typeof(SampleFloatPort), InputPort, PortDirection.Input, PortTypeConnection.Single)]
    [NodeTooltip("Select the asset where the graph data will be saved")]
    public class SampleOutputNode : Node
    {
        private const string InputPort = "Input";

        public override Type TypeNodeData => typeof(SampleOutputNodeData);
        public new SampleOutputNodeData Data => (SampleOutputNodeData) base.Data;

        public override void OnCreateContent(VisualElement nodeVisual)
        {
            ObjectField objectField = nodeVisual.Q<ObjectField>("asset");
            objectField.objectType = typeof(SampleAsset);
        }

        public override void OnChangeConnections()
        {
            UpdateAsset();
        }

        public override void OnCalculate()
        {
            UpdateAsset();
        }

        private void UpdateAsset()
        {
            SampleFloatPort inputPort = FindPortByName<SampleFloatPort>(InputPort);
            if (Data.Asset != null) Data.Asset.SampleData = inputPort.Value;
        }
    }
}