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
    [Design("Samples/SamplePort.uxml", "Samples/SampleFloatPortStyle.uss")]
    public sealed class SampleFloatPort : Port
    {
        public new float Value
        {
            get => base.Value is float ? (float) base.Value :
                base.Value is int ? (float) (int) base.Value : 0;
            set => base.Value = value;
        }
    }
}