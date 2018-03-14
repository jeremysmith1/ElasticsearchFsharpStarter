module AlphaAdv

open MessageRestClient
open Secrets

let APIKey = AlphaAdvantageKey

type FunctionTypes = | IntraDaily = 1 | Daily = 2

let functionStringFromFunType =
    fun funType ->
    match funType with
    | FunctionTypes.IntraDaily -> "DIGITAL_CURRENCY_INTRADAY"
    | FunctionTypes.Daily -> "DIGITAL_CURRENCY_DAILY"
    | _ -> "unkown"


type MarketTypes = | UnitedStatesDollar = 1 | BritishPoundSterling = 2 | IndianRupee = 3

let marketStringFromMarketType =
    fun marketType ->
    match marketType with
    | MarketTypes.UnitedStatesDollar -> "USD"
    | MarketTypes.BritishPoundSterling -> "GBP"
    | MarketTypes.IndianRupee -> "INR"
    | _ -> "unkown"

type DigitalCurrencyParam = { nameOfFunction: FunctionTypes; symbol: string; market: MarketTypes; indexName: string }
     
let DigitalCurrencyRequest =
    fun args ->
    let functionString = functionStringFromFunType args.nameOfFunction
    let marketString = marketStringFromMarketType args.market

    let queryString = sprintf "query?function=%s&symbol=%s&market=%s&apikey=%s" 
                        functionString args.symbol marketString APIKey

    MessageBasedRestClient.MakeAlphaAdvRequest queryString 
    |> Async.RunSynchronously

let BuildRequestFromCurrenceySymbols =  
    fun currencySymbols -> currencySymbols 
                        |> Seq.map (fun symbol ->  
                                    {   nameOfFunction= FunctionTypes.Daily;
                                        symbol= (fst symbol); 
                                        market= MarketTypes.UnitedStatesDollar;
                                        indexName = (snd symbol) })
                        |> Seq.toList