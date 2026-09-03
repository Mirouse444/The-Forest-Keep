using System;

public interface IHPLineSpawner
{
    HPLine GetHPLine(out Action<HPLine> releaseAction);
}