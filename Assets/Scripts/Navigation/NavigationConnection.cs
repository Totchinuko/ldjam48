using UnityEngine;
using System;
using System.Collections.Generic;

namespace Constantine
{
    [Serializable]
    public struct NavigationConnection
    {
        public NavigationPoint destination;
        public bool jump;
    }
}