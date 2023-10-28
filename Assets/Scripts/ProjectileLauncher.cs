using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;
    public Transform lauchPoint;


    public void Fire()
    {
        GameObject projectileCreated = Instantiate(projectile, lauchPoint.position, projectile.transform.rotation);

        Vector3 playerScale = projectile.transform.localScale;
        projectileCreated.transform.localScale = new Vector3(playerScale.x * transform.localScale.x > 0 ? 1 : -1,
            playerScale.y, 
            playerScale.z);
    }
}
