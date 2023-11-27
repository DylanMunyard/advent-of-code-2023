using System.Net.Sockets;

namespace aspire_aoc.AppHost;

public class SeqContainerResource(string name) : ContainerResource(name), IResourceWithConnectionString
{
    /// <summary>
    /// Gets the connection string for the Seq server.
    /// </summary>
    /// <returns>A connection string for the Seq server in the form "host:port".</returns>
    public string GetConnectionString()
    {
        if (!this.TryGetAnnotationsOfType<AllocatedEndpointAnnotation>(out var allocatedEndpoints))
        {
            throw new DistributedApplicationException("Seq resource does not have endpoint annotation.");
        }

        // We should only have one endpoint for Seq for local scenarios.
        var endpoint = allocatedEndpoints.Single();
        return endpoint.EndPointString;
    }
}

public static class SeqExtensions
{
    public static IResourceBuilder<SeqContainerResource> AddSeqContainer(this IDistributedApplicationBuilder builder, string name)
    {
        var seq = new SeqContainerResource(name);
        return builder.AddResource(seq)
            .WithAnnotation(new ServiceBindingAnnotation(ProtocolType.Tcp, containerPort: 80))
            .WithAnnotation(new ContainerImageAnnotation { Image = "datalust/seq", Tag = "latest" })
            .WithAnnotation(new EnvironmentCallbackAnnotation("ACCEPT_EULA", () => "Y"));
    }
}
