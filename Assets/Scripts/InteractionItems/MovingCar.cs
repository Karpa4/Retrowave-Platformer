using UnityEngine;

public class MovingCar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _speed;
    private bool _moovingRight = true;

    private void Update()
    {
        if (transform.position.x > _maxX)
        {
            if (_moovingRight)
            {
                _moovingRight = false;
                _speed = -_speed;
                _sprite.flipX = !_sprite.flipX;
            }
        }
        else if (transform.position.x < _minX)
        {
            if (!_moovingRight)
            {
                _moovingRight = true;
                _sprite.flipX = !_sprite.flipX;
                _speed = -_speed;
            }
        }

        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }
}
