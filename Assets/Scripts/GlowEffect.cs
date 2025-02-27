using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    public Material glowMaterial; // Assign your glowing material
    public Color targetColor = Color.yellow; // The target glow color
    public float glowSpeed = 1f; // Speed of the glow effect

    private void Update()
    {
        // Pulsing effect by changing the emission intensity over time
        float emission = Mathf.PingPong(Time.time * glowSpeed, 1f);
        glowMaterial.SetColor("_EmissionColor", targetColor * emission);
    }
}
