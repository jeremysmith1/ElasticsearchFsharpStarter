module ESCurrencyModel

open CurrencyModel
open Newtonsoft.Json

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

let TryParseDatum =
    fun json -> 
    let convertedObj =
        try
            Some(JsonConvert.DeserializeObject<CryptoModel>(json))
        with
            | _ -> None                

    if isNull convertedObj.Value.MetaData
    then None
    else convertedObj
  