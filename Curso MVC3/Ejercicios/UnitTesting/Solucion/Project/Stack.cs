﻿namespace Project
{
    using System.Collections.Generic;

    public class Stack
    {
        readonly IList<int> elements=new List<int>();

        public bool IsEmpty
        {
            get { return elements.Count == 0; }
        }

        public void Push(int element)
        {
            elements.Insert(0,element);
        }

        public int Pop()
        {
            int element= elements[0];
            elements.RemoveAt(0);
            return element;
        }
    }
}
