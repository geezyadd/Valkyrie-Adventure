
public interface IEtityInputSource 
{
    float _direction { get; }
    bool Jump { get; }
    bool Attack { get; }
    void ResetOneTimeActions();  
}
