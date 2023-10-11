using System.Collections.Generic;
using Core;

public static class ResourceBank
{
    private static readonly Dictionary<GameResource, ObservableInt> Resources = new();

    public static void ChangeResource(GameResource r, int v)
    {
        if (!Resources.ContainsKey(r))
        {
            Resources.Add(r, new ObservableInt(v));
        }
        else
        {
            Resources[r].Value += v;
        }
    }

    public static ObservableInt GetResource(GameResource r)
    {
        return Resources[r];
    }
}