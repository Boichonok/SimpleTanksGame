using System;
using UnityEngine;
namespace Tank
{

    public interface IShell
    {
        float AttackValue { get; set; }
        float LifeTime { get; set; }
        void ShellController();
    }
}
