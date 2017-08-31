using EPiServer.DataAbstraction.RuntimeModel;
using EPiServer.DataAbstraction.RuntimeModel.Internal;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Geta.Epi.ExtendedContentTypes.DataAbstraction.RuntimeModel;
using StructureMap;

namespace Geta.Epi.ExtendedContentTypes
{
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    [InitializableModule]
    public class ExtendedContentTypesInitialization : IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.StructureMap().Configure(ConfigureContainer);
        }

        private void ConfigureContainer(ConfigurationExpression container)
        {
            container.For<IContentTypeModelScanner>().Use<ExtendedContentTypeModelScanner>();
            container.For<ContentTypeModelScanner>().Use<ExtendedContentTypeModelScanner>();
        }
    }
}
