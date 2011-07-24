using System;

namespace SilverlightRun.Tombstoning
{
    /// <summary>
    /// Automatically saves the value of the annotated public get&set property whenever the app deactivates.
    /// Automatically loads the value and assigns it to the right instance and property when the app activates.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SurvivesTombstoningAttribute : Attribute
    {
        public SurvivesTombstoningAttribute() { }
    }
}
