using System.Runtime.Serialization;

namespace CodeInk.Core.Entities.OrderEntities;
public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    Pending,
    [EnumMember(Value = "PaymentReceived")]
    PaymentReceived,
    [EnumMember(Value = "PaymentFailed")]
    PaymentFailed,
}
