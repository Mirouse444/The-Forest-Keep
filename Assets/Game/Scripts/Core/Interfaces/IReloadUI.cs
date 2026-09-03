using System;

public interface IReloadUI
{ 
    int CurrentAmmo { get; }
    int MaxAmmo { get; }
    
    public event Action<int, int> OnAmmoChanged;
    event Action<float> OnReloadStarted; 
}