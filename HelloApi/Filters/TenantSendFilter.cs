using MassTransit;

namespace HelloApi.Filters
{
    public class TenantSendFilter<T> : IFilter<SendContext<T>>
        where T : class
    {
        private readonly Tenant tenant;

        public TenantSendFilter(Tenant tenant)
        {
            this.tenant = tenant;
        }
        public void Probe(ProbeContext context)
        {
            throw new NotImplementedException();
        }

        public Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
        {
            if(!string.IsNullOrEmpty(tenant.MyValue))
            {
                context.Headers.Set("tenant-from-send", tenant.MyValue);
            }

            return next.Send(context);
        }
    }
}
