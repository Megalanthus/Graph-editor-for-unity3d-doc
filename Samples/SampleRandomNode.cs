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
using UnityEditor;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace GraphEditor.Samples
{
    [Serializable]
    [Node("Sample random node", typeof(SampleGraph))]
    [Design("Samples/SampleRandomNode.uxml", "Samples/SampleNodeStyle.uss")]
    [Port(typeof(SampleFloatPort), RandomOutputPort, PortDirection.Output)]
    [NodeTooltip("Random number generator")]
    public class SampleRandomNode : Node
    {
        private const string RandomOutputPort = "Random output";

        public override Type TypeNodeData => typeof(SampleRandomNodeData);
        public new SampleRandomNodeData Data => (SampleRandomNodeData) base.Data;

        public override void OnCreateNode()
        {
            GenerateRandom();
        }

        public override void OnCreateContent(VisualElement nodeVisual)
        {
            Button generateButton = nodeVisual.contentContainer.Q<Button>("generate");
            generateButton.clicked += GenerateRandom;
        }

        public override void OnCalculate()
        {
            FindPortByName(RandomOutputPort).Value = Data.RandomValue;
        }

        private void GenerateRandom()
        {
            Undo.RecordObject(Data, "Generate random");
            Data.RandomValue = Random.Range(0f, 100f);
            Calculate();
        }
    }
}