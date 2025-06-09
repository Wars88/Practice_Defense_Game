using UnityEngine;

public class TowerRing : MonoBehaviour
{
    [SerializeField] float _rotateSpeed;

    private void Update()
    {
        transform.Rotate(0f, _rotateSpeed * Time.deltaTime, 0f);
    }
}