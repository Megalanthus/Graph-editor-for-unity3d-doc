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
using UnityEngine;

namespace GraphEditor.Samples
{
    [Serializable]
    [CreateAssetMenu(fileName = "SampleGraph", menuName = "Graph/Sample graph", order = 5000)]
    [GraphDesign("Samples/SampleGraphStyle.uss")]
    public sealed class SampleGraph : Graph
    {
        public override bool CanConnectPorts(Port port1, Port port2)
        {
            return port1 is SampleIntPort && port2 is SampleFloatPort ||
                   port2 is SampleIntPort && port1 is SampleFloatPort;
        }
    }
}