using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExperienceGroupRegistry
{
    public static Dictionary<string, ExperienceGroup> ExperienceGroups = new Dictionary<string, ExperienceGroup>();

    public static ExperienceGroup GetExperienceGroup(string id)
    {
        ExperienceGroup value;
        ExperienceGroups.TryGetValue(id, out value);
        return value;
    }

    public static void registerExperienceGroups()
    {
        ExperienceGroups.Add("erratic", new ExperienceGroup("erratic", (level =>
        {
            if(level < 50)
            {
                return (int)(Math.Pow(level, 3) * (100 - level)) / 50;
            } 
            else if(50 <= level && level < 68)
            {
                return (int)(Math.Pow(level, 3) * (150 - level)) / 100;
            }
            else if (68 <= level && level < 98)
            {
                return (int)(Math.Pow(level, 3) * Math.Floor((1911D - level*10) / 3)) / 500;
            } else
            {
                return (int)(Math.Pow(level, 3) * (160 - level)) / 100;
            }
        }
        )));

        ExperienceGroups.Add("fast", new ExperienceGroup("fast", (level => (int)(4 * Math.Pow(level, 3)) / 5)));

        ExperienceGroups.Add("medium_fast", new ExperienceGroup("medium_fast", (level => (int)Math.Pow(level, 3))));

        ExperienceGroups.Add("medium_slow", new ExperienceGroup("medium_slow", (level => {
            if(level == 0)
            {
                return 0;
            }
            else if (level == 1)
            {
                return 9;
            } 
            else
            {
                return (int)((1.2 * Math.Pow(level, 3)) - (15 * Math.Pow(level, 2)) + (level * 100) - 140);
            }
        }
        )));
        ExperienceGroups.Add("slow", new ExperienceGroup("slow", (level => (int)(5 * Math.Pow(level, 3)) / 4)));

        ExperienceGroups.Add("fluctuating", new ExperienceGroup("fluctuating", (level =>
        {
            if(level < 15)
            {
                return (int)(Math.Pow(level, 3) * ( Math.Floor((level + 1) / 3D) + 24)) / 50;
            } 
            else if(15 <= level && level < 36)
            {
                return (int)(Math.Pow(level, 3) * (level + 14)) / 50;
            }
            else
            {
                return (int)(Math.Pow(level, 3) * (Math.Floor(level / 2D) + 32)) / 50;
            }
        })));
    }

    internal static void testExperienceGroups()
    {
        string printedText = "\nExperience Groups: ";
        foreach (ExperienceGroup experienceGroup in ExperienceGroups.Values)
        {
            printedText += "\t\t" + experienceGroup.experienceGroupName;
        }
        for (int i = 1; i < 101; i++)
        {
            printedText += "\nlevel " + i + " experience: ";
            foreach (ExperienceGroup experienceGroup in ExperienceGroups.Values)
            {
                printedText += "\t\t" + experienceGroup.getExpToNextLevel(i);
            }
        }
        Debug.Log(printedText);
    }
}
