module ESCurrencyModel

open CurrencyModel

type Datum = { 
            Date: System.DateTime; 
            Datum: CurrencyModel.TimeSeriesDigitalCurrencyIntraday;
            Symbol: string;
            Market: string;
            }

let convertToESDatum symbolString (convertedObj: Option<CryptoModel>) = 
    match convertedObj with
     | None -> None
     | Some obj -> obj.TimeSeriesDigitalCurrencyIntraday 
                    |> Seq.map (fun (KeyValue(k,v)) -> 
                     {Date = System.DateTime.Parse k; Datum = v; Symbol = symbolString; Market= obj.MetaData.The4MarketCode})
                     |> Some            
