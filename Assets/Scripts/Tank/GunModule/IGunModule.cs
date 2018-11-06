using System;
using UnityEngine;
namespace Tank
{
    public interface IGunModule
    {
        float AttackValue { get; set; }
        GameObject Shell { get; set; }
        GameObject Tower { get; set; }
        GameObject Gun { get; set; }
        Transform SpawnShell { get; set; }
        Transform SpawnModulePos { get; set; }
        float ShootPower { get; set; }
        void InitModule();
    }
}
