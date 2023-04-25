using UnityEngine;

public class MaterialType : MonoBehaviour
{
    // Base class for materials
}

public class Wood : MaterialType
{
    public void Ignite()
    {
        // Ignites the wood
    }
}

public class Metal : MaterialType
{
    public void Melt()
    {
        // Melts the metal
    }
}

public class Rock : MaterialType
{
    public void Shatter()
    {
        // Shatters the rock
    }
}
