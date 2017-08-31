using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer.DataAbstraction;
using EPiServer.DataAbstraction.RuntimeModel;
using EPiServer.DataAbstraction.RuntimeModel.Internal;
using EPiServer.Framework.TypeScanner;
using EPiServer.ServiceLocation;
using Geta.Epi.ExtendedContentTypes.DataAnnotations;

namespace Geta.Epi.ExtendedContentTypes.DataAbstraction.RuntimeModel
{
    [ServiceConfiguration(typeof(IContentTypeModelScanner))]
    [ServiceConfiguration(typeof(ContentTypeModelScanner))]
    public class ExtendedContentTypeModelScanner : ContentTypeModelScanner
    {
        private IEnumerable<Type> _extendedContentTypeBaseTypes;

        protected readonly ITypeScannerLookup TypeScannerLookup;
        protected IEnumerable<Type> ExtendedContentTypeBaseTypes => _extendedContentTypeBaseTypes ?? (_extendedContentTypeBaseTypes = GetExtendedContentTypeBaseTypes());

        public ExtendedContentTypeModelScanner(ITypeScannerLookup typeScannerLookup, ContentTypeModelRegister typeModelRegister, ContentModelValidator modelValidator, ContentTypeModelRepository contentTypeModelRepository, IContentTypeModelAssigner contentTypeModelAssigner, ContentDataInterceptorHandler modelTypeInterceptorHandler, ContentDataInterceptor modelTypeInterceptor, IContentTypeModelFilter[] filters, IEnumerable<ContentScannerExtension> registerExtensions, IAvailableModelSettingsRepository availableContentTypeService, IContentTypeModelScannerEventsRaiser contentTypeModelScannerEvents) : base(typeScannerLookup, typeModelRegister, modelValidator, contentTypeModelRepository, contentTypeModelAssigner, modelTypeInterceptorHandler, modelTypeInterceptor, filters, registerExtensions, availableContentTypeService, contentTypeModelScannerEvents)
        {
            TypeScannerLookup = typeScannerLookup;
        }

        protected virtual IEnumerable<Type> GetExtendedContentTypeBaseTypes()
        {
            var extendedContentTypeBaseTypes = TypeScannerLookup.AllTypes
                .Where(t => t.GetCustomAttributes<ExtendedContentTypeAttribute>().Any())
                .Select(t => t.BaseType)
                .Distinct();

            return extendedContentTypeBaseTypes;
        }

        public override IEnumerable<Type> IgnoredTypes => base.IgnoredTypes.Union(ExtendedContentTypeBaseTypes);
    }
}