using MassTransit;

namespace HelloApi.Filters
{
    public class TenantConsumeFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        public void Probe(ProbeContext context)
        {
            throw new NotImplementedException();
        }

        public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            var tenantFromPublish = context.Headers.Get<string>("tenant-from-publish");
            var tenantFromSend = context.Headers.Get<string>("tenant-from-send");

            if (!string.IsNullOrEmpty(tenantFromPublish))
            {
                Console.WriteLine($"Tenant from publish: {tenantFromPublish}");
            }
            else if (!string.IsNullOrEmpty(tenantFromSend))
            {
                Console.WriteLine($"Tenant from send: {tenantFromSend}");
            }

            return next.Send(context);
        }
    }
}
