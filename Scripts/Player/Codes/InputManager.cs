public class InputManager
{
    private static InputManager instance;
    private PlayerControls playerControls;

    private InputManager()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    public static InputManager Instantiate()
    {
        if (instance == null)
            instance = new InputManager();

        return instance;
    }

    public float GetMovX()
    {
        return playerControls.Movement.MovX.ReadValue<float>();
    }

    public float GetMovY()
    {
        return playerControls.Movement.MovY.ReadValue<float>();
    }

    public bool IsRunning()
    {
        return playerControls.Movement.Run.ReadValue<float>() == 1f;
    }

    public bool IsAttacking()
    {
        return playerControls.Movement.Attack.ReadValue<float>() == 1f;
    }
}
