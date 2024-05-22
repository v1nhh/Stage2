//using System;
//using MediatR;

//namespace UserRoleModule.ApplicationCore.Queries
//{
//    public class GetEncryptedTextQuery: IRequest<string>
//    {
//        public string Text { get; set; }

//        public GetEncryptedTextQuery(string text)
//        {
//            Text = text;
//        }
//    }

//public class GetEncryptedTextHandler : IRequestHandler<GetEncryptedTextQuery, string>
//{
//    private readonly ILogger<GetEncryptedTextHandler> _logger;
//    private readonly IDataProtectionProvider _provider;

//    public GetEncryptedTextHandler (ILogger<GetEncryptedTextHandler> logger, IDataProtectionProvider provider)
//    {
//        _logger = logger;
//        _provider = provider;
//    }

//    public Task<string> Handle(GetEncryptedTextQuery request, CancellationToken cancellationToken)
//    {
//        _logger.LogInformation("GetEncryptedStringHandler called");
//        // For more info check: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/purpose-strings?view=aspnetcore-5.0
//        var purpose = new string[] { GetType().Namespace, "CTAM.v1" };
//        var protector = _provider.CreateProtector(purpose);
//        return Task.Factory.StartNew(() => protector.Protect(request.Text));
//    }
//}

//}
