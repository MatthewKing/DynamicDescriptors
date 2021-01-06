# DynamicDescriptors

The WinForms [PropertyGrid](http://msdn.microsoft.com/en-us/library/system.windows.forms.propertygrid.aspx) is really useful, but it isn't particularly easy to customize - especially when binding to objects that are out of your control. DynamicDescriptors helps alleviate this pain by providing a runtime-customizable implementation of ICustomTypeDescriptor that can be bound to the PropertyGrid.

Properties can be customized with an easy-to-use fluent interface:

```csharp
var instanceToBind = new ExampleClass();

var descriptor = DynamicDescriptor.CreateFromInstance(instanceToBind);

descriptor.GetDynamicProperty("PropertyOne") // Get the property using its name.
    .SetDisplayName("Property #1")
    .SetDescription("The first property")
    .SetCategory("Example category");

descriptor.GetDynamicProperty((ExampleClass x) => x.Property2) // Get the property using an expression.
    .SetDisplayName("Property #2")
    .SetDescription("The second property")
    .SetCategory("Example category");

propertyGrid.SelectedObject = descriptor;
```

## Binding to an object instance

We can create a DynamicDescriptor for an object instance:

```csharp
var instance = new ExampleClass();

var descriptor = DynamicDescriptor.CreateFromInstance(instance);
```

## Binding to a dictionary

We can create a DynamicDescriptor backed by a dictionary. This will act as if the dictionary key/value pairs are properties of a bound object:

```csharp
var data = new Dictionary<string, object>();
data["Property1"] = "hello";
data["Property2"] = "world";

var descriptor = DynamicDescriptor.CreateFromDictionary(data);
```

We can also supply type information:

```csharp
var data = new Dictionary<string, object>();
data["Property1"] = "value";
data["Property2"] = 1;

var types = new Dictionary<string, Type>();
types["Property1"] = typeof(string);
types["Property2"] = typeof(int);

var descriptor = DynamicDescriptor.CreateFromDictionary(data, types);
```

## What can be customized?

**DisplayName:**

```csharp
descriptor.GetDynamicProperty("PropertyName").SetDisplayName("Property display name");
```

This modifies the value returned by the [DisplayName property](http://msdn.microsoft.com/en-us/library/system.componentmodel.memberdescriptor.displayname.aspx).

**Description:**

```csharp
descriptor.GetDynamicProperty("PropertyName").SetDescription("A description of the property");
```

This modifies the value returned by the [Description property](http://msdn.microsoft.com/en-us/library/system.componentmodel.memberdescriptor.description.aspx).

**Category:**

```csharp
descriptor.GetDynamicProperty("PropertyName").SetCategory("Category name");
```

This modifies the value returned by the [Category property](http://msdn.microsoft.com/en-us/library/system.componentmodel.memberdescriptor.category.aspx).

**Converter:**

```csharp
TypeConverter converter = /* your custom type converter */;
descriptor.GetDynamicProperty("PropertyName").SetConverter(converter);
```

This modifies the value returned by the [Converter property](http://msdn.microsoft.com/en-us/library/system.componentmodel.propertydescriptor.converter.aspx).

**IsReadOnly:**

```csharp
descriptor.GetDynamicProperty("PropertyName").SetReadOnly(true);
```

This modifies the value returned by the [IsReadOnly property](http://msdn.microsoft.com/en-us/library/system.componentmodel.propertydescriptor.isreadonly.aspx).

**Property order:**

```csharp
descriptor.GetDynamicProperty("PropertyOne").SetPropertyOrder(1);
descriptor.GetDynamicProperty("PropertyTwo").SetPropertyOrder(2);
descriptor.GetDynamicProperty("PropertyThree").SetPropertyOrder(3);
```

This modifies the order in which properties are returned by the [GetProperties method](http://msdn.microsoft.com/en-us/library/hc91c96t.aspx).

## Installation

Just grab it from [NuGet](https://www.nuget.org/packages/DynamicDescriptors/)

```
PM> Install-Package DynamicDescriptors
```

## License and copyright

Copyright Matthew King 2012-2020.
Distributed under the [MIT License](http://opensource.org/licenses/MIT). Refer to license.txt for more information.
