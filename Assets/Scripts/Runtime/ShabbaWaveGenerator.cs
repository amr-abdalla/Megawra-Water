public class ShabbaWaveGenerator : MonoBehaviourBase
{
    // Properties
    // reference to the mesh / mesh generator
    // waveImpactExtent : extent of the mesh distortion from the surface (normalized)
    // the water material

    // Unity messages / callbacks
    // Trigger (use OnCollisionEnter) (on the surface of the water) that will have a callback when the shabba enters the water 


    // internals
    // getClosestVertexToCollision : look into the vertices in the mesh, selecting only those that are interesting (aka the surface line) and get the closest vertex to the collision point. What we need most is the UV of that point
    // setMaterialData : send the relevant data to the shader so it can react 
    // the collision point (as a uv)
    // float surfaceWaveStrength : 1 - polish idea : start a couroutine that is going to interpolate from current surfaceWaveStrength in the shader to 1 in a very short time
        // start a coroutine that diminishes surfaceWaveStrength over time

}

