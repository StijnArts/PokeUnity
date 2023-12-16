using Assets.Scripts.Battle.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data.Moves
{
    public class SecondaryEffect : HitEffect
    {
        public int? Chance;
        public Ability Ability;
        public bool? DustProof;
        public HitEffect Self;
    }
}
