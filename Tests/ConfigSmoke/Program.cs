using InvisibleManXRay.Models.Templates.Configs;

const string grpcLink = "vless://00000000-0000-0000-0000-000000000001@example.com:8447?encryption=none&security=reality&type=grpc&serviceName=vpn-grpc-x9m2&sni=yahoo.com&fp=chrome&pbk=test-public-key&sid=0123456789abcdef#grpc-test";
const string tcpLink = "vless://00000000-0000-0000-0000-000000000001@example.com:443?encryption=none&security=reality&type=tcp&flow=xtls-rprx-vision&sni=yahoo.com&fp=chrome&pbk=test-public-key&sid=0123456789abcdef#tcp-test";

VerifyGrpcProfile();
VerifyTcpVisionProfile();
Console.WriteLine("VLESS config smoke tests passed.");

void VerifyGrpcProfile()
{
    Vless template = Parse(grpcLink);
    V2Ray config = template.ConvertToV2Ray();
    V2Ray.Outbound outbound = config.outbounds.Single();

    Require(outbound.settings.vnext.Single().users.Single().flow == string.Empty, "gRPC REALITY flow must remain empty");
    Require(outbound.streamSettings.network == "grpc", "gRPC network was not preserved");
    Require(outbound.streamSettings.security == "reality", "REALITY security was not preserved");
    Require(outbound.streamSettings.grpcSettings.serviceName == "vpn-grpc-x9m2", "gRPC serviceName was not preserved");
}

void VerifyTcpVisionProfile()
{
    Vless template = Parse(tcpLink);
    V2Ray config = template.ConvertToV2Ray();
    string flow = config.outbounds.Single().settings.vnext.Single().users.Single().flow;

    Require(flow == "xtls-rprx-vision", "explicit TCP Vision flow was not preserved");
}

Vless Parse(string link)
{
    Vless template = new();
    template.FetchDataFromLink(link);
    return template;
}

void Require(bool condition, string message)
{
    if (!condition)
    {
        throw new InvalidOperationException(message);
    }
}
