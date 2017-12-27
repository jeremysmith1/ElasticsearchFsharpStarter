open ElasticWrapper
open Person
open AlphaAdv
open Newtonsoft.Json
open CurrencyModel
open System
open ESCurrencyModel

[<EntryPoint>]
let main argv =

    let values = { nameOfFunction= FunctionTypes.Daily; symbol= SymbolTypes.Bitcoin; market= MarketTypes.UnitedStatesDollar; }
    //todo: handle better
    let symbolString = symbolStringFromSymbol values.symbol
    let marketString = marketStringFromMarketType values.market

    let response = DigitalCurrencyRequest values

    let jsonClient= JsonConvert.DeserializeObject<Empty>(response) 

    let cryptoModel = jsonClient.TimeSeriesDigitalCurrencyDaily 
                        |> Seq.map (fun (KeyValue(k,v)) -> 
                            {Date = k; Datum = v; Symbol = symbolString; Market= marketString})

    let currencyClient = elasticsearchClientDefault (symbolString.ToLower())

    let result = BulkInsertCrypto currencyClient cryptoModel

    match result with
    | true -> printfn "Successfully Added"
    | false -> printfn "Could Not Add"

    0 // return an integer exit code