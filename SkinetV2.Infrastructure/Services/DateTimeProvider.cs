using SkinetV2.Application.common.Interfaces;

namespace SkinetV2.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}