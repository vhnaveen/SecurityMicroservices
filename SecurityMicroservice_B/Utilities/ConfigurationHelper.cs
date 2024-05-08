namespace SecurityMicroservice_B.Utilities
{
    /// <summary>
    /// Name : ConfigurationHelper
    /// Description : This class is used for getting the appSetting.json file key values
    /// </summary>
    public static class ConfigurationHelper
    {
        #region GetValueByKey

        /// <summary>
        /// Read AppSetting GetValueByKey
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="key">string</param>
        /// <returns>string</returns>
        public static string GetValueByKey(this IConfiguration configuration, string key)
        {
            if (configuration == null)
                return string.Empty;

            try
            {
                return configuration.GetSection(key).Value ?? string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Read AppSetting GetValueByKey return generic value
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="key">string</param>
        /// <returns>T</returns>
        public static T? GetValueByKey<T>(this IConfiguration configuration, string key)
        {
            if (configuration == null)
                return default;

            try
            {
                return configuration.GetSection(key).Value == null ?
                    default :
                    (T?)Convert.ChangeType(configuration.GetSection(key).Value, typeof(T));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
