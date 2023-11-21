using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    public float power = 10f; // Siła uderzenia piłki
    public float airResistance = 0.1f; // Współczynnik oporu powietrza
    public float heightMultiplier = 2f; // Mnożnik wysokości celowania
    private bool isBallMoving = false; // Flaga określająca, czy piłka się porusza
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Kliknięcie lewym przyciskiem myszy, gdy piłka nie porusza się jeszcze
            ApplyForceToBall();
        }
    }

    void ApplyForceToBall()
    {
        if (rb != null)
        {
            // Pobierz pozycję myszy w przestrzeni świata gry
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Oblicz kierunek uderzenia na podstawie różnicy między pozycją piłki a punktem uderzenia myszy
                Vector3 direction = hit.point + new Vector3(0f, heightMultiplier, 0f) - transform.position;

                // Dodaj składową pionową do kierunku, aby piłka leciała do góry
                //direction.y *= heightMultiplier;

                // Normalizuj kierunek, aby uzyskać jednostkowy wektor kierunku
                direction.Normalize();

                // Dodaj siłę do piłki w kierunku wyliczonym przez raycast
                rb.AddForce(direction * power, ForceMode.Impulse);

                // Ustaw flagę na true, aby oznaczyć, że piłka jest w ruchu
                isBallMoving = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isBallMoving)
        {
            // Dodaj siłę oporu powietrza proporcjonalną do prędkości piłki
            Vector3 airResistanceForce = -rb.velocity * airResistance;

            // Ustaw opór w kierunku pionowym na 0, aby piłka swobodnie spadała w dół
            airResistanceForce.y = 0;

            rb.AddForce(airResistanceForce);

            // Dostosuj rotację piłki podczas lotu, aby lepiej odzwierciedlić jej kierunek lotu
            Vector3 forwardDirection = rb.velocity.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(forwardDirection, Vector3.up);
            rb.MoveRotation(targetRotation);

            // Dostosuj prędkość pionową, aby uzyskać efekt paraboli
            Vector3 newVelocity = rb.velocity;
            newVelocity.y *= heightMultiplier;
            rb.velocity = newVelocity;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Sprawdź, czy piłka koliduje z czymś, co może ją zatrzymać
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Ustaw flagę na false, gdy piłka zatrzyma się
            isBallMoving = false;
        }
    }
}