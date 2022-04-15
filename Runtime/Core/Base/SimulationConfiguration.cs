using System;
using Unity.Mathematics;
using UnityEngine;

namespace andywiecko.PBD2D.Core
{
    public interface ISimulationConfiguration
    {
        int StepsCount { get; }
        int SubstepsCount { get; }
        float DeltaTime { get; }
        float2 GlobalExternalForce { get; }
        float GlobalDamping { get; }
    }

    [Serializable]
    public class SimulationConfiguration : ISimulationConfiguration
    {
        [field: SerializeField, Tooltip("Number of steps in PBD simulation.")]
        public int StepsCount { get; set; } = 2;

        [field: SerializeField, Tooltip("Number of substeps in PBD simulation.")]
        public int SubstepsCount { get; set; } = 8;

        [field: SerializeField, Min(1e-15f), Tooltip("The smalest time step considered in the PBD simulation step.")]
        public float DeltaTime { get; set; } = 0.001f;

        [field: SerializeField, Tooltip("External force applied to all PBD simulated bodies.")]
        public float2 GlobalExternalForce { get; set; } = new(0, -10);

        [field: SerializeField, Min(0), Tooltip("Energy dissipation factor.")]
        public float GlobalDamping { get; set; } = 0;
    }
}
