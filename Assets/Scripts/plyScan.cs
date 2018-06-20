using UnityEngine;
using System.Collections;

public class plyScan : MonoBehaviour
{
    public GameObject prefabSelect;
    public float cooldownSeconds = 5.0F;
    public float scanLifetime = 5.0F;

    private float cooldownExpire = 0.0F;


    void Update()
    {
        float time = Time.realtimeSinceStartup;

        if (time >= cooldownExpire)
        {

            Vector3 posMouse = Input.mousePosition;
            if (Input.GetButtonDown("Fire1"))
            {
                if (Camera.main == null)
                {
                    Debug.LogError("Error, no Camera tagged as 'MainCamera");
                }
                else
                {
                    Ray rCast = Camera.main.ScreenPointToRay(posMouse);
                    RaycastHit rCastHit;

                    if (Physics.Raycast(rCast, out rCastHit))
                    {
                        if (rCastHit.collider.gameObject.CompareTag("Ground"))
                        {
                            GameObject proScan = Instantiate(prefabSelect, rCastHit.point, Quaternion.identity) as GameObject;
                            Destroy(proScan, scanLifetime);
                            cooldownExpire = time + cooldownSeconds;
                        }
                    }
                }
            }
        }
    }
}
