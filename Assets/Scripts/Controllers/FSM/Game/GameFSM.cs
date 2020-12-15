public class GameFSM : FSM
{
    public GameState currentGameState;

    public void changeState(GameState newState)
    {
        if (currentGameState != null)
        {
            // Exit from previous state
            StartCoroutine(currentGameState.Exit());
        }

        // Set current state to a new one
        currentGameState = newState;

        // Initialize new phase
        StartCoroutine(currentGameState.Enter());
    }

}
