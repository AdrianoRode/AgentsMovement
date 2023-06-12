using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Transform enemy;
    public bool evading;
    public float speed = 7;
    public float health = 100;
    private int randomX;
    private int randomZ;

    void Start()
    {
        RandomPos();
    }

    void Update()
    {
        MoveToDestination();
        Damage();
    }

    void Damage()
    {
        if (Vector3.Distance(transform.position, enemy.position) < 5f)
        {
            health -= 10f * Time.deltaTime;

            if(health <= 0)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

    }

    void MoveToDestination()
    {
        var step = speed * Time.deltaTime;
        var playerPosX = transform.position.x;
        var playerPosZ = transform.position.z;

        Vector3 destination = new Vector3(randomX, transform.position.y, randomZ);

        if (Vector3.Distance(transform.position, enemy.position) < 29f)
        {
            speed = 10;
            evading = true;

            Vector3 direction = transform.position - enemy.position;
            direction.Normalize();

            transform.position += direction * step;

        }
        else
        {
            evading = false;
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
            transform.LookAt(new Vector3(randomX, transform.position.y, randomZ));

            if (playerPosX == randomX && playerPosZ == randomZ)
            {
                RandomPos();
            }
        }
        
    }
    void RandomPos()
    {
        randomX = Random.Range(-50, 50);
        randomZ = Random.Range(-58, 58);
    }
}
