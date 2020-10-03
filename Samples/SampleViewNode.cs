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

namespace GraphEditor.Samples
{
    [Serializable]
    [Node("Sample view node", typeof(SampleGraph))]
    [Design("Samples/SampleViewNode.uxml", "Samples/SampleNodeStyle.uss")]
    [Port(typeof(SampleFloatPort), InputPort, PortDirection.Input, PortTypeConnection.Single)]
    [NodeTooltip("Viewing the received value")]
    public class SampleViewNode : Node
    {
        private const string InputPort = "Input";

        public override Type TypeNodeData => typeof(SampleViewNodeData);
        public new SampleViewNodeData Data => (SampleViewNodeData) base.Data;

        public override void OnChangeConnections()
        {
            UpdateValue();
        }

        public override void OnCalculate()
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            SampleFloatPort inputPort = FindPortByName<SampleFloatPort>(InputPort);
            Data.Value = inputPort.Value;
        }
    }
}