using api.Models.EntityModel;

namespace api.Models.IntegrationModel
{
    public interface IAcquirerApi
    {
        CardTransaction Process(CardPayment cardPayment);
    }
}