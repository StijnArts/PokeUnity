using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data.Moves
{
    public static class MoveFlags
    {
        public static string AllyAnimation = "AllyAnimation"; // The move plays its animation when used on an ally.
        public static string BypassSub = "BypassSub"; // Ignores a target's substitute.
        public static string bite = "Bite"; // Power is multiplied by 1.5 when used by a Pokemon with the Ability Strong Jaw.
        public static string Bullet = "Bullet"; // Has no effect on Pokemon with the Ability Bulletproof.
        public static string CantUseTwice = "CantUseTwice"; // The user cannot select this move after a previous successful use.
        public static string Charge = "Charge"; // The user is unable to make a move between turns.
        public static string Contact = "Contact"; // Makes contact.
        public static string Dance = "Dance"; // When used by a Pokemon, other Pokemon with the Ability Dancer can attempt to execute the same move.
        public static string Defrost = "Defrost"; // Thaws the user if executed successfully while the user is frozen.
        public static string Distance = "Distance"; // Can target a Pokemon positioned anywhere in a Triple Battle.
        public static string FailCopycat = "FailCopycat"; // Cannot be selected by Copycat.
        public static string FailEncore = "FailEncore"; // Encore fails if target used this move.
        public static string FailInstruct = "FailInstruct"; // Cannot be repeated by Instruct.
        public static string FailMeFirst = "FailMeFirst"; // Cannot be selected by Me First.
        public static string FailMimic = "FailMimic"; // Cannot be copied by Mimic.
        public static string FutureMove = "FutureMove"; // Targets a slot, and in 2 turns damages that slot.
        public static string Gravity = "Gravity"; // Prevented from being executed or selected during Gravity's effect.
        public static string Heal = "Heal"; // Prevented from being executed or selected during Heal Block's effect.
        public static string Mirror = "Mirror"; // Can be copied by Mirror Move.
        public static string MustPressure = "MustPressure"; // Additional PP is deducted due to Pressure when it ordinarily would not.
        public static string NoAssist = "NoAssist"; // Cannot be selected by Assist.
        public static string NonSky = "NonSky"; // Prevented from being executed or selected in a Sky Battle.
        public static string NoParentalBond = "NoParentalBond"; // Cannot be made to hit twice via Parental Bond.
        public static string NoSleepTalk = "NoSleepTalk"; // Cannot be selected by Sleep Talk.
        public static string PledgeCombo = "PledgeCombo"; // Gems will not activate. Cannot be redirected by Storm Drain / Lightning Rod.
        public static string Powder = "Powder"; // Has no effect on Pokemon which are Grass-type, have the Ability Overcoat, or hold Safety Goggles.
        public static string Protect = "Protect"; // Blocked by Detect, Protect, Spiky Shield, and if not a Status move, King's Shield.
        public static string Pulse = "Pulse"; // Power is multiplied by 1.5 when used by a Pokemon with the Ability Mega Launcher.
        public static string Punch = "Punch"; // Power is multiplied by 1.2 when used by a Pokemon with the Ability Iron Fist.
        public static string Recharge = "Recharge"; // If this move is successful, the user must recharge on the following turn and cannot make a move.
        public static string Reflectable = "Reflectable"; // Bounced back to the original user by Magic Coat or the Ability Magic Bounce.
        public static string Slicing = "Slicing"; // Power is multiplied by 1.5 when used by a Pokemon with the Ability Sharpness.
        public static string Snatch = "Snatch"; // Can be stolen from the original user and instead used by another Pokemon using Snatch.
        public static string Sound = "Sound"; // Has no effect on Pokemon with the Ability Soundproof.
        public static string Wind = "Wind"; // Activates the Wind Power and Wind Rider Abilities.
    }
}
