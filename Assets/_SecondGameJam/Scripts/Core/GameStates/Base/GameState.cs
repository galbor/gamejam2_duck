namespace _SecondGameJam.Scripts.Core.GameStates.Base
{
    /** Create GameStates using this interface */
    public interface IGameState
    {
        /** Called function on state enter */
        void EnterState();

        /** Cleanup function on state exit */
        void ExitState();

        /** Handles Logic and UI interactions on each frame */
        void UpdateState();
    }
}