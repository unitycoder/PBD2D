using andywiecko.BurstCollections;
using andywiecko.BurstMathUtils;
using andywiecko.ECS;
using Unity.Collections;
using Unity.Mathematics;

namespace andywiecko.PBD2D.Core
{
    public interface IEdgeLengthConstraints : IComponent
    {
        float Stiffness { get; }
        float Compliance { get; }
        Ref<NativeIndexedArray<Id<Point>, float2>> PredictedPositions { get; }
        Ref<NativeIndexedArray<Id<Point>, float>> MassesInv { get; }
        Ref<NativeList<EdgeLengthConstraint>> Constraints { get; }
    }

    public interface ITriangleAreaConstraints : IComponent
    {
        float Stiffness { get; }
        float Compliance { get; }
        Ref<NativeIndexedArray<Id<Point>, float2>> PredictedPositions { get; }
        Ref<NativeIndexedArray<Id<Point>, float>> MassesInv { get; }
        Ref<NativeList<TriangleAreaConstraint>> Constraints { get; }
    }

    public interface IShapeMatchingConstraint : IComponent
    {
        float Stiffness { get; }
        float Beta { get; }
        float TotalMass { get; }
        Ref<NativeIndexedArray<Id<Point>, float>> MassesInv { get; }
        Ref<NativeIndexedArray<Id<Point>, float2>> InitialRelativePositions { get; }
        Ref<NativeIndexedArray<Id<Point>, float2>> PredictedPositions { get; }
        Ref<NativeIndexedArray<Id<Point>, float2>> RelativePositions { get; }
        Ref<NativeReference<float2>> CenterOfMass { get; }
        Ref<NativeReference<float2x2>> ApqMatrix { get; }
        float2x2 AqqMatrix { get; }
        Ref<NativeReference<float2x2>> AMatrix { get; }
        Ref<NativeReference<Complex>> Rotation { get; }
    }
}
