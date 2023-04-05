using Godot;
using System;

public partial class ProgressBarUpdate : ProgressBar
{
    private void _OnPlayerTookDamage(float amount)
    {
        Value -= amount;
    }
}
