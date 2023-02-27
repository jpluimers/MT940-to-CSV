using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raptorious.SharpMt940Lib;
using Raptorious.SharpMt940Lib.Mt940Format;
using CsvHelper;

namespace MT940toCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var mt940FilePath in args)
            {
                if (File.Exists(mt940FilePath))
                    ProcessCsvFile(mt940FilePath);
            }
        }

        private static void ProcessCsvFile(string mt940FilePath)
        {
            var cultureInfo = new CultureInfo("nl-NL"); // ABN-AMRO uses decimal comma; https://en.wikipedia.org/wiki/Decimal_mark#Countries_using_Arabic_numerals_with_decimal_comma
            ICollection<CustomerStatementMessage> statements = Mt940Parser.Parse(new AbnAmro(), mt940FilePath, cultureInfo);

            var mapped = FlattenThenMap(statements);

            string csvFilePath = Path.ChangeExtension(mt940FilePath, ".csv");
            using (TextWriter writer = new StreamWriter(csvFilePath))
            {
                var csv = new CsvWriter(writer);
                csv.Configuration.Encoding = Encoding.UTF8;
                csv.WriteRecords(mapped);
            }
        }
        private static IEnumerable FlattenThenMap(ICollection<CustomerStatementMessage> statements)
        {
            var flattened = from statement in statements
                            from transaction in statement.Transactions
                            select new { statement, transaction };
            var mapped = flattened.Select(item =>
            {
                var statement = item.statement;
                var transaction = item.transaction;
                TransactionBalance closingAvailableBalance = statement.ClosingAvailableBalance;
                TransactionBalance closingBalance = statement.ClosingBalance;
                TransactionBalance forwardAvailableBalance = statement.ForwardAvailableBalance;
                TransactionBalance openingBalance = statement.OpeningBalance;
                TransactionDetails transactionDetails = transaction.Details;
                var transactionDescription = transaction.Description.Replace(Environment.NewLine, "");
                AbnAmroTransactionDescription abnAmroTransactionDescription = new AbnAmroTransactionDescription(transactionDescription);
                var entry = new
                {
                    // Statement:
                    Account = statement.Account,
                    ClosingAvailableBalance = closingAvailableBalance,
                    CAB_Balance = closingAvailableBalance?.Balance?.Value,
                    CAB_Currency = closingAvailableBalance?.Currency?.Code,
                    CAB_DebitCredit = closingAvailableBalance?.DebitCredit,
                    CAB_EntryDate = closingAvailableBalance?.EntryDate.ToIso8601DateOnly(),
                    ClosingBalance = closingBalance,
                    CB_Balance = closingBalance?.Balance?.Value,
                    CB_Currency = closingBalance?.Currency?.Code,
                    CB_DebitCredit = closingBalance?.DebitCredit,
                    CB_EntryDate = closingBalance?.EntryDate.ToIso8601DateOnly(),
                    StatementDescription = statement.Description,
                    ForwardAvailableBalance = statement.ForwardAvailableBalance,
                    FAB_Balance = forwardAvailableBalance?.Balance?.Value,
                    FAB_Currency = forwardAvailableBalance?.Currency?.Code,
                    FAB_DebitCredit = forwardAvailableBalance?.DebitCredit,
                    FAB_EntryDate = forwardAvailableBalance?.EntryDate.ToIso8601DateOnly(),
                    OpeningBalance = statement.OpeningBalance,
                    OB_Balance = openingBalance?.Balance?.Value,
                    OB_Currency = openingBalance?.Currency?.Code,
                    OB_DebitCredit = openingBalance?.DebitCredit,
                    OB_EntryDate = openingBalance?.EntryDate.ToIso8601DateOnly(),
                    RelatedMessage = statement.RelatedMessage,
                    SequenceNumber = statement.SequenceNumber,
                    StatementNumber = statement.StatementNumber,
                    TransactionReference = statement.TransactionReference,
                    // Transaction:
                    AccountServicingReference = transaction.AccountServicingReference,
                    Amount = transaction.Amount,
                    Currency = transaction.Amount.Currency,
                    Value = transaction.Amount.Value,
                    DebitCredit = transaction.DebitCredit,
                    TransactionDescription = transactionDescription,
                    AATD_BetalingsKenmerk = abnAmroTransactionDescription.BetalingsKenmerk,
                    AATD_Bic = abnAmroTransactionDescription.Bic,
                    AATD_Iban = abnAmroTransactionDescription.Iban,
                    AATD_Incassant = abnAmroTransactionDescription.Incassant,
                    AATD_Kenmerk = abnAmroTransactionDescription.Kenmerk,
                    AATD_Machtiging = abnAmroTransactionDescription.Machtiging,
                    AATD_Naam = abnAmroTransactionDescription.Naam,
                    AATD_Nr = abnAmroTransactionDescription.Nr,
                    AATD_Omschrijving = abnAmroTransactionDescription.Omschrijving,
                    AATD_TransactionType = abnAmroTransactionDescription.TransactionType,
                    TransactionDetails = transactionDetails,
                    TD_Account = transactionDetails.Account,
                    TD_Description = transactionDetails.Description,
                    TD_Name = transactionDetails.Name,
                    EntryDate = transaction.EntryDate?.ToIso8601DateOnly(),
                    FundsCode = transaction.FundsCode,
                    Reference = transaction.Reference,
                    SupplementaryDetails = transaction.SupplementaryDetails,
                    TransactionType = transaction.TransactionType,
                    TransactionValue = transaction.Value,
                    ValueDate = transaction.ValueDate.ToIso8601DateOnly(),
                    /*
                        Statement:

            public string Account { get; }
            public TransactionBalance ClosingAvailableBalance { get; }
            public TransactionBalance ClosingBalance { get; }
            public string Description { get; }
            public TransactionBalance ForwardAvailableBalance { get; }
            public TransactionBalance OpeningBalance { get; }
            public string RelatedMessage { get; }
            public int SequenceNumber { get; }
            public int StatementNumber { get; }
            public string TransactionReference { get; }
            public ICollection<Transaction> Transactions { get; }

                    TransactionBalance:

            public Money Balance { get; }
            public Currency Currency { get; }
            public DebitCredit DebitCredit { get; }
            public DateTime EntryDate { get; }

                    Money:

            public Currency Currency { get; }
            public decimal Value { get; }

                    Transaction:

            public string AccountServicingReference { get; }
            public Money Amount { get; }
            public DebitCredit DebitCredit { get; }
            public string Description { get; set; }
            public TransactionDetails Details { get; set; }
            public DateTime? EntryDate { get; }
            public string FundsCode { get; set; }
            public string Reference { get; }
            public string SupplementaryDetails { get; }
            public string TransactionType { get; }
            public string Value { get; set; }
            public DateTime ValueDate { get; }

                    TransactionDetails:

            public string Account { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }

                        */
                };
                return entry;
            });
            return mapped;
        }
    }
}
