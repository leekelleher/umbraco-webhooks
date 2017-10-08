using Umbraco.Core;
using Umbraco.Core.Models;

namespace Our.Umbraco.Webhooks4Umbraco.Extensions
{
    internal static class UmbracoEntitySerializationExtensions
    {
        public static object ToSerializableObject(this IContent entity)
        {
            var writer = ApplicationContext.Current.Services.UserService.GetUserById(entity.WriterId);

            return new
            {
                id = entity.Id,
                parentId = entity.ParentId,
                name = entity.Name,
                contentType = entity.ContentType.Alias,
                path = entity.Path,
                published = entity.Published,
                editorId = entity.WriterId,
                editorName = writer != null ? writer.Name : null,
                editorEmail = writer != null ? writer.Email : null,
                entityType = "content"
            };
        }

        public static object ToSerializableObject(this IMedia entity)
        {
            return new
            {
                id = entity.Id,
                parentId = entity.ParentId,
                name = entity.Name,
                contentType = entity.ContentType.Alias,
                path = entity.Path,
                entityType = "media"
            };
        }
    }
}