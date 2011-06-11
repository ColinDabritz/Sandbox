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
            // test example of using the Modifiers
            // Note this is a bit hackish, and should probably be reworked
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
}
