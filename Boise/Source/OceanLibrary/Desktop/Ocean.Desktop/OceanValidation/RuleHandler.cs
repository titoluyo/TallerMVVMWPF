using System;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents a delegate RuleHanlder
    /// </summary>
    public delegate Boolean RuleHandler(Object target, RuleDescriptorBase e);

    /// <summary>
    ///  Represents a delegate RuleHanlder
    /// </summary>
    public delegate Boolean RuleHandler<in T, in TR>(T target, TR e) where TR : RuleDescriptorBase;

}