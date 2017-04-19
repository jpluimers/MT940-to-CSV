using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT940toCSV
{
    public class AbnAmroTransactionDescription
    {
        public class TransactionTypes
        {
            public const string AbnAmroBankNv = "ABN AMRO BANK N.V.";
            public const string AbnAmroBeleggen = "ABNAMRO BELEGGEN";
            public const string AfsluitingRenteEnOfKosten = "AFSLUITING RENTE EN/OF KOSTEN";
            public const string Bea = "BEA";
            public const string Gea = "GEA";
            public const string SepaAcceptgirobetaling = "SEPA ACCEPTGIROBETALING";
            public const string SepaIdeal = "SEPA IDEAL";
            public const string SepaIncassoAlgemeenDoorlopend = "SEPA INCASSO ALGEMEEN DOORLOPEND";
            public const string SepaOverboeking = "SEPA OVERBOEKING";
            public const string SepaPeriodiekeOverboeking = "SEPA PERIODIEKE OVERB.";
        }
        private class FieldHeaders
        {
            public const string BetalingsKenmerk = "BETALINGSKENM.: ";
            public const string Bic = "BIC: ";
            public const string Iban = "IBAN: ";
            public const string Incassant = "INCASSANT: ";
            public const string Kenmerk = "KENMERK: ";
            public const string Machtiging = "MACHTIGING: ";
            public const string Naam = "NAAM: ";
            public const string Nr = "NR:";
            public const string Omschrijving = "OMSCHRIJVING: ";
        }
        public string BetalingsKenmerk { get; }
        public string Bic { get; }
        public string Iban { get; }
        public string Incassant { get; }
        public string Kenmerk { get; }
        public string Machtiging { get; }
        public string Naam { get; }
        public string Nr { get; }
        public string Omschrijving { get; }
        public string TransactionType { get; }
        public AbnAmroTransactionDescription(string TransactionDescription)
        {
            var stream = TransactionDescription;
            //    public const string  = "ABN AMRO BANK N.V.";
            //public const string  = "ABNAMRO BELEGGEN";
            //public const string  = "AFSLUITING RENTE EN/OF KOSTEN";
            if (stream.StartsWith(TransactionTypes.AbnAmroBankNv) ||
                stream.StartsWith(TransactionTypes.AbnAmroBeleggen) ||
                stream.StartsWith(TransactionTypes.AfsluitingRenteEnOfKosten))
            {
                Omschrijving = stream.Substring(65);
                stream = stream.Substring(0, 65);
                Kenmerk = stream.Substring(33);
                stream = stream.Substring(0, 33);
            }
            else if (stream.StartsWith(TransactionTypes.Bea) || stream.StartsWith(TransactionTypes.Gea))
            {
                Nr = Extractor(FieldHeaders.Nr, ref stream);
            }
            else if (stream.StartsWith(TransactionTypes.SepaIncassoAlgemeenDoorlopend))
            {
                Kenmerk = Extractor(FieldHeaders.Kenmerk, ref stream);
                Iban = Extractor(FieldHeaders.Iban, ref stream);
                Omschrijving = Extractor(FieldHeaders.Omschrijving, ref stream);
                Machtiging = Extractor(FieldHeaders.Machtiging, ref stream);
                Naam = Extractor(FieldHeaders.Naam, ref stream);
                Incassant = Extractor(FieldHeaders.Incassant, ref stream);
            }
            else
            {
                // always in order "back to front" assuming the order is always the same
                Kenmerk = Extractor(FieldHeaders.Kenmerk, ref stream);
                Omschrijving = Extractor(FieldHeaders.Omschrijving, ref stream);
                if (stream.StartsWith(TransactionTypes.SepaAcceptgirobetaling))
                {
                    BetalingsKenmerk = Extractor(FieldHeaders.BetalingsKenmerk, ref stream);
                }
                Naam = Extractor(FieldHeaders.Naam, ref stream);
                Bic = Extractor(FieldHeaders.Bic, ref stream);
                Iban = Extractor(FieldHeaders.Iban, ref stream);
            }
            TransactionType = stream.Trim();
        }
        private string Extractor(string key, ref string stream)
        {
            var lastIndexOf = stream.LastIndexOf(key);
            if (lastIndexOf == -1)
            {
                return null;
            }
            else
            {
                var result = stream.Substring(lastIndexOf + key.Length).TrimEnd();
                stream = stream.Substring(0, lastIndexOf);
                return result;
            }
        }
    }
}
