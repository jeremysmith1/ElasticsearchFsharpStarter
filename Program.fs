open ElasticWrapper
open Person
open AlphaAdv
open Newtonsoft.Json
open CurrencyModel
open System
open ESCurrencyModel
open System.Threading.Tasks

[<EntryPoint>]
let main argv =

    let currencySymbols = System.IO.File.ReadLines(@".\Resource\CurrencySymbols.csv") 
                        |> Seq.skip 1
                        |> Seq.map ((fun (line: string) -> line.Split ',') 
                        >> (fun (values: string[]) -> (values.[0], values.[1]))) 

    let marketString = marketStringFromMarketType MarketTypes.UnitedStatesDollar

    let convertResponse singleResponse= let convertedObj = JsonConvert.DeserializeObject<CryptoModel>(singleResponse) 
                                        if isNull convertedObj.MetaData 
                                        then None 
                                        else Some convertedObj

    let convertToESDatum symbolString (convertedObj: Option<CryptoModel>) = match convertedObj with
                                                                 | None -> None
                                                                 | Some obj -> obj.TimeSeriesDigitalCurrencyDaily 
                                                                                |> Seq.map (fun (KeyValue(k,v)) -> 
                                                                                 {Date = k; Datum = v; Symbol = symbolString; Market= marketString})
                                                                                 |> Some

    let bulkInsertData (symbolString: string) cryptoModel = match cryptoModel with
                                                             | None -> sprintf "Could not add: %A" symbolString 
                                                             | Some obj -> 
                                                                let result = BulkInsertCrypto (elasticsearchClientDefault ("crypto_" + symbolString.ToLower())) obj
                                                                if result 
                                                                then sprintf "Added: %A" symbolString 
                                                                else sprintf "Could not Index: %A" symbolString

    let requestCollection =  currencySymbols 
                             |> Seq.map (fun symbol ->  { nameOfFunction= FunctionTypes.Daily; symbol= (fst symbol); market= MarketTypes.UnitedStatesDollar; indexName = (snd symbol) })
                             |> Seq.toList

    let stopWatch = System.Diagnostics.Stopwatch.StartNew()
    stopWatch.Start()

    for request in requestCollection do
        DigitalCurrencyRequest request 
        |> convertResponse 
        |> convertToESDatum request.symbol
        |> bulkInsertData request.indexName
        |> Console.WriteLine
    // Parallel.ForEach(requestCollection, fun x -> DigitalCurrencyRequest x 
    //                                             |> convertResponse 
    //                                             |> convertToESDatum x.symbol
    //                                             |> bulkInsertData x.indexName
    //                                             |> Console.WriteLine) 
    // |> ignore
    stopWatch.Stop()
    printfn "%f" stopWatch.Elapsed.TotalMilliseconds  

    0 // return an integer exit code