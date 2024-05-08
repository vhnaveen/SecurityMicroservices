using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SecurityMicroservice_A.Utilities
{
    /// <summary>
    /// This class is used to create the filter to add the prefix to all the APIs
    /// </summary>
    public class SwaggerPathPrefixFilter : IDocumentFilter
    {
        #region Private properties

        private readonly string _pathPrefix;

        #endregion Private properties

        #region Public properties

        /// <summary>
        /// Public OpenApiDocument property for unit testing
        /// </summary>
        public OpenApiDocument? OpenApiDocument { get; set; }

        #endregion Public properties

        #region Constructor

        /// <summary>
        /// Constructor with parameter of the prefix
        /// </summary>
        /// <param name="prefix"></param>
        public SwaggerPathPrefixFilter(string prefix)
        {
            this._pathPrefix = prefix;
        }

        #endregion Constructor

        #region Interface implementation

        /// <summary>
        /// This method is used to replaces the existing path with the prefix + path
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            #region Set the data for the unit testing

            OpenApiDocument = swaggerDoc;

            #endregion Set the data for the unit testing

            var paths = swaggerDoc.Paths.Keys.ToList();
            foreach (var path in paths)
            {
                var pathToChange = swaggerDoc.Paths[path];
                swaggerDoc.Paths.Remove(path);
                swaggerDoc.Paths.Add($"/{_pathPrefix}{path}", pathToChange);
            }
        }

        #endregion Interface implementation
    }
}
