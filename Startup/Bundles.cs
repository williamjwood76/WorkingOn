using Microsoft.AspNetCore.ResponseCompression;

namespace Paychex_SimpleTimeClock.Startup
{
    public static class Bundles
    {
        public static void AddBundles(this IServiceCollection serviceCollection)
        {
            var globalCss = new[] {
                ""
            };

            var globalJs = new[] {
                ""
            };

            serviceCollection.AddResponseCompression(options => { 
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { 
                    "text/javascript" 
                }); 
            });

            serviceCollection.AddWebOptimizer(pipeline =>
            {
                #region Bundling                
                pipeline.AddCssBundle("/bundles/css/FileExplorer.css", globalCss);
                pipeline.AddJavaScriptBundle("/bundles/scripts/FileExplorer.js", globalJs);
                #endregion

                #region Minification                
                pipeline.MinifyCssFiles(new[]                    
                {                         
                    "css/**/*.css"                    
                }.ToArray());     
                
                pipeline.MinifyJsFiles(new[]                    
                {                        
                    "js/**/*.js"                    
                }.ToArray());                
                #endregion
            });

        }
    }
}
