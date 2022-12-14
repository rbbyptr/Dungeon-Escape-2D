using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnim;
    private Animator _swordAnim;
    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = GetComponentInChildren<Animator>();
        _swordAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Move(float move)
    {
        if (_playerAnim != null)
        {
            _playerAnim.SetFloat("Move", Mathf.Abs(move));        
        }
    }

    public void Jump(bool state)
    {
        if (_playerAnim != null)
        {
            _playerAnim.SetBool("Jumping", state);
        }
    }

    public void Attack()
    {
        if (_playerAnim != null)
        {
            _playerAnim.SetTrigger("Attack");
            _swordAnim.SetTrigger("SwordAnimation");
        }
    }
}
