using andywiecko.BurstCollections;
using andywiecko.ECS;
using andywiecko.PBD2D.Core;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace andywiecko.PBD2D.Components
{
    [RequireComponent(typeof(TriMesh))]
    [Category(PBDCategory.Constraints)]
    public class TriMeshTriangleAreaConstraints : BaseComponent, ITriangleAreaConstraints
    {
        public Ref<NativeIndexedArray<Id<Point>, float2>> Positions => TriMesh.Positions;
        public Ref<NativeIndexedArray<Id<Point>, float>> MassesInv => TriMesh.MassesInv;
        public Ref<NativeList<TriangleAreaConstraint>> Constraints { get; private set; }

        [field: SerializeField, Min(0)]
        public float Compliance { get; private set; } = 0;

        [field: SerializeField, Range(0, 1)]
        public float Stiffness { get; private set; } = 1f;

        private TriMesh TriMesh { get; set; }

        private void Start()
        {
            TriMesh = GetComponent<TriMesh>();

            DisposeOnDestroy(
                Constraints = new NativeList<TriangleAreaConstraint>(TriMesh.Triangles.Value.Length, Allocator.Persistent)
            );

            var positions = TriMesh.Positions.Value.AsReadOnly();
            foreach (var t in TriMesh.Triangles.Value.AsReadOnly())
            {
                var a2 = t.GetSignedArea2(positions);
                Constraints.Value.Add(new(t, a2));
            }
        }
    }
}