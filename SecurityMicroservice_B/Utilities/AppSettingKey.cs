namespace SecurityMicroservice_B.Utilities
{
    /// <summary>
    /// Name : AppSettingKey
    /// Description : Class used for getting application setting file keys
    /// </summary>
    public sealed class AppSettingKey
    {

        #region Message Key: Common

        /// <summary>
        /// Key for the CORS origins
        /// </summary>
        public const string CORS_ORIGIN = "CORS_Origins";

        #endregion Message Key: Common


        #region Application specific keys

        /// <summary>
        /// AppSetting key to retrieve the environment
        /// </summary>
        public const string ENVIRONMENT = "Application:Environment";

        /// <summary>
        /// Gets the information about the default route schema for the application
        /// </summary>
        public const string DEFAULT_ROUTE_SCHEMA = "Application:DefaultRouteSchema";

        /// <summary>
        /// AppSetting key to retrieve the ApplicationURL
        /// </summary>
        public const string APPLICATION_URL = "Application:ApplicationURL";

        /// <summary>
        /// AppSetting key to retrieve the PST_TimeZoneInfo
        /// </summary>
        public const string PST_TIME_ZONE_INFO = "Application:PST_TimeZoneInfo";

        #endregion

        #region Azure AD: keys section

        /// <summary>
        /// Azure instance key
        /// </summary>
        public const string AZURE_AD_INSTANCE_KEY = "AzureAd:Instance";

        /// <summary>
        /// Azure domain key
        /// </summary>
        public const string AZURE_AD_DOMAIN_KEY = "AzureAd:Domain";

        /// <summary>
        /// Azure tenant id key
        /// </summary>
        public const string AZURE_AD_TENANT_ID_KEY = "AzureAd:TenantId";

        /// <summary>
        /// Azure client id key
        /// </summary>
        public const string AZURE_AD_CLIENT_ID_KEY = "AzureAd:ClientId";

        /// <summary>
        /// Azure AD scopes key
        /// </summary>
        public const string AZURE_AD_SCOPES_KEY = "AzureAd:Scopes";

        /// <summary>
        /// Azure callback path key
        /// </summary>
        public const string AZURE_AD_CALLBACK_PATH_KEY = "AzureAd:CallbackPath";

        /// <summary>
        /// Azure audience key
        /// </summary>
        public const string AZURE_AD_AUDIENCE_KEY = "AzureAd:Audience";

        /// <summary>
        /// AD authorization URL segment suffix
        /// </summary>
        public const string MICROSOFT_AD_AUTH_URL_SUFFIX = "AzureAdUrlSuffixes:MicrosoftAdAuthUrlSuffix";

        /// <summary>
        /// AD token URL segment suffix
        /// </summary>
        public const string MICROSOFT_AD_TOKEN_URL_SUFFIX = "AzureAdUrlSuffixes:MicrosoftAdTokenUrlSuffix";

        #endregion Azure AD keys section
    }
}
