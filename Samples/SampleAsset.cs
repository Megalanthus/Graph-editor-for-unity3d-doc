/*
 * Graph editor for Unity3D.
 * 
 * Copyright (c) Marshalkin Aleksey <megalanthus@gmail.com>
 * Asset store: https://assetstore.unity.com/publishers/50181
 * Github: https://github.com/megalanthus
*/

using System;
using UnityEngine;

namespace GraphEditor.Samples
{
    [Serializable]
    [CreateAssetMenu(fileName = "SampleAsset", menuName = "Graph/Sample asset", order = 5000)]
    public class SampleAsset : ScriptableObject
    {
        public float SampleData;
    }
}