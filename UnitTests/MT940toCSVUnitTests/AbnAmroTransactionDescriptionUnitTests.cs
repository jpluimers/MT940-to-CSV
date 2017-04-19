using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MT940toCSV;

namespace MT940toCSVUnitTests
{
    [TestClass]
    public class AbnAmroTransactionDescriptionUnitTests
    {
        [TestMethod]
        public void SepaPeriodiekeOverb1_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("SEPA PERIODIEKE OVERB.           IBAN: NL05MHCB0631533929BIC: MHCBNL2A                    NAAM: DE INITIALENOMSCHRIJVING: SPAARPLAN");
            Assert.AreEqual("MHCBNL2A", subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.SepaPeriodiekeOverboeking, subject.TransactionType);
            Assert.AreEqual("NL05MHCB0631533929", subject.Iban);
            Assert.AreEqual(null, subject.Kenmerk);
            Assert.AreEqual("DE INITIALEN", subject.Naam);
            Assert.AreEqual("SPAARPLAN", subject.Omschrijving);
        }

        [TestMethod]
        public void SepaPeriodiekeOverb2_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("SEPA PERIODIEKE OVERB.           IBAN: NL27TRIO0395948217BIC: TRIONL2U                    NAAM: EEN ANDER PERSOONOMSCHRIJVING: KWARTAAL RENTE 1%  SCHENKING");
            Assert.AreEqual("TRIONL2U", subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.SepaPeriodiekeOverboeking, subject.TransactionType);
            Assert.AreEqual("NL27TRIO0395948217", subject.Iban);
            Assert.AreEqual(null, subject.Kenmerk);
            Assert.AreEqual("EEN ANDER PERSOON", subject.Naam);
            Assert.AreEqual("KWARTAAL RENTE 1%  SCHENKING", subject.Omschrijving);
        }

        [TestMethod]
        public void SepaOverboeking_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("SEPA OVERBOEKING                 IBAN: NL43SNSB0927873351BIC: SNSBNL2A                    NAAM: SLACHTHUIS HET VARKENOMSCHRIJVING: MAALTIJDEN BEZORGD  BON NR 1619 DECEMBER 2015");
            Assert.AreEqual("SNSBNL2A", subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.SepaOverboeking, subject.TransactionType);
            Assert.AreEqual("NL43SNSB0927873351", subject.Iban);
            Assert.AreEqual(null, subject.Kenmerk);
            Assert.AreEqual("SLACHTHUIS HET VARKEN", subject.Naam);
            Assert.AreEqual("MAALTIJDEN BEZORGD  BON NR 1619 DECEMBER 2015", subject.Omschrijving);
        }

        [TestMethod]
        public void SepaOverboeking_Salaris_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("SEPA OVERBOEKING                 IBAN: NL77DEUT0168784146BIC: DEUTNL2AXXX                 NAAM: AUTOMATIC DATA PROCESSINGLTD                              OMSCHRIJVING: HET BEDRIJF EEN DIENSTVERLENER                    KENMERK: 53789468");
            Assert.AreEqual("DEUTNL2AXXX", subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.SepaOverboeking, subject.TransactionType);
            Assert.AreEqual("NL77DEUT0168784146", subject.Iban);
            Assert.AreEqual("AUTOMATIC DATA PROCESSINGLTD", subject.Naam);
            Assert.AreEqual("HET BEDRIJF EEN DIENSTVERLENER", subject.Omschrijving);
        }

        [TestMethod]
        public void SepaIncassoAlgemeenDoorlopend_Ziggo_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("SEPA INCASSO ALGEMEEN DOORLOPEND INCASSANT: NL89ZIG370267060000NAAM: ZIGGO B.V.                 MACHTIGING: 95016206-0179415217OMSCHRIJVING: ARNL55709583325 JE  KLANTNUMMER 95016206         ABON T/M FEB. FACT.NR: 374063698 Z IEN  CHECK: ZIGGO.NL/MIJNZIGGOIBAN: NL77RABO0115856730");
            Assert.AreEqual(null, subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.SepaIncassoAlgemeenDoorlopend, subject.TransactionType);
            Assert.AreEqual("NL77RABO0115856730", subject.Iban);
            Assert.AreEqual("NL89ZIG370267060000", subject.Incassant);
            Assert.AreEqual("95016206-0179415217", subject.Machtiging);
            Assert.AreEqual("ZIGGO B.V.", subject.Naam);
            Assert.AreEqual("ARNL55709583325 JE  KLANTNUMMER 95016206         ABON T/M FEB. FACT.NR: 374063698 Z IEN  CHECK: ZIGGO.NL/MIJNZIGGO", subject.Omschrijving);
        }

        [TestMethod]
        public void SepaIncassoAlgemeenDoorlopend_Xs4all_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("SEPA INCASSO ALGEMEEN DOORLOPEND INCASSANT: NL70XS4332875340001NAAM: XS4ALL INTERNET B.V.       MACHTIGING: M17553429390OMSCHRIJVING: 1630565 BETREFT FA CTUUR D.D. 03-07-2016 INCL. 6,94BTW XS4ALL INTERNET BV          IBAN: NL25INGB0651581550KENMERK: 000000050490422");
            Assert.AreEqual(null, subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.SepaIncassoAlgemeenDoorlopend, subject.TransactionType);
            Assert.AreEqual("NL25INGB0651581550", subject.Iban);
            Assert.AreEqual("000000050490422", subject.Kenmerk);
            Assert.AreEqual("NL70XS4332875340001", subject.Incassant);
            Assert.AreEqual("M17553429390", subject.Machtiging);
            Assert.AreEqual("XS4ALL INTERNET B.V.", subject.Naam);
            Assert.AreEqual("1630565 BETREFT FA CTUUR D.D. 03-07-2016 INCL. 6,94BTW XS4ALL INTERNET BV", subject.Omschrijving);
        }

        [TestMethod]
        public void SepaIdeal_Bol_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("SEPA IDEAL                       IBAN: NL39INGB0681706103BIC: INGBNL2A                    NAAM: BOL.COM B.V.OMSCHRIJVING: 9212080300 9855623 900883122 BOL.COM BESTELLING 9212080300 BOL.COM                  KENMERK: 18-12-2016 19:48 9855623900883122");
            Assert.AreEqual("INGBNL2A", subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.SepaIdeal, subject.TransactionType);
            Assert.AreEqual("NL39INGB0681706103", subject.Iban);
            Assert.AreEqual("18-12-2016 19:48 9855623900883122", subject.Kenmerk);
            Assert.AreEqual(null, subject.Incassant);
            Assert.AreEqual(null, subject.Machtiging);
            Assert.AreEqual("BOL.COM B.V.", subject.Naam);
            Assert.AreEqual("9212080300 9855623 900883122 BOL.COM BESTELLING 9212080300 BOL.COM", subject.Omschrijving);
        }

        [TestMethod]
        public void SepaIdeal_Cak_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("SEPA ACCEPTGIROBETALING          IBAN: NL39ABNA0616385485BIC: ABNANL2A                    NAAM: CAKBETALINGSKENM.: 9120547590174    KENMERK: EB WLZ 2016-NOV FACT 20547590174");
            Assert.AreEqual("ABNANL2A", subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.SepaAcceptgirobetaling, subject.TransactionType);
            Assert.AreEqual("NL39ABNA0616385485", subject.Iban);
            Assert.AreEqual("EB WLZ 2016-NOV FACT 20547590174", subject.Kenmerk);
            Assert.AreEqual(null, subject.Incassant);
            Assert.AreEqual(null, subject.Machtiging);
            Assert.AreEqual("CAK", subject.Naam);
            Assert.AreEqual(null, subject.Omschrijving);
        }

        [TestMethod]
        public void Gea_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("GEA   NR:S2F936   26.04.17/14.31 HOOFDSTRAAT 398,PAS987");
            Assert.AreEqual(null, subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.Gea, subject.TransactionType);
            Assert.AreEqual(null, subject.Iban);
            Assert.AreEqual(null, subject.Kenmerk);
            Assert.AreEqual(null, subject.Incassant);
            Assert.AreEqual(null, subject.Machtiging);
            Assert.AreEqual(null, subject.Naam);
            Assert.AreEqual("S2F936   26.04.17/14.31 HOOFDSTRAAT 398,PAS987", subject.Nr);
            Assert.AreEqual(null, subject.Omschrijving);
        }

        [TestMethod]
        public void Bea_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("BEA   NR:32MK09   31.01.17/19.30 MEGA PRAXIS A DAM ZO AMS, PAS284");
            Assert.AreEqual(null, subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.Bea, subject.TransactionType);
            Assert.AreEqual(null, subject.Iban);
            Assert.AreEqual(null, subject.Kenmerk);
            Assert.AreEqual(null, subject.Incassant);
            Assert.AreEqual(null, subject.Machtiging);
            Assert.AreEqual(null, subject.Naam);
            Assert.AreEqual("32MK09   31.01.17/19.30 MEGA PRAXIS A DAM ZO AMS, PAS284", subject.Nr);
            Assert.AreEqual(null, subject.Omschrijving);
        }

        [TestMethod]
        public void AbnAmroBankNv_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("ABN AMRO BANK N.V.               BETAALGEMAK E               3,15BETAALPAS                   0,60");
            Assert.AreEqual(null, subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.AbnAmroBankNv, subject.TransactionType);
            Assert.AreEqual(null, subject.Iban);
            Assert.AreEqual("BETAALGEMAK E               3,15", subject.Kenmerk);
            Assert.AreEqual(null, subject.Incassant);
            Assert.AreEqual(null, subject.Machtiging);
            Assert.AreEqual(null, subject.Naam);
            Assert.AreEqual(null, subject.Nr);
            Assert.AreEqual("BETAALPAS                   0,60", subject.Omschrijving);
        }

        [TestMethod]
        public void AbnAmroBeleggen_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("ABNAMRO BELEGGEN                 SERVICEKOSTEN              47,99BTW                        10,08 ZIE UW NOTA VOOR DETAILS");
            Assert.AreEqual(null, subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.AbnAmroBeleggen, subject.TransactionType);
            Assert.AreEqual(null, subject.Iban);
            Assert.AreEqual("SERVICEKOSTEN              47,99", subject.Kenmerk);
            Assert.AreEqual(null, subject.Incassant);
            Assert.AreEqual(null, subject.Machtiging);
            Assert.AreEqual(null, subject.Naam);
            Assert.AreEqual(null, subject.Nr);
            Assert.AreEqual("BTW                        10,08 ZIE UW NOTA VOOR DETAILS", subject.Omschrijving);
        }

        [TestMethod]
        public void AfsluitingRenteEnOfKosten_Succeeds()
        {
            var subject = new AbnAmroTransactionDescription("AFSLUITING RENTE EN/OF KOSTEN    CREDITRENTE                9,43CCREDITRENTEOVERZICHT:            DIRECT SPARENVAN 30.06.2016 TOT 30.09.2016:   0,40%RENTE EFFECTIEF OP JAARBASIS");
            Assert.AreEqual(null, subject.Bic);
            Assert.AreEqual(AbnAmroTransactionDescription.TransactionTypes.AfsluitingRenteEnOfKosten, subject.TransactionType);
            Assert.AreEqual(null, subject.Iban);
            Assert.AreEqual("CREDITRENTE                9,43C", subject.Kenmerk);
            Assert.AreEqual(null, subject.Incassant);
            Assert.AreEqual(null, subject.Machtiging);
            Assert.AreEqual(null, subject.Naam);
            Assert.AreEqual(null, subject.Nr);
            Assert.AreEqual("CREDITRENTEOVERZICHT:            DIRECT SPARENVAN 30.06.2016 TOT 30.09.2016:   0,40%RENTE EFFECTIEF OP JAARBASIS", subject.Omschrijving);
        }

    }
}
