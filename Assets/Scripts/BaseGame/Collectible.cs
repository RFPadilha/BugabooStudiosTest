using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class Collectible : MonoBehaviour
{
    public float pickUpRadius = 1.2f;
    public float rotationSpeed = 10f;

    SphereCollider _collider;
    private CollectibleInternal _coin;
    void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = pickUpRadius;
        _coin = new CollectibleInternal();
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
    private void OnTriggerEnter(Collider other)
    {
        PickUp(other);
    }
    public void PickUp(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            //other.gameObject.GetComponent<PlayerController>().IncreaseScore(value);
            _coin.HandleCollect(other.gameObject.GetComponent<IPlayerController>());
            Destroy(this.gameObject); 
        }
    }
}
public class CollectibleInternal
{
    int value = 1;
    public void HandleCollect(IPlayerController playerController)
    {
        playerController.IncreaseScore(value);
    }
}
