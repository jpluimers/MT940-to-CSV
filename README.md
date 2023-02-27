# MT940-to-CSV

Convert MT940 files to CSV using C# library SharpMt940Lib

For now the implementation hard coded for ABN AMRO MT940 format expecting `nl-NL` formatting of numbers.

Example ABN AMRO MT940 file used for testing from [wiki.yuki.nl](http://wiki.yuki.nl/Default.aspx?Page=ABNAMRO%20MT940%20voorbeeld).

Syntax:

    MT940toCSV.exe [MT940-FilePath...]

You supply zero or more `MT940-FilePath` entries. 

For each entry, it exports CSV by changing the extension of the source file.

Usually the FileName portion of `MT940-FilePath` looks like `MT940170417161106.STA` when downloaded from https://www.abnamro.nl/portalserver/mijn-abnamro/zelf-regelen/download-transacties/index.html#/downloadMutations-widget-mijn-abnamro/download-mutations

## `TRTP` Transaction types supported

A while ago, ABN-AMRO introduced `TRTP` transaction types.  
Documentation is in https://www.abnamro.nl/nl/media/SEPA_formaatverschillen_MT940_MT942_tcm16-73977.pdf (found via https://www.google.com/search?q=%22TRTP%22+abnamro&nfpr=1)

These transaction supported were implemented as we spotted them in the wild:
- `SEPA OVERBOEKING`
- `SEPA INCASSO ALGEMEEN DOORLOPEND`
- `IDEAL`

## Code

The code is still work in progress, as testing has been limited to producing the administration output that lay the foundation of reporting during close to 10 book-years.

A future target is to base this on .NET Standard instead of .NET Framework.

More ideas might be obtained from:
- https://github.com/akretion/banking/blob/master/account_banking_nl_abnamro/abnamro.py
- https://github.com/dovadi/mt940/blob/master/docs/MT940_voorbeeldbestanden_abnamro.txt

## Dependencies

Dependencies are via [NuGet](https://en.wikipedia.org/wiki/NuGet) through [`Applications/MT940toCSV/packages.config`](./Applications/MT940toCSV/packages.config)

It means you have to build it at least once in Visual Studio so the dependencies are resolved.

These are the current dependencies:

- [.NET Framework 4.5.2](https://en.wikipedia.org/wiki/.NET_Framework_version_history#.NET_Framework_4.5.2) or higher
- [CsvHelper 2.16.3.0](https://www.nuget.org/packages/csvhelper/2.16.3) (because it took very long for [3.0.0 and later](https://joshclose.github.io/CsvHelper/change-log) to come out) with source at <https://github.com/joshclose/csvhelper>
- [Raptorious.Finance.Swift.Mt940 1.2.0.4](https://www.nuget.org/packages/Raptorious.Finance.Swift.Mt940/1.2.0.4) with source at <https://bitbucket.org/raptux/sharpmt940lib>

## After conversion

The next step will be parsing the `TransactionDescription` field that is formatted contrary to what is described in [Formaatverschillen: Gevolgen SEPA voor MT940/942](https://www.abnamro.nl/en/images/Generiek/PDFs/020_Zakelijk/01_Betalingsverkeer/Formaatverschillen_SEPA_MT940_MT942_v1-2.pdf) section "2.3 Verschillen TAG 86 Mutatie Informatie".

The slash (`/`) separation described there is not used (it is only used in `TRTP` transactions); in stead some (but not all) fields are in this form:

    NAME: VALUE

Here:

- `NAME` cannot have spaces
- `VALUE` starts and ends with a non-space, but can contain spaces in other positions.

Observed `NAME` values for now:

- `OMSCHRIJVING`  
- `IBAN`  
- `KENMERK`  
- `CHECK`  
- `FACT.NR`  
- `RELATIENR`  
- `INCASSANT`  
- `BETALINGSKENM.`  
- `FACTUUR`  
- `BIC`  
- `NAAM`  
- `CREDITRENTEOVERZICHT`  
- `TERMIJN`  
- `MACHTIGING`  
- `RELATIENR`  
- `VLGNR`  
- `RENTE`  

The non-delimited first parts observed until now are:

- `SEPA ACCEPTGIROBETALING`  
- `SEPA IDEAL`  
- `SEPA INCASSO ALGEMEEN DOORLOPEND`  
- `SEPA OVERBOEKING`  
- `SEPA PERIODIEKE OVERB.`  
