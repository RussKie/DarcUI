// Copyright (c) Igor Velikorossov. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;

namespace DarcUI.CustomControls;

internal class ReadOnlySubscriptionProxy : CustomTypeDescriptor
{
    public object WrappedObject { get; private set; }
    public List<string> EditableProperties { get; private set; }

    public ReadOnlySubscriptionProxy(object o)
        : base(TypeDescriptor.GetProvider(o).GetTypeDescriptor(o))
    {
        WrappedObject = o;
        EditableProperties = new List<string>()
        {
            nameof(SubscriptionProxy.Enabled),
            nameof(SubscriptionProxy.Batchable),
            nameof(SubscriptionProxy.UpdateFrequency),
            nameof(SubscriptionProxy.TokenFailureNotificationTags),
        };
    }

    public override PropertyDescriptorCollection GetProperties() => GetProperties(Array.Empty<Attribute>());

    public override PropertyDescriptorCollection GetProperties(Attribute[]? attributes)
    {
        List<PropertyDescriptor> result = new();

        IEnumerable<PropertyDescriptor> properties = base.GetProperties(attributes).Cast<PropertyDescriptor>();

        foreach (PropertyDescriptor propertyDescriptor in properties)
        {
            PropertyDescriptor? resultPropertyDescriptor;

            if (!EditableProperties.Contains(propertyDescriptor.Name))
            {
                List<Attribute> atts = propertyDescriptor.Attributes.Cast<Attribute>().ToList();
                atts.RemoveAll(a => a.GetType().Equals(typeof(ReadOnlyAttribute)));
                atts.Add(new ReadOnlyAttribute(true));

                resultPropertyDescriptor = TypeDescriptor.CreateProperty(WrappedObject.GetType(), propertyDescriptor, atts.ToArray());
            }
            else
            {
                resultPropertyDescriptor = propertyDescriptor;
            }

            result.Add(resultPropertyDescriptor);
        }

        return new PropertyDescriptorCollection(result.ToArray());
    }
}
