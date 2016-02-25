﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoStore_FrontEnd
{
    /// <summary>
    /// CommitsMap.xaml 的交互逻辑
    /// </summary>
    public partial class CommitsMap : UserControl
    {
        // Structures:

        public class TimeNode
        {
            public TimeNode()
            {
                parentNodes = new List<TimeNode>();
            }
            
            public string           name;
            public string           text;
            public DateTime         time;
            public List<TimeNode>   parentNodes;

            public bool             _drawn;
        };


        // Methods:

        public CommitsMap()
        {
            InitializeComponent();

            canvas.Width = 0;
            canvas.Height = 0;


        }
        
        public void AddNode(TimeNode timeNode)
        {
        }

        public bool RemoveNode(uint index)
        {
            return true;
        }

        public void AddStartIndex(uint index)
        {
            _startIndices.Add(index);
        }

        public void ClearNodes()
        {
        }

        public void Draw(uint maxDepth)
        {
            // Clean up the canvas
            canvas.Children.Clear();
            canvas.Width = 0;
            canvas.Height = 0;

            // Clean up nodes' flags.
            foreach (TimeNode eachnode in _nodes)
                eachnode._drawn = false;

            // Define vars
            uint depth = 0;
            TimeNode currNode;

            // Start from each start index
            foreach (int startNodeIdx in _startIndices)
            {
                // Point to the start node, and get ready to start drawing loop
                currNode = _nodes[startNodeIdx];
            }


        }
        
        // Properties:
        List<TimeNode>      _nodes;
        List<uint>          _startIndices;
    }
}
