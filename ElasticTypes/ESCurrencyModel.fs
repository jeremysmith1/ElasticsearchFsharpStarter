module ESCurrencyModel

open CurrencyModel

type Datum = { 
            Date: string; 
            Datum: CurrencyModel.TimeSeriesDigitalCurrencyIntraday;
            Symbol: string;
            Market: string;
            }

let convertToESDatum symbolString (convertedObj: Option<CryptoModel>) = 
    match convertedObj with
     | None -> None
     | Some obj -> obj.TimeSeriesDigitalCurrencyIntraday 
                    |> Seq.map (fun (KeyValue(k,v)) -> 
                     {Date = k; Datum = v; Symbol = symbolString; Market= obj.MetaData.The4MarketCode})
                     |> Some            
