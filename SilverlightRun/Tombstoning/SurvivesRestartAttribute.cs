using System;

namespace SilverlightRun.Tombstoning
{
    /// <summary>
    /// Automatically saves the value of the annotated public get&set property whenever the app closes/deactivates.
    /// Automatically loads the value and assigns it to the right instance and property when the app launches/activates.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SurvivesRestartAttribute : Attribute
    {
    }
}
