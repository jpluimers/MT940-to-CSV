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
            public const string Space = " ";
            public const string Slash = "/";
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            public const string Slashed_Ttrp = Slash + "TRTP" + Slash;
            public const string AbnAmroBankNv = "ABN AMRO BANK N.V.";
            public const string AbnAmroBeleggen = "ABNAMRO BELEGGEN";
            public const string AfsluitingRenteEnOfKosten = "AFSLUITING RENTE EN/OF KOSTEN";
            public const string Bea = "BEA";
            public const string Gea = "GEA";
            public const string SepaAcceptgirobetaling = "SEPA ACCEPTGIROBETALING";
            public const string Ideal = "IDEAL";
            public const string SepaIdeal = "SEPA " + Ideal;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            public const string SepaIdeal_Space = SepaIdeal + Space;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            public const string Trtp_Ideal = Slashed_Ttrp + Ideal;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            public const string Trtp_Ideal_Slash = Trtp_Ideal + Slash;
            public const string SepaIncassoAlgemeenDoorlopend = "SEPA INCASSO ALGEMEEN DOORLOPEND";
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            public const string SepaIncassoAlgemeenDoorlopend_Space = SepaIncassoAlgemeenDoorlopend + Space;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            public const string Trtp_SepaIncassoAlgemeenDoorlopend = Slashed_Ttrp + SepaIncassoAlgemeenDoorlopend;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            public const string Trtp_SepaIncassoAlgemeenDoorlopend_Slash = Trtp_SepaIncassoAlgemeenDoorlopend + Slash;
            public const string SepaOverboeking = "SEPA OVERBOEKING";
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            public const string SepaOverboeking_Space = SepaOverboeking + Space;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            public const string Trtp_SepaOverboeking = Slashed_Ttrp + SepaOverboeking;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
            public const string Trtp_SepaOverboeking_Slash = Trtp_SepaOverboeking + Slash;
            public const string SepaPeriodiekeOverboeking = "SEPA PERIODIEKE OVERB.";
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
        private class Space_Colon_FieldHeaders
        {
            /*
"SEPA INCASSO ALGEMEEN DOORLOPEND INCASSANT: NL70XS4332875340001NAAM: XS4ALL INTERNET B.V.       MACHTIGING: M17553429390OMSCHRIJVING: 1630565 BETREFT FA CTUUR D.D. 03-07-2016 INCL. 6,94BTW XS4ALL INTERNET BV          IBAN: NL25INGB0651581550KENMERK: 000000050490422"             */
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
        private class Trtp_FieldHeaders
        {
            /*
"/TRTP/SEPA INCASSO ALGEMEEN DOORLOPEND/CSID/NL70XS4332875340001/NAME/XS4ALL INTERNET B.V./MARF/M10009560656/REMI/FACTUUR 03-12-2019, VRAGEN  ZIE XS4ALL.NL/FACTUUR./IBAN/NL55INGB0004683839/BIC/INGBNL2A/EREF/141201960026294";
"/TRTP/SEPA OVERBOEKING/IBAN/NL03INGB0656744189/BIC/INGBNL2A/NAME/HR BAR/REMI/DELL OPTIPLEX MICRO / MINI PC MET SSD TYPE 3060-24GHFMFF INTEL NUC, INCLUSIEF VERZENDEN/EREF/NOTPROVIDED"
*/
            public const string Betalingskenmerk = "BETALINGSKENM.: ";
            public const string Bic = "/BIC/";
            public const string Iban = "/IBAN/";
            public const string Incassant = "/CSID/";
            public const string Kenmerk = "/EREF/";
            public const string Machtiging = "/MARF/";
            public const string Naam = "/NAME/";
            public const string Nr = "NR:";
            public const string Omschrijving = "/REMI/";
        }
        public string Betalingskenmerk { get; }
        public string Bic { get; }
        public string Iban { get; }
        public string Incassant { get; }
        public string Kenmerk { get; }
        public string Machtiging { get; }
        public string Naam { get; }
        public string Nr { get; }
        public string Omschrijving { get; }
        public string TransactionType { get; }
        public AbnAmroTransactionDescription(string transactionDescription)
        {
            var stream = transactionDescription;
            if (stream.StartsWith(TransactionTypes.AbnAmroBankNv, StringComparison.Ordinal) ||
                stream.StartsWith(TransactionTypes.AbnAmroBeleggen, StringComparison.Ordinal) ||
                stream.StartsWith(TransactionTypes.AfsluitingRenteEnOfKosten, StringComparison.Ordinal))
            {
                Omschrijving = stream.Substring(65);
                stream = stream.Substring(0, 65);
                Kenmerk = stream.Substring(33);
                stream = stream.Substring(0, 33);
            }
            else if (stream.StartsWith(TransactionTypes.Bea, StringComparison.Ordinal) || 
                     stream.StartsWith(TransactionTypes.Gea, StringComparison.Ordinal))
            {
                Nr = Extractor(Space_Colon_FieldHeaders.Nr, ref stream);
            }
            else if (stream.StartsWith(TransactionTypes.Trtp_SepaIncassoAlgemeenDoorlopend_Slash, StringComparison.Ordinal))
            {
                Kenmerk = Extractor(Trtp_FieldHeaders.Kenmerk, ref stream);
                Bic = Extractor(Trtp_FieldHeaders.Bic, ref stream);
                Iban = Extractor(Trtp_FieldHeaders.Iban, ref stream);
                Omschrijving = Extractor(Trtp_FieldHeaders.Omschrijving, ref stream);
                Machtiging = Extractor(Trtp_FieldHeaders.Machtiging, ref stream);
                Naam = Extractor(Trtp_FieldHeaders.Naam, ref stream);
                Incassant = Extractor(Trtp_FieldHeaders.Incassant, ref stream);
            }
            else if (stream.StartsWith(TransactionTypes.SepaIncassoAlgemeenDoorlopend_Space, StringComparison.Ordinal))
            {
                Kenmerk = Extractor(Space_Colon_FieldHeaders.Kenmerk, ref stream);
                Iban = Extractor(Space_Colon_FieldHeaders.Iban, ref stream);
                Omschrijving = Extractor(Space_Colon_FieldHeaders.Omschrijving, ref stream);
                Machtiging = Extractor(Space_Colon_FieldHeaders.Machtiging, ref stream);
                Naam = Extractor(Space_Colon_FieldHeaders.Naam, ref stream);
                Incassant = Extractor(Space_Colon_FieldHeaders.Incassant, ref stream);
            }
            else if (stream.StartsWith(TransactionTypes.Trtp_Ideal, StringComparison.Ordinal))
            {
                /*
                "/TRTP/SEPA OVERBOEKING/IBAN/NL43SNSB0927873351/BIC/SNSBNL2A/NAME/SLACHTHUIS HET VARKEN/REMI/MAALTIJDEN BEZORGD  BON NR 1619 DECEMBER 2015/EREF/NOTPROVIDED";
                */
                Kenmerk = Extractor(Trtp_FieldHeaders.Kenmerk, ref stream);
                Omschrijving = Extractor(Trtp_FieldHeaders.Omschrijving, ref stream);
                Naam = Extractor(Trtp_FieldHeaders.Naam, ref stream);
                Bic = Extractor(Trtp_FieldHeaders.Bic, ref stream);
                Iban = Extractor(Trtp_FieldHeaders.Iban, ref stream);
            }
            else if (stream.StartsWith(TransactionTypes.Trtp_SepaOverboeking_Slash, StringComparison.Ordinal))
            {
                /*
                "/TRTP/SEPA OVERBOEKING/IBAN/NL43SNSB0927873351/BIC/SNSBNL2A/NAME/SLACHTHUIS HET VARKEN/REMI/MAALTIJDEN BEZORGD  BON NR 1619 DECEMBER 2015/EREF/NOTPROVIDED";
                */
                Kenmerk = Extractor(Trtp_FieldHeaders.Kenmerk, ref stream);
                Omschrijving = Extractor(Trtp_FieldHeaders.Omschrijving, ref stream);
                Naam = Extractor(Trtp_FieldHeaders.Naam, ref stream);
                Bic = Extractor(Trtp_FieldHeaders.Bic, ref stream);
                Iban = Extractor(Trtp_FieldHeaders.Iban, ref stream);
            }
            else // seems to always assume TransactionTypes.SepaOverboeking_Space
            {
                // always in order "back to front" assuming the order is always the same
                Kenmerk = Extractor(Space_Colon_FieldHeaders.Kenmerk, ref stream);
                Omschrijving = Extractor(Space_Colon_FieldHeaders.Omschrijving, ref stream);
                if (stream.StartsWith(TransactionTypes.SepaAcceptgirobetaling, StringComparison.Ordinal))
                {
                    Betalingskenmerk = Extractor(Space_Colon_FieldHeaders.BetalingsKenmerk, ref stream);
                }
                Naam = Extractor(Space_Colon_FieldHeaders.Naam, ref stream);
                Bic = Extractor(Space_Colon_FieldHeaders.Bic, ref stream);
                Iban = Extractor(Space_Colon_FieldHeaders.Iban, ref stream);
            }
            TransactionType = stream.Trim();
        }
        private static string Extractor(string key, ref string stream)
        {
            var lastIndexOf = stream.LastIndexOf(key, StringComparison.Ordinal);
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
