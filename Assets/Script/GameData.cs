using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static Dictionary<IconsType, Sprite> SpriteLibrary;

    public static IconsType RandomArt()
    {
        var values = Enum.GetValues(typeof(IconsType));
        foreach (var variable in values)
        {
            var iconsType = variable is IconsType ? (IconsType) variable : IconsType.Art1;
            if (iconsType.ToString().Co == sprite.name)
            {
                spriteLibrary.Add(iconsType, sprite);
            }
        }

        return IconsType
    }
}