module AlphaAdv

open RestSharp
open Secrets

let APIKey = AlphaAdvantageKey

//Consider using the CSV instead of naming explicitly
type FunctionTypes = | IntraDaily = 1 | Daily = 2

let functionStringFromFunType funType =
    match funType with
    | FunctionTypes.IntraDaily -> "DIGITAL_CURRENCY_INTRADAY"
    | FunctionTypes.Daily -> "DIGITAL_CURRENCY_DAILY"
    | _ -> "unkown"

type SymbolTypes = | Bitcoin = 1 | Dogecoin = 2 | Ethereum = 3 | Litecoin = 4

let symbolStringFromSymbol currencyType =
    match currencyType with
    | SymbolTypes.Bitcoin -> "BTC"
    | SymbolTypes.Dogecoin -> "DOGE"
    | SymbolTypes.Ethereum -> "ETH"
    | SymbolTypes.Litecoin -> "LTC"
    | _ -> "unkown"

type MarketTypes = | UnitedStatesDollar = 1 | BritishPoundSterling = 2 | IndianRupee = 3

let marketStringFromMarketType marketType =
    match marketType with
    | MarketTypes.UnitedStatesDollar -> "USD"
    | MarketTypes.BritishPoundSterling -> "GBP"
    | MarketTypes.IndianRupee -> "INR"
    | _ -> "unkown"

type DigitalCurrencyParam = { nameOfFunction: FunctionTypes; symbol: SymbolTypes; market: MarketTypes; }

let AlphaAdvClient = RestClient("https://www.alphavantage.co")
let DigitalCurrencyRequest args =
    let functionString = functionStringFromFunType args.nameOfFunction
    let symbolString = symbolStringFromSymbol args.symbol
    let marketString = marketStringFromMarketType args.market

    let queryString = sprintf "query?function=%s&symbol=%s&market=%s&apikey=%s" 
                        functionString symbolString marketString APIKey
                       
    let request = RestRequest queryString

    let response = AlphaAdvClient.Execute request

    // convertStringToObject 
    response.Content


