using System;

public interface IProjectUpdater 
{
    event Action UpdateCalled;
    event Action FixedUpdateCalled;
    event Action LateUpdateCalled;
}
