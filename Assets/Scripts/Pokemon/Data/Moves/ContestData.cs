using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data.Moves
{
    public class ContestData
    {
        public string Condition;
        public int Appeal;
        public int Jam;

        public ContestData(string condition, int appeal, int jam)
        {
            Condition = condition;
            Appeal = appeal;
            Jam = jam;
        }
    }
}
