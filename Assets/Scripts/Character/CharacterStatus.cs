using UnityEngine;
using Mirror;

public class CharacterStatus : NetworkBehaviour {
    public float Speed = 6;
    public float DizzyTime = 3;
    public float DizzyTimeReduceAmount = 0.2f;
    public int InitWater;
    public string Name;
    public AudioClip OnHitSound;
    public AudioClip FireSound;

    [HideInInspector]
    public int RemainedWater = 0;

    [HideInInspector]
    public Vector2 Direction;

    [HideInInspector]
    public Collectable ItemCanCollect = null;
    private bool _isCurrentShooting = false;

    private void Start() {
        RemainedWater = InitWater;
        if(isLocalPlayer) {
            foreach (var camera in Camera.allCameras)
                camera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = this.transform;
        }
    }

    [Command]
    private void CmdMove(float x, float y) {
        if (Mathf.Approximately(x, 0) && Mathf.Approximately(y, 0))
            return;
        Direction.x = x * Mathf.Sqrt(1.0f - (y * y) * 0.5f);
        Direction.y = y * Mathf.Sqrt(1.0f - (x * x) * 0.5f);
        
        var shooter = this.GetComponent<Shooter>();
        if(!shooter.shooting)
        {
            Animator animator = this.GetComponent<Animator>();
            animator.SetFloat("DirectionX", Direction.x);
            animator.SetFloat("DirectionY", Direction.y);
        }
    }

    [Command]
    public void CmdSetIsShooting(bool isShooting)
    {
        if (isShooting && !_isCurrentShooting) {
            this.GetComponent<AI.FSM.CharacterFSM>().SetTrigger(AI.FSM.FSMTriggerID.AttackStart);
            _isCurrentShooting = true;
        } else if (!isShooting && _isCurrentShooting) {
            this.GetComponent<AI.FSM.CharacterFSM>().SetTrigger(AI.FSM.FSMTriggerID.AttackEnd);
            _isCurrentShooting = false;
        }
        GetComponent<Shooter>().shooting = isShooting;
    }

    private void Update() {
        if(isLocalPlayer) {
            if(Input.GetButtonDown("Fire"))
                CmdSetIsShooting(true);
            if(Input.GetButtonUp("Fire"))
                CmdSetIsShooting(false);

            
            CmdMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        var shooter = this.GetComponent<Shooter>();
        if(shooter.shooting)
        {
            Animator animator = this.GetComponent<Animator>();
            animator.SetFloat("DirectionX", Mathf.Cos(shooter.GetShootAngle() * Mathf.Deg2Rad));
            animator.SetFloat("DirectionY", Mathf.Sin(shooter.GetShootAngle() * Mathf.Deg2Rad));
        }
    }

    [ClientRpc]
    public void PlayOnHitSFX() {
        GameManager.Instance.SFXSource.PlayOneShot(OnHitSound);
    }
}
