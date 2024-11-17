using UnityEngine;

public class RocketControl : MonoBehaviour
{
    public PhysicEngine physicEngine; // Reference to the PhysicEngine script
    public Fuel fuelSystem; // Reference to the Fuel script
    public float thrust = 10f; // Thrust power

    private bool isDocked = true; // Rocket starts docked at StartPlanet
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Start the rocket docked at the StartPlanet
        GameObject startPlanet = GameObject.FindGameObjectWithTag("StartPlanet");
        if (startPlanet != null)
        {
            transform.position = startPlanet.transform.position;
            rb.isKinematic = true; // Prevent movement while docked
        }
    }

    void Update()
    {
        if (physicEngine == null || fuelSystem == null)
        {
            Debug.LogError("PhysicEngine or FuelSystem is not assigned!");
            return;
        }

        // Undock the rocket when the player presses Space
        if (isDocked && Input.GetKey(KeyCode.Space))
        {
            Undock();
        }

        // Handle movement only if undocked
        if (!isDocked)
        {
            RotateTowardsCursor();

            if (Input.GetKey(KeyCode.Space))
            {
                if (fuelSystem.HasFuel())
                {
                    ApplyThrust();
                    fuelSystem.ConsumeFuel(fuelSystem.fuelConsumptionRate);
                }
                else
                {
                    Debug.Log("Out of fuel!");
                }
            }
            else
            {
                physicEngine.rocketInputAcceleration = Vector2.zero; // No thrust when not pressing Space
            }
        }
    }

    void RotateTowardsCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle - 90, Time.deltaTime * 5f);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, smoothAngle));
    }

    void ApplyThrust()
    {
        Vector2 thrustDirection = transform.up;
        physicEngine.rocketInputAcceleration = thrustDirection * thrust;
    }

    void Undock()
    {
        isDocked = false;
        rb.isKinematic = false; // Enable physics for the rocket
        Debug.Log("Rocket undocked from StartPlanet!");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StartPlanet"))
        {
            Debug.Log("Rocket landed on StartPlanet!");
            rb.velocity = Vector2.zero; // Stop movement
            rb.angularVelocity = 0; // Stop rotation
        }
        else if (collision.gameObject.CompareTag("EndPlanet"))
        {
            Debug.Log("Rocket reached EndPlanet! You win!");

            // Stop the rocket completely
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;

            // Optionally freeze all movement and logic
            Time.timeScale = 0; // Stop the game
        }
    }
}