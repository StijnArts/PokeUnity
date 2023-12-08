using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGroup
{
    private Func<int, int> formula;
    public string experienceGroupName;
    public ExperienceGroup(string experienceGroupName, Func<int, int> formula)
    {
        this.experienceGroupName = experienceGroupName;
        this.formula = formula;
    }

    public int getExpToNextLevel(int level)
    {
        if(level+1 == 1) return 0;
        return formula(level+1);
    }

    public int getExpForCurrentLevel(int level)
    {
        if (level == 0) return 0;
        if (level == 1) return 0;
        return formula(level);
    }
}
