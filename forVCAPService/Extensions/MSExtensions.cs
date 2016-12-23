using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MusicStore.Extensions
{
    public static class MSExtensions
    {
        public static IConfigurationBuilder AddVcapServices(this IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Add(new VcapServicesConfigurationSource());
            return configurationBuilder;
        }
    }
}
