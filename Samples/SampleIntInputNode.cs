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
    [Node("Sample integer input node", typeof(SampleGraph))]
    [Design("Samples/SampleIntInputNode.uxml", "Samples/SampleNodeStyle.uss")]
    [Port(typeof(SampleIntPort), IntOutputPort, PortDirection.Output)]
    [NodeTooltip("Enter an integer value")]
    public class SampleIntInputNode : Node
    {
        private const string IntOutputPort = "Integer output";

        public override Type TypeNodeData => typeof(SampleIntInputNodeData);
        public new SampleIntInputNodeData Data => (SampleIntInputNodeData) base.Data;

        public override void OnChangeData()
        {
            Calculate();
        }

        public override void OnCalculate()
        {
            FindPortByName(IntOutputPort).Value = Data.IntValue;
        }
    }
}