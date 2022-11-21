using System;

public interface IExceptionLogger
{
    public void Log(Exception exception, string message);
}
