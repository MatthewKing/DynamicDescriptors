DynamicDescriptors
==================

The WinForms [PropertyGrid](http://msdn.microsoft.com/en-us/library/system.windows.forms.propertygrid.aspx) is really useful, but it isn't particularly easy to customize - especially when binding to objects that are out of your control. DynamicDescriptors helps alleviate this pain by providing a runtime-customizable implementation of ICustomTypeDescriptor that can be bound to the PropertyGrid.

Properties can be customized with an easy-to-use fluent interface:

```csharp
var instanceToBind = new ExampleClass();

var descriptor = DynamicDescriptor.CreateFromInstance(instanceToBind);

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

What can be customized?
-----------------------

**DisplayName:**
```csharp
descriptor.GetProperty("PropertyName").SetDisplayName("Property display name");
```
This modifies the value returned by the [DisplayName property](http://msdn.microsoft.com/en-us/library/system.componentmodel.memberdescriptor.displayname.aspx).

**Description:**
```csharp
descriptor.GetProperty("PropertyName").SetDescription("A description of the property");
```
This modifies the value returned by the [Description property](http://msdn.microsoft.com/en-us/library/system.componentmodel.memberdescriptor.description.aspx).

**Category:**
```csharp
descriptor.GetProperty("PropertyName").SetCategory("Category name");
```
This modifies the value returned by the [Category property](http://msdn.microsoft.com/en-us/library/system.componentmodel.memberdescriptor.category.aspx).

**Converter:**
```csharp
TypeConverter converter = /* your custom type converter */;
descriptor.GetProperty("PropertyName").SetConverter(converter);
```
This modifies the value returned by the [Converter property](http://msdn.microsoft.com/en-us/library/system.componentmodel.propertydescriptor.converter.aspx).

**IsReadOnly:**
```csharp
descriptor.GetProperty("PropertyName").SetReadOnly(true);
```
This modifies the value returned by the [IsReadOnly property](http://msdn.microsoft.com/en-us/library/system.componentmodel.propertydescriptor.isreadonly.aspx).

**Property order:**
```csharp
descriptor.GetProperty("PropertyOne").SetPropertyOrder(1);
descriptor.GetProperty("PropertyTwo").SetPropertyOrder(2);
descriptor.GetProperty("PropertyThree").SetPropertyOrder(3);
```
This modifies the order in which properties are returned by the [GetProperties method](http://msdn.microsoft.com/en-us/library/hc91c96t.aspx).

Copyright
---------
Copyright Matthew King 2012-2013.

License
-------
DynamicDescriptors is licensed under the [Boost Software License](http://www.boost.org/users/license.html). Refer to license.txt for more information.