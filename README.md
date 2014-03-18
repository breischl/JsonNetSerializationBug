JsonNet Serialization Bug Repro
===============================

This is an attempt to reproduce what I believe to be a bug in Json.Net. It's not consistently reproducible - I 
get a failure in about 33% of test runs. In a failure, Json.Net will not serialize one or more properties
of the input object.

The test will fail because one or more properties have not been serialized. If you read the trace output, 
you'll see that this is because the `ShouldSerialize for property '[whatever]'` is false. ie, Json.Net decided
that the property shouldn't be serialized. 

Samples
=======
I've included some sample output from failures on my machine, in the Failures folder.

Conjecture About the Source
===========================
Under the covers Json.Net generates IL to call the `ShouldSerializeXXX()` method for each property (where `XXX` is the property name).
I believe that the IL generation has a subtle bug that causes it to not actually call the `ShouldSerializeXXX()` 
method if that method is declared `virtual` and is not `final` or a constructor. Instead it just returns whatever value is on the stack, which is undefined. 
I believe the relevant source is [here](https://github.com/JamesNK/Newtonsoft.Json/blob/44bc7ea2462e0cd858ad68a8918938ebfdea8b0b/Src/Newtonsoft.Json/Utilities/DynamicReflectionDelegateFactory.cs#L90).

Based on that, changing method signatures from `public virtual bool ShouldSerializeXXX()`
to `public bool ShouldSerializeXXX()` should work around the bug. In my testing that change does appear to avoid the bug.

