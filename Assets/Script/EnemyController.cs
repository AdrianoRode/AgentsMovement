using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public enum States{PURSUIT, WANDER}
    public States actualState;
    public Transform player;
    private Animator anim;
    public int speed = 7;
    private int randomX;
    private int randomZ;
    private int hit;
    void Start()
    {
        randomX = Random.Range(-10, 10);
        randomZ = Random.Range(-20, 20);
        anim = GetComponent<Animator>();
    }

    void Update()
    {    
        var step = speed * Time.deltaTime;
        switch(actualState)
        {
            case States.PURSUIT:
                transform.position = Vector3.MoveTowards(transform.position, player.position, step);
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                speed = 8;
                anim.SetBool("isChasing", true);
                break;

            case States.WANDER:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(randomX, 0f, randomZ), step);
                transform.LookAt(new Vector3(randomX, transform.position.y, randomZ));
                speed = 4;
                anim.SetBool("isChasing", false);
                break;
                
            default:
                break;
        }    

        ChangeState();
    }

    void ChangeState()
    {
        var enemyPosX = transform.position.x;
        var enemyPosZ = transform.position.z;
        var distance = Vector3.Distance(transform.position, player.position);

        if(distance < 30f)
        {
            actualState = States.PURSUIT;
            if(distance < 5f)
            {
                anim.SetTrigger("Attack");
            }
        }
        else
        {
            if(enemyPosX == randomX && enemyPosZ == randomZ)
            {
                RandomPos();
            }
            actualState = States.WANDER;
        }
        
    }

    void RandomPos()
    {
        randomX = Random.Range(-10, 10);
        randomZ = Random.Range(-20, 20);
    }
}
