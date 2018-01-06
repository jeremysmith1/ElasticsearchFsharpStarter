open ElasticWrapper
open AlphaAdv
open Newtonsoft.Json
open CurrencyModel
open System
open ESCurrencyModel

[<EntryPoint>]
let main argv =

    //Retrieve all Currency symbols we can collect (currency code,currency name)
    let currencySymbols = 
        IO.File.ReadLines(@".\Resource\CurrencySymbols.csv") 
        |> Seq.skip 1
        |> Seq.map ((fun (line: string) -> line.Split ',') 
        >> (fun (values: string[]) -> (values.[0], values.[1]))) 


    let convertResponse =
        fun singleResponse -> 
        let convertedObj = JsonConvert.DeserializeObject<CryptoModel>(singleResponse) 
        if isNull convertedObj.MetaData 
        then None 
        else Some convertedObj


    let requestCollection =  
        currencySymbols 
        |>Seq.map (fun symbol ->  { nameOfFunction= FunctionTypes.IntraDaily;
                                    symbol= (fst symbol); 
                                    market= MarketTypes.UnitedStatesDollar;
                                    indexName = (snd symbol) })
        |> Seq.toList


    for request in requestCollection do
        
        DigitalCurrencyRequest request 
        |> convertResponse 
        |> convertToESDatum request.symbol
        |> BulkInsertWithGivenIndex request.indexName
        |> Console.WriteLine

    0 // return an integer exit code