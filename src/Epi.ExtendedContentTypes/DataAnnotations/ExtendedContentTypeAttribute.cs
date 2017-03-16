using System;
using EPiServer.DataAnnotations;

namespace Geta.Epi.ExtendedContentTypes.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ExtendedContentTypeAttribute : ContentTypeAttribute
    {
    }
}