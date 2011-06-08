using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{

    class Program
    {
        static void Main(string[] args)
        {

            Modifier ArmorClass = new Modifier()
            {
                Name = "Misc",
                Base = 1,
                innerModifier = new Modifier()
                {
                    Name = "Dex",
                    Base = 5,
                    innerModifier = new ConstrainingModifier()
                    {
                        Name = "Shield",
                        Base = 5,
                        OverrideName = "Dex",
                        MaxOverrideValue = 2,
                        innerModifier = new Modifier()
                        {
                            Name = "Magic Armor",
                            Base = 4,
                            innerModifier = new ConstrainingModifier()
                            {
                                Name = "Armor",
                                Base = 8,
                                OverrideName = "Dex",
                                MaxOverrideValue = 3,
                                innerModifier = new Modifier()
                                {
                                    Name = "AC",
                                    Base = 10
                                }
                            }
                        }
                    }

                }
            };

            Console.WriteLine(ArmorClass.Value);

        }
    }

    public class Modifier
    {
        public string Name { get; set; }
        public int Base { get; set; }

        public Modifier innerModifier;

        public int GetConstrainedValue()
        {
            if (innerModifier != null)
            {
                return innerModifier.GetConstrainedValue(Name, Base);
            }
            else
            {
                return Base;
            }
        }

        public virtual int GetConstrainedValue(string name, int value)
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

        public int Value
        {
            get
            {
                if (innerModifier != null)
                {
                    return GetConstrainedValue() + innerModifier.Value;
                }
                else
                {
                    return GetConstrainedValue();
                }
            }
        }
    }

    public class ConstrainingModifier : Modifier
    {
        public string OverrideName { get; set; }
        public int MaxOverrideValue { get; set; }

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
