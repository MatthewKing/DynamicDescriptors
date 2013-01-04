DynamicDescriptors
==================

The WinForms [PropertyGrid](http://msdn.microsoft.com/en-us/library/system.windows.forms.propertygrid.aspx) is really useful, but it isn't particularly easy to customize - especially when binding to objects that are out of your control. DynamicDescriptors helps alleviate this pain by providing a runtime-customizable implementation of ICustomTypeDescriptor that can be bound to the PropertyGrid.

Properties can be customized with an easy-to-use fluent interface:

```csharp
var instanceToBind = new ExampleClass();

var descriptor = DynamicDescriptor.Create(instanceToBind);

descriptor.GetProperty("PropertyOne")
    .SetDisplayName("Property #1")
    .SetDescription("The first property")
    .SetCategory("Example category");
    
descriptor.GetProperty("PropertyTwo")
    .SetDisplayName("Property #2")
    .SetDescription("The second property")
    .SetCategory("Example category");

propertyGrid.SelectedObject = descriptor;
```

Copyright
---------
Copyright Matthew King 2012.

License
-------
DynamicDescriptors is licensed under the [Boost Software License](http://www.boost.org/users/license.html). Refer to license.txt for more information.