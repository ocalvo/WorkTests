using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Markup;

namespace Interop
{
    public class XamlApplication : Windows.UI.Xaml.Application, IXamlMetadataProvider
    {
        private List<IXamlMetadataProvider> _metadataProviders;

        private void EnsureMetadataProviders()
        {
            if (_metadataProviders != null)
            {
                return;
            }

            _metadataProviders = new List<IXamlMetadataProvider>();

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type == this.GetType())
                {
                    continue;
                }
                if (typeof(IXamlMetadataProvider).IsAssignableFrom(type))
                {
                    IXamlMetadataProvider provider = (IXamlMetadataProvider) Activator.CreateInstance(type);
                    _metadataProviders.Add(provider);
                }
            }
        }

        public IXamlType GetXamlType(Type type)
        {
            EnsureMetadataProviders();
            foreach (IXamlMetadataProvider provider in _metadataProviders)
            {
                IXamlType result = provider.GetXamlType(type);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public IXamlType GetXamlType(string fullName)
        {
            EnsureMetadataProviders();
            foreach (IXamlMetadataProvider provider in _metadataProviders)
            {
                IXamlType result = provider.GetXamlType(fullName);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public XmlnsDefinition[] GetXmlnsDefinitions()
        {
            EnsureMetadataProviders();
            List<XmlnsDefinition> definitions = new List<XmlnsDefinition>();
            foreach (IXamlMetadataProvider provider in _metadataProviders)
            {
                definitions.AddRange(provider.GetXmlnsDefinitions());
            }
            return definitions.ToArray();
        }
    }
}
