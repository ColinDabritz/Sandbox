using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    public class Modifier
    {
        public string Name { get; set; }
        public int Base { get; set; }

        public Modifier innerModifier;

        // get the constrained value for this instances base value
        public int GetConstrainedValue()
        {
            if (innerModifier != null) // something down the chain, get that value by this name
            {
                return innerModifier.GetConstrainedValue(Name, Base);
            }
            else // nothing down the chain
            {
                return Base;
            }
        }


        // get the constrained value for this instances base value
        //  offering modifiers before this one (down the chain) the chance to
        //  constrain this value (by name)
        public virtual int GetConstrainedValue(string name, int value)
        {
            if (innerModifier != null) // something down the chain
            {
                return innerModifier.GetConstrainedValue(name, value);
            }
            else
            {
                return value; // nothing down the chain
            }
        }

        public int Value
        {
            get
            {
                if (innerModifier != null) // other values down the chain
                {
                    // get this instances value (constrained) plus previous instances
                    return GetConstrainedValue() + innerModifier.Value;
                }
                else // nothing down the chain
                {
                    // just get the (constrained) value
                    return GetConstrainedValue();
                }
            }
        }
    }
}
