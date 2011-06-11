using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    public class ConstrainingModifier : Modifier
    {
        // the name to override and the max value to limit it
        public string OverrideName { get; set; }
        public int MaxOverrideValue { get; set; }

        // gets the constrained value
        // will constrain the value if it matches the override name for this modifier
        public override int GetConstrainedValue(string name, int value)
        {
            if (name == OverrideName)
            {
                if (innerModifier != null)
                {
                    return Math.Min(MaxOverrideValue, innerModifier.GetConstrainedValue(name, value));
                }
                else
                {
                    return Math.Min(MaxOverrideValue, value);
                }
            }
            else
            {
                if (innerModifier != null)
                {
                    return innerModifier.GetConstrainedValue(name, value);
                }
                else
                {
                    return value;
                }
            }

        }
    }
}
