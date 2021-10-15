using System;

public class Mock
{
    public float Value
    {
        get;
        private set;
    }

    public string Argument
    {
        get;
        set;
    }

    public Mock CalledWith(string arg)
    {
        if(Argument != arg)
        {
            throw new Exception("Not called with argument: " + arg);
        }

        return this;
    }

    public void Returns(float value)
    {
        Value = value;
    }
}

