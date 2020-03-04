using DotNetNinja.AutoBoundConfiguration;

namespace DotNetNinja.ApiTemplate.Configuration
{
    [AutoBind("OpenAPI")]
    public class OpenApiSettings
    {
        /// <summary>
        /// Requires application restart to take affect.
        /// </summary>
        public bool IsUserInterfaceEnabled { get; set; }

        public string TermsOfServiceUrl { get; set; }

        public string ServiceDescription { get; set; }

        public OpenApiContactSettings Contact { get; set; }

        public OpenApiLicenseSettings License { get; set; }

    }
}