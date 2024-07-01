public interface IState<Enemy>
{
    void OnEnter(Enemy enemy);
    void OnExecute(Enemy enemy);
    void OnExit(Enemy enemy);
}
