using UnityEngine;

internal class vector3 : Transform
{
    private int v1;
    private int v2;
    private int v3;

    public vector3(int v1, int v2, int v3)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
    }
}