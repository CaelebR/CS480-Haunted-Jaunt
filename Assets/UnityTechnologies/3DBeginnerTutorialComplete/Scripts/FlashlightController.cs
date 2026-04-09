using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public WaypointPatrol[] ghosts;

    public float range = 8f;
    public float dotThreshold = 0.95f;

    public Light flashlightLight;

    public float maxIntensity = 6f;
    public float fadeSpeed = 5f;

    private bool flashlightOn = true;
    private float targetIntensity;

    void Start()
    {
        if (flashlightLight != null)
        {
            targetIntensity = maxIntensity;
            flashlightLight.intensity = maxIntensity;
        }
    }

    void Update()
    {
        if (flashlightLight == null)
        {
            Debug.LogError("Flashlight light not assigned!");
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightOn = !flashlightOn;

            if (flashlightOn)
                targetIntensity = maxIntensity;
            else
                targetIntensity = 0f;
        }

        flashlightLight.intensity = Mathf.Lerp(
            flashlightLight.intensity,
            targetIntensity,
            fadeSpeed * Time.deltaTime
        );

        if (ghosts == null)
            return;

        for (int i = 0; i < ghosts.Length; i++)
        {
            WaypointPatrol ghost = ghosts[i];

            if (ghost == null)
                continue;

            ghost.isStunned = false;

            Vector3 toGhost = ghost.transform.position - transform.position;
            float distance = toGhost.magnitude;

            if (distance > range)
                continue;

            Vector3 direction = toGhost.normalized;
            float dot = Vector3.Dot(transform.forward, direction);

            if (flashlightLight.intensity > 0.5f && dot > dotThreshold)
            {
                ghost.isStunned = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 origin = transform.position + Vector3.up * 0.5f;

        Gizmos.DrawRay(origin, transform.forward * range);

        float angle = Mathf.Acos(dotThreshold) * Mathf.Rad2Deg;

        Vector3 leftBoundary = Quaternion.Euler(0f, -angle, 0f) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0f, angle, 0f) * transform.forward;

        Gizmos.DrawRay(origin, leftBoundary * range);
        Gizmos.DrawRay(origin, rightBoundary * range);
    }
}