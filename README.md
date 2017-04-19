# MT940-to-CSV
Convert MT940 files to CSV using C# library SharpMt940Lib

For now the implementation hard coded for ABN AMRO MT940 format expecting `nl-NL` formatting of numbers.

Example ABN AMRO MT940 file used for testing from [wiki.yuki.nl](http://wiki.yuki.nl/Default.aspx?Page=ABNAMRO%20MT940%20voorbeeld).

Syntax:

    MT940toCSV.exe [MT940-FilePath...]

You supply zero or more `MT940-FilePath` entries. 

For each entry, it exports CSV by changing the extension of the source file.

Usually the FileName portion of `MT940-FilePath` looks like `MT940170417161106.STA` when downloaded from https://www.abnamro.nl/nl/paymentsreporting/downloadmutations.html

Note it is still work in progress.

The next step will be parsing the `TransactionDescription` field that is formatted contrary to what is described in "2.3 Verschillen TAG 86 Mutatie Informatie" from 2.3 Verschillen TAG 86 Mutatie Informatie.

The slash (`/`) separation described there is not used; in stead some (but not all) fields are in this form:

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
