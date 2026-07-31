using System;

internal interface IMeleeLauncher
{
    event Action<MeleeStrikeData> OnFire;
}