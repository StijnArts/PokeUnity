﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public interface Target
    {
        public enum TargettingType { Single, All, Adjacent, Ally, Field, Battle }
    }
}
