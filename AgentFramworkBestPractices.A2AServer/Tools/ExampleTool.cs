using System;

namespace AgentFramworkBestPractices.A2AServer.Tools;

public class ExampleTool
{

    [Description("Retrieves invoices for the specified company and optionally within the specified time range")]
    public string QueryInvoices() => "Retrieves invoices for the specified company and optionally within the specified time range";

    [Description("Retrieves invoice using the transaction id")]
    public string QueryByTransactionId() => "Retrieves invoice using the transaction id";


    [Description("Retrieves invoice using the invoice id")]
    public string QueryByInvoiceId() => "Retrieves invoice using the invoice id";
}
