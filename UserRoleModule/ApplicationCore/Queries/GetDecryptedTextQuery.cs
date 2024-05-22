//using System;
//using MediatR;

//namespace UserRoleModule.ApplicationCore.Queries
//{
//    public class GetDecryptedTextQuery: IRequest<string>
//    {
//        public string Text { get; set; }

//        public GetDecryptedTextQuery(string text)
//        {
//            Text = text;
//        }
//    }

//public class GetDecryptedTextHandler: IRequestHandler<GetDecryptedTextQuery, string>
//{
//    private readonly ILogger<GetDecryptedTextHandler> _logger;
//    private readonly IDataProtectionProvider _provider;

//    public GetDecryptedTextHandler(ILogger<GetDecryptedTextHandler> logger, IDataProtectionProvider provider)
//    {
//        _logger = logger;
//        _provider = provider;
//    }

//    public Task<string> Handle(GetDecryptedTextQuery request, CancellationToken cancellationToken)
//    {
//        _logger.LogInformation("GetDecryptedStringHandler called");
//        // For more info check: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/purpose-strings?view=aspnetcore-5.0
//        var purpose = new string[] { GetType().Namespace, "CTAM.v1" };
//        var protector = _provider.CreateProtector(purpose);
//        return Task.Factory.StartNew(() => protector.Unprotect(request.Text));
//    }
//}

//}
