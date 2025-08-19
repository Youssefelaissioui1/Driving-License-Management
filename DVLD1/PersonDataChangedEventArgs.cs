using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDBusinessLayer;
public class PersonDataChangedEventArgs : EventArgs
{
    public People Person { get; }

    public PersonDataChangedEventArgs(People person)
    {
        Person = person;
    }
}
